using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class Camera_Player_Follow : MonoBehaviour
{
    [SerializeField] Transform focusPoint;
    [SerializeField] Vector3 followOffset;

    private void Update()
    {
        transform.position = focusPoint.position + followOffset;
    }
}
