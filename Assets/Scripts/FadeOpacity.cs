using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FadeOpacity : MonoBehaviour
{
    public Image fade;
    float fades = 1.0f;
    float time = 0;
    GameObject FadeOff;
    private void Start()
    {
        FadeOff = GameObject.Find("Panel");
    }
    void Update()
    {
        time += Time.deltaTime;
        if (fades > 0.0f && time >= 0.05f)
        {
            fades -= 0.2f;
            fade.color = new Color(0, 0, 0, fades);
            time = 0;

        }
        else if (fades <= 0.1f)
        {
            FadeOff.SetActive(false);
            //행동할것
            time = 0;
        }
    }

}