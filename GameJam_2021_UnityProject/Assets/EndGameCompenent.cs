using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndGameCompenent : MonoBehaviour
{
    static float pickupRange = 6;

    public LayerMask whatIsPlayer;
    public Text infoText;

    private void Update()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, pickupRange, whatIsPlayer);

        if (hits.Length > 0)
        {
            infoText.enabled = true;
            if (Input.GetKeyDown(KeyCode.E))
            {
                UnityEngine.SceneManagement.SceneManager.LoadSceneAsync("Ending");
            }
        }
        else
        {
            infoText.enabled = false;
        }
    }
}
