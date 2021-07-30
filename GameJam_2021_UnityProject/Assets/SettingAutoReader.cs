using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingAutoReader : MonoBehaviour
{
    private void FixedUpdate()
    {
        GameObject.Find("SETTINGS").GetComponent<GameSettings>().UpdateSettings();
    }

}
