using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playerprefs : MonoBehaviour
{
    void Awake()
    {
        if (!PlayerPrefs.HasKey("BGMProgress"))
        {
            PlayerPrefs.SetFloat("BGMProgress", 1);
        }

        if (!PlayerPrefs.HasKey("VibeProgress"))
        {
            PlayerPrefs.SetFloat("VibeProgress", 1);
        }
        PlayerPrefs.Save();
    }
}
