using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoLoadAfter : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(AutoStart(28));
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            UnityEngine.SceneManagement.SceneManager.LoadSceneAsync("Level_1");
        }
    }

    IEnumerator AutoStart(float t)
    {
        yield return new WaitForSeconds(t);
        Debug.Log("Load");
        UnityEngine.SceneManagement.SceneManager.LoadSceneAsync("Level_1");
        yield return null;
    }
}
