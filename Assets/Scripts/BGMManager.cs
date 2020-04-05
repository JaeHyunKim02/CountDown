using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMManager : MonoBehaviour
{
    public AudioSource audio;

    private void Awake()
    {
        audio.volume = (PlayerPrefs.GetFloat("BGMProgress", default) / 2);
    }
    void Start()
    {
        //audio.volume = (PlayerPrefs.GetFloat("BGMProgress", default) / 2);
        //DontDestroyOnLoad(gameObject);
    }
    void Update()
    {
        audio.volume = (PlayerPrefs.GetFloat("BGMProgress", default) / 2);
    }
}
