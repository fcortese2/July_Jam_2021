using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DoorKey : MonoBehaviour
{
    static float pickupRange = 2f;

    public LayerMask whatIsPlayer;
    public bool isUnlocked = false;

    public Text hiddenText;

    private void Update()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, pickupRange, whatIsPlayer);

        foreach (Collider hit in hits)
        {
            if (hit.GetComponent<Player_Controller>())
            {
                if (!isUnlocked)
                {
                    hiddenText.enabled = true;
                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        isUnlocked = true;
                    }
                }
            }
        }

        if (hits.Length == 0)
        {
            hiddenText.enabled = false;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, pickupRange);
    }

}
