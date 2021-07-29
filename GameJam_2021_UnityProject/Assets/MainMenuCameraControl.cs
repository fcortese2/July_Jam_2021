using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuCameraControl : MonoBehaviour
{
    public bool mainMenuFocused = true;

    [SerializeField] Text playText;
    [SerializeField] Text settingsText;
    [SerializeField] Text exitText;

    public GameObject settingsPanel;

    int normalFontSize = 25;
    int selectedFontSize = 40;

    public void PlayGame()
    {
        SceneManager.LoadSceneAsync("Level_1");
    }

    public void QuitGame()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }

    public void OpenSettings()
    {
        settingsPanel.gameObject.SetActive(true);
    }

    public void CloseSettings()
    {
        settingsPanel.gameObject.SetActive(false);
    }
}
