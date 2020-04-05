using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BGMScroll : MonoBehaviour
{
    float BGMProgress = 0f;

    void Awake()
    {
        if(!PlayerPrefs.HasKey("BGMProgress"))
        {
            BGMProgress = PlayerPrefs.GetFloat("BGMProgress", 1);
        }
        else
        {
            BGMProgress = PlayerPrefs.GetFloat("BGMProgress", default);
        }

        if(BGMProgress == 1f)
            gameObject.GetComponent<Toggle>().isOn = true;
        else if (BGMProgress == 0f)
            gameObject.GetComponent<Toggle>().isOn = false;
        PlayerPrefs.Save();
    }

    public void BGMSave()
    {
        if (gameObject.GetComponent<Toggle>().isOn == true)
            BGMProgress = 1f;
        else if (gameObject.GetComponent<Toggle>().isOn == false)
            BGMProgress = 0f;
        PlayerPrefs.SetFloat("BGMProgress", BGMProgress);
        PlayerPrefs.Save();
        //Debug.Log(BGMProgress);
    }
}
