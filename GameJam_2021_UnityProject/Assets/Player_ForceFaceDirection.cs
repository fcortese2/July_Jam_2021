using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_ForceFaceDirection : MonoBehaviour
{
    private void LateUpdate()
    {
        transform.rotation = Quaternion.LookRotation(Vector3.forward, Vector3.up);
    }

}
