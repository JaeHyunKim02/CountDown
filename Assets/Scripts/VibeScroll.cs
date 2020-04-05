using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VibeScroll : MonoBehaviour
{
    float VibeProgress = 0f;

    void Awake()
    {
        if (!PlayerPrefs.HasKey("VibeProgress"))
        {
            VibeProgress = PlayerPrefs.GetFloat("VibeProgress", 1);
        }
        else
        {
            VibeProgress = PlayerPrefs.GetFloat("VibeProgress", default);
        }

        if (VibeProgress == 1f)
            gameObject.GetComponent<Toggle>().isOn = true;
        else if (VibeProgress == 0f)
            gameObject.GetComponent<Toggle>().isOn = false;
        PlayerPrefs.Save();
    }

    public void VibeSave()
    {
        if (gameObject.GetComponent<Toggle>().isOn == true)
            VibeProgress = 1f;
        else if (gameObject.GetComponent<Toggle>().isOn == false)
            VibeProgress = 0f;
        PlayerPrefs.SetFloat("VibeProgress", VibeProgress);
        //Debug.Log(VibeProgress);
        PlayerPrefs.Save();
    }
}
