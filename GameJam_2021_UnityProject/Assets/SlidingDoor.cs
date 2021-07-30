using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlidingDoor : MonoBehaviour
{
    static float pickupRange = 6;

    public LayerMask whatIsPlayer;
    public bool needsKeys = false;
    public DoorKey[] keys;

    Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, pickupRange, whatIsPlayer);

        foreach (Collider hit in hits)
        {
            if (hit.GetComponent<Player_Controller>())
            {
                if (needsKeys)
                {
                    foreach (DoorKey key in keys)
                    {
                        if (!key.isUnlocked)
                        {
                            return;
                        }
                    }
                    if (!GetComponent<AudioSource>().isPlaying)
                    {
                        GetComponent<AudioSource>().Play();
                    }
                    
                    animator.SetBool("open", true);
                }
                else
                {
                    if (!GetComponent<AudioSource>().isPlaying)
                    {
                        GetComponent<AudioSource>().Play();
                    }

                    animator.SetBool("open", true);
                }
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, pickupRange);
    }
}