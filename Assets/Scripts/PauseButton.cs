using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseButton : MonoBehaviour
{
    public GameObject PauseWindow;
    public static bool isPause = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ClickPause()
    {
        GameObject.Find("FXManager").GetComponent<FXManager>().SoundManager_F("Touch");
        PauseWindow.SetActive(true);
        isPause = true;
        //Debug.Log("isPause");
    }

    public void ClosePause()
    {
        GameObject.Find("FXManager").GetComponent<FXManager>().SoundManager_F("Touch");
        PauseWindow.SetActive(false);
        isPause = false;
        //Debug.Log("!isPause");
    }
} 
