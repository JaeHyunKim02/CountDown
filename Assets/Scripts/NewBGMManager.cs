using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NewBGMManager : MonoBehaviour
{
    public static NewBGMManager instance=null;
    public AudioSource audio;
    static int InstantCount = 0;

    private void Awake()
    {

        if (InstantCount == 0 && SceneManager.GetActiveScene().name == "CountDownScene" && (null == GameObject.Find("InGameBGMManager") || null == GameObject.Find("InGameBGMManager(Clone)")))
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            InstantCount++;
        }
        else if (SceneManager.GetActiveScene().name == "CountDownScene" && (null != GameObject.Find("InGameBGMManager") || null != GameObject.Find("InGameBGMManager(Clone)")))
            Destroy(gameObject);
        else if (SceneManager.GetActiveScene().name != "CountDownScene" && (null != GameObject.Find("InGameBGMManager") || null != GameObject.Find("InGameBGMManager(Clone)")))
            Destroy(gameObject);

        audio.volume = (PlayerPrefs.GetFloat("BGMProgress", default) / 2);
    }

    void Update()
    {
        audio.volume = (PlayerPrefs.GetFloat("BGMProgress", default) / 2);

        if (SceneManager.GetActiveScene().name != "CountDownScene" && (null != GameObject.Find("InGameBGMManager") || null != GameObject.Find("InGameBGMManager(Clone)")))
        {
            Destroy(gameObject);
            InstantCount--;
        }
    }
}
