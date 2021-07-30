using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class GameSettings : MonoBehaviour
{
    public float gammaPercentage = 5;
    public float volumePercentage = 50;
    public AudioMixerGroup mixer;

    private static GameSettings _instance;
    public static GameSettings Instance { get { return _instance; } }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    private void Start()
    {
        Object.DontDestroyOnLoad(this);
    }

    public void UpdateSettings()
    {
        if (gammaPercentage == 5)
        {
            GameObject.Find("Directional Light").GetComponent<Light>().intensity = 0.025f;
        }
        else
        {
            GameObject.Find("Directional Light").GetComponent<Light>().intensity = (.06f * (gammaPercentage / 5f));
        }

        
    }

    public void increaseGammaPercentage()
    {
        gammaPercentage = Mathf.Clamp(gammaPercentage + 5, 5, 100);
        UpdateSettings();
    }

    public void decreaseGammaPercentage()
    {
        gammaPercentage = Mathf.Clamp(gammaPercentage - 5, 5, 100);
        UpdateSettings();
    }

    public void increaseVolumePercentage()
    {
        volumePercentage = Mathf.Clamp(volumePercentage + 5, 5, 100);
        UpdateSettings();
    }

    public void decreaseVolumePercentage()
    {
        volumePercentage = Mathf.Clamp(volumePercentage - 5, 0, 100);
        UpdateSettings();
    }
}
