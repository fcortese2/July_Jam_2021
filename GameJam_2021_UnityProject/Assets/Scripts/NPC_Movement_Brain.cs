using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class NPC_Movement_Brain : MonoBehaviour
{
    public Vector3 Position { get { return transform.position; } }

    public List<Vector3> positions = new List<Vector3>();
    [Range(.2f, 20)][SerializeField] float moveSpeed = 2f;
    [Range(1f, 20f)] [SerializeField] float turnSpeed = 3f;
    [SerializeField] float arrivalSensitivity = .3f;
    [SerializeField] float intervalBetweenWaypoints = 2f;

    [SerializeField] Animator animator;

    private void Start()
    {
        transform.position = positions[0];
        StartCoroutine(LoopWalk(intervalBetweenWaypoints));
    }

    IEnumerator LoopWalk(float intervalBetweenPositions)
    {
        while (true)
        {
            for (int i = 0; i < positions.Count - 1; i++)
            {
                animator.SetBool("walk", true);
                animator.SetBool("idle", false);
                while (Vector3.Distance(transform.position, positions[i+1]) > arrivalSensitivity)
                {
                    transform.position = Vector3.MoveTowards(transform.position, positions[i + 1], moveSpeed * Time.deltaTime);
                    transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(positions[i + 1] - transform.position), turnSpeed * Time.deltaTime);

                    yield return new WaitForEndOfFrame();
                }

                animator.SetBool("walk", false);
                animator.SetBool("idle", true);
                yield return new WaitForSeconds(intervalBetweenPositions);
            }


            animator.SetBool("walk", true);
            animator.SetBool("idle", false);
            while (Vector3.Distance(transform.position, positions[0]) > arrivalSensitivity)
            {
                transform.position = Vector3.MoveTowards(transform.position, positions[0], moveSpeed * Time.deltaTime);
                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(positions[0] - transform.position), turnSpeed * Time.deltaTime);
                yield return new WaitForEndOfFrame();
            }
            animator.SetBool("walk", false);
            animator.SetBool("idle", true);
            yield return new WaitForSeconds(intervalBetweenPositions);

        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        foreach (Vector3 pathPoint in positions)
        {
            Gizmos.DrawSphere(pathPoint, arrivalSensitivity);
        }
    }

    
}

#if UNITY_EDITOR

[CustomEditor(typeof(NPC_Movement_Brain))]
public class NPC_Movement_Brain_Editor: Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        NPC_Movement_Brain npc = (NPC_Movement_Brain)target;

        if (GUILayout.Button("Append Current Position To Path..."))
        {
            npc.positions.Add(npc.Position);
        }

        if (GUILayout.Button("Clear Path"))
        {
            npc.positions = new List<Vector3>();
        }

    }
}
#endif