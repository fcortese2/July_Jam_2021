using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Player_Controller : MonoBehaviour
{
    [Tooltip("The lower the value, the higher the player speed")][SerializeField] float playerSpeed = 10;
    
    float m_h = 0, m_v = 0;
    Vector3 vel = Vector3.zero;

    #region component_refs
    Rigidbody rb;
    Camera camera;
    #endregion

    #region vision_control
    public float ViewRadius { get { return viewRadius; } }
    public float ViewAngle { get { return viewAngle; } }
    public LayerMask WhatIsEnemy { get { return whatIsEnemy; } }
    public LayerMask WhatIsWorld { get { return whatIsWorld; } }
    public Transform[] GetEnemiesInView { get { return enemiesInRange.ToArray(); } }

    [Header("Vision Control")]
    [SerializeField] float viewRadius;
    [Range(0,360)][SerializeField] float viewAngle;

    [SerializeField] LayerMask whatIsEnemy;
    [SerializeField] LayerMask whatIsWorld;
    List<Transform> enemiesInRange = new List<Transform>();
    #endregion

    #region mesh_setup
    [SerializeField] float meshResolution;
    private Mesh mesh;
    [SerializeField] private MeshFilter meshFilter;
    #endregion

    private void Start()
    {
        mesh = new Mesh();
        mesh.name = "vision mesh";
        meshFilter.mesh = mesh;

        rb = GetComponent<Rigidbody>();
        camera = Camera.main;
        StartCoroutine(FindTargetsWithDelay(.2f));
    }

    private void Update()
    {
        m_h = Input.GetAxisRaw("Horizontal");
        m_v = Input.GetAxisRaw("Vertical");

        Vector3 mousePos = camera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, camera.transform.position.y));
        transform.LookAt(mousePos + Vector3.up * transform.position.y);
        DrawVisionMesh();
    }

    private void FixedUpdate()
    {

        if (!(m_h == 0 && m_v == 0))
        {
            Vector3 newPos = new Vector3(transform.position.x + m_h, 1, transform.position.z + m_v);
            transform.position = Vector3.Lerp(transform.position, newPos, playerSpeed * Time.deltaTime);
        }
        
    }

    public Vector3 DirFromAngle(float angleInDegrees, bool angleIsGlobal)
    {
        if (!angleIsGlobal)
        {
            angleInDegrees += transform.eulerAngles.y;
        }
        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }

    public void FindVisibleTargets()
    {
        Collider[] targetsInViewRadius = Physics.OverlapSphere(transform.position, viewRadius, whatIsEnemy);
        enemiesInRange.Clear();

        for (int i = 0; i < targetsInViewRadius.Length; i++)
        {
            Transform target = targetsInViewRadius[i].transform;
            Vector3 dirToTarget = (target.position - transform.position).normalized;
            if (Vector3.Angle(transform.forward, dirToTarget) < viewAngle / 2)
            {
                float distToTarget = Vector3.Distance(transform.position, target.position);
                if (!Physics.Raycast(transform.position, dirToTarget, distToTarget, whatIsWorld))
                {
                    //if looking at enemy...
                    enemiesInRange.Add(target);
                }
            }
        }

    }

    IEnumerator FindTargetsWithDelay(float delay)
    {
        while (true)
        {
            yield return new WaitForSeconds(delay);
            FindVisibleTargets();
        }
    }

    void DrawVisionMesh()
    {
        int stepCount = Mathf.RoundToInt(viewAngle * meshResolution);
        float stepAngleSize = viewAngle / stepCount;

        List<Vector3> viewPoints = new List<Vector3>();

        for (int i = 0; i < stepCount; i++)
        {
            float angle = transform.eulerAngles.y - viewAngle / 2 + stepAngleSize * i;
            //Debug.DrawLine(transform.position, transform.position + DirFromAngle(angle, true) * viewRadius, Color.red);
            ViewCastInfo newViewCast = ViewCast(angle);
            viewPoints.Add(newViewCast.point);
        }

        int vertexCount = viewPoints.Count + 1;
        Vector3[] verts = new Vector3[vertexCount];
        int[] triangles = new int[(vertexCount - 2) * 3];

        verts[0] = Vector3.zero;
        for (int i = 0; i < vertexCount - 1; i++)
        {
            verts[i + 1] = transform.InverseTransformPoint(viewPoints[i]);

            if (i < vertexCount - 2)
            {
                triangles[i * 3] = 0;
                triangles[i * 3 + 1] = i + 1;
                triangles[i * 3 + 2] = i + 2;
            }
            
        }

        mesh.Clear();
        mesh.vertices = verts;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();


    }

    ViewCastInfo ViewCast(float globalAngle)
    {
        Vector3 dir = DirFromAngle(globalAngle, true);
        RaycastHit hit;
        if (Physics.Raycast(transform.position, dir, out hit, viewRadius, whatIsWorld))
        {
            return new ViewCastInfo(true, hit.point, hit.distance, globalAngle);
        }
        else
        {
            return new ViewCastInfo(false, transform.position + dir * viewRadius, viewRadius, globalAngle);
        }
    }

    public struct ViewCastInfo
    {
        public bool hit;
        public Vector3 point;
        public float dist;
        public float angle;

        public ViewCastInfo(bool _hit, Vector3 _point, float _dist, float _angle)
        {
            hit = _hit;
            point = _point;
            dist = _dist;
            angle = _angle;
        }
    }
}

[CustomEditor(typeof(Player_Controller))]
public class Player_Controller_Editor : Editor
{
    private void OnSceneGUI()
    {
        Player_Controller controller = (Player_Controller)target;
        Handles.color = Color.white;
        Handles.DrawWireArc(controller.transform.position, Vector3.up, Vector3.forward, 360, controller.ViewRadius);
        Vector3 viewAngleA = controller.DirFromAngle(-controller.ViewAngle / 2, false);
        Vector3 viewAngleB = controller.DirFromAngle(controller.ViewAngle / 2, false);

        Handles.DrawLine(controller.transform.position, controller.transform.position + viewAngleA * controller.ViewRadius);
        Handles.DrawLine(controller.transform.position, controller.transform.position + viewAngleB * controller.ViewRadius);

        foreach (Transform enemyInRange in controller.GetEnemiesInView)
        {
            Handles.color = Color.yellow;
            Handles.DrawDottedLine(controller.transform.position, enemyInRange.position, 5f);
        }
    }
}