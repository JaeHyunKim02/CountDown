using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Title : MonoBehaviour
{
    public Canvas canvas;
    public Image[] level;
    public Image[] BG;

    public void OnStart()
    {
        //Debug.Log("start");

        //for (int i = 0; i < level.Length; i++)
        //{
        //    level[i].enabled = true;
        //}
        //for (int i = 0; i < BG.Length; i++)
        //{
        //    BG[i].enabled = false;
        //}
    }

    public void OnExit()
    {
        Application.Quit();
    }
}
