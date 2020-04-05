using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectButton : MonoBehaviour
{

    public GameObject TitleCanvas;
    public GameObject SelectCanvas;

    void Start()
    {
    }

    public void PressSelect()
    {
        GameObject.Find("FXManager").GetComponent<FXManager>().SoundManager_F("Touch");
        TitleCanvas.SetActive(false);
        SelectCanvas.SetActive(true);
    }
}
