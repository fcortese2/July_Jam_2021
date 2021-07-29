using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSettings : MonoBehaviour
{
    public float gammaPercentage = 5;
    public float volumePercentage = 50;

    public void increaseGammaPercentage()
    {
        gammaPercentage = Mathf.Clamp(gammaPercentage + 5, 5, 100);
    }

    public void decreaseGammaPercentage()
    {
        gammaPercentage = Mathf.Clamp(gammaPercentage - 5, 5, 100);
    }

    public void increaseVolumePercentage()
    {
        volumePercentage = Mathf.Clamp(volumePercentage + 5, 5, 100);
    }

    public void decreaseVolumePercentage()
    {
        volumePercentage = Mathf.Clamp(volumePercentage - 5, 0, 100);
    }
}
