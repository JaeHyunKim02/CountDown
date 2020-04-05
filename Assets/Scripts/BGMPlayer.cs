using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FXPlayer : MonoBehaviour
{
    GameObject FXManager;
   

    void Start()
    {
        //FXManager = GameObject.Find("FXManager");

        //if(SceneManager.GetActiveScene().name == "TitleScene" || SceneManager.GetActiveScene().name == "StageSelect")
        //    FXManager.GetComponent<FXManager>().SoundManager("TitleFX");
        //if (SceneManager.GetActiveScene().name == "InGame")
        //    FXManager.GetComponent<FXManager>().SoundManager("InGameFX");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
