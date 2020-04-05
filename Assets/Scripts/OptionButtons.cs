using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class OptionButtons : MonoBehaviour
{
    public GameObject Option;
    public GameObject Credit;
    public static bool isVibration = true;

    [SerializeField]
    private Image Sound;

    [SerializeField]
    private Sprite SoundOn;

    [SerializeField]
    private Sprite SoundOff;

    private bool isOn = true;

    private void Update()
    {
        if (Input.GetKey(KeyCode.Escape))//뒤로가기 키
        {
            if (Option.activeSelf == false)
                Option.SetActive(true);
            if (Option.activeSelf == true)
                Option.SetActive(false);

        }
    }
    public void ToggleButton()
    {
        if (isOn)
        {
            Sound.sprite = SoundOff;
            PlayerPrefs.SetFloat("BGMProgress", 0.0f);
            PlayerPrefs.Save();
            isOn = false;
            Debug.Log("소리 끄기");
        }
        else if (!isOn)
        {
            Sound.sprite = SoundOn;
            PlayerPrefs.SetFloat("BGMProgress", 1.0f);
            PlayerPrefs.Save();
            isOn = true;
            Debug.Log("소리 키기");

        }
    }

    public void OpenCredit()
    {
        Credit.SetActive(true);
    }
    public void ClickOption()
    {
        GameObject.Find("FXManager").GetComponent<FXManager>().SoundManager_F("Touch");
        Option.SetActive(true);
    }

    public void CloseOption()
    {
        GameObject.Find("FXManager").GetComponent<FXManager>().SoundManager_F("WindowOff");
        Option.SetActive(false);
    }

    public void ClickCredit()
    {
        GameObject.Find("FXManager").GetComponent<FXManager>().SoundManager_F("Touch");
        Credit.SetActive(true);
    }

    public void CloseCredit()
    {
        GameObject.Find("FXManager").GetComponent<FXManager>().SoundManager_F("WindowOff");
        Credit.SetActive(false);
    }

    public void VibrationButton()
    {
        if (!isVibration)
        {
            isVibration = true;
        }
        else
            isVibration = false;

    }
}
