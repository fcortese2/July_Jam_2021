using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class Camera_Player_Follow : MonoBehaviour
{
    [SerializeField] Transform focusPoint;
    [SerializeField] Vector3 followOffset;
    [HideInInspector] public float forceYoffset;

    private void Awake()
    {
        forceYoffset = followOffset.y;
        Cursor.visible = false;
    }

    private void FixedUpdate()
    {
        //float desiredY = Vector3.Lerp(transform.position, new Vector3(transform.position.x, forceYoffset, transform.position.z), 5 * Time.deltaTime).y;
        transform.position = focusPoint.position + new Vector3(followOffset.x, forceYoffset, followOffset.z);
        //transform.position = focusPoint.position + new Vector3(followOffset.x, desiredY, followOffset.z);
    }
}
