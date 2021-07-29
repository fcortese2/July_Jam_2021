using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MenuOptionItem : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        transform.GetChild(0).GetComponent<Text>().fontSize = 40;
        if (transform.GetChild(0).GetComponent<Text>().text != "S E T T I N G S")
        {
            GameObject.Find("Main Camera").GetComponent<MainMenuCameraControl>().CloseSettings();
        }
    }

    public void OnPointerExit(PointerEventData pointerEventData)
    {
        transform.GetChild(0).GetComponent<Text>().fontSize = 25;
    }

}
