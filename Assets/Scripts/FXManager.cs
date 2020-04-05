using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FXManager : MonoBehaviour
{
    public AudioClip Touch;
    public AudioClip WindowOff;
    public AudioClip Swipe;

    public AudioSource audio_F;

    void Update()
    {
        audio_F.volume = PlayerPrefs.GetFloat("BGMProgress", default);
    }

    public void SoundManager_F(string audioName)
    {

        if (audioName == "Touch")
        {
            audio_F.PlayOneShot(Touch);
        }

        if (audioName == "WindowOff")
        {
            audio_F.PlayOneShot(WindowOff);
        }

        if (audioName == "Swipe")
        {
            audio_F.PlayOneShot(Swipe);
        }
    }
}
