using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class LoadData : MonoBehaviour
{
    //AsyncOperation Loading;

    // Start is called before the first frame update
    void Start()
    {
       // Loading = 
        
        Application.targetFrameRate = 60;//프레임 60으로 고정

        Init_G_star();


        //PlayerPrefs.GetInt("LastStage", default);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Init_G_star()
    {

        if (myStatic.isHome)
        {
            myStatic.stageC = 1;
            myStatic.isHome = false;
            myStatic.TutorialStage = 0;
        }
    }
}
