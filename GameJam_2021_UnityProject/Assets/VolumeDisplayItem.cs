using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeDisplayItem : MonoBehaviour
{
    GameSettings settings;

    private void Start()
    {
        settings = GameObject.Find("SETTINGS").GetComponent<GameSettings>();
    }

    void Update()
    {
        GetComponent<Text>().text = $"{settings.volumePercentage.ToString()}%";
    }
}
