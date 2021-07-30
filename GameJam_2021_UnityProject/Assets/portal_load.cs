using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class portal_load : MonoBehaviour
{

    public string nextLevelSceneName;

    public LayerMask whatIsPlayer;

    private void Update()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, 2f, whatIsPlayer);

        if (hits.Length > 0)
        {
            SceneManager.LoadSceneAsync(nextLevelSceneName);
        }

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, 2f);
    }


}
