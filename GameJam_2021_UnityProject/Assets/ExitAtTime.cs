using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitAtTime : MonoBehaviour
{

    void Start()
    {
        StartCoroutine(ExitAt());
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            UnityEngine.SceneManagement.SceneManager.LoadSceneAsync("MainMenu");
        }
    }

    IEnumerator ExitAt()
    {
        yield return new WaitForSeconds(24);
        UnityEngine.SceneManagement.SceneManager.LoadSceneAsync("MainMenu");
        yield return null;
    }
}
