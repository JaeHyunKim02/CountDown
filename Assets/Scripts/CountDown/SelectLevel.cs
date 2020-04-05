using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum Level
{
    STAGE_1, STAGE_2, STAGE_3
}
public class SelectLevel : MonoBehaviour
{

    public Canvas canvas;
    //public Level level;


	// Use this for initialization
	void Start ()
    {
        //DontDestroyOnLoad(this);
        //canvas.enabled = true;
	}
	
    public void Stage_1()
    {

        //GameObject.Find("FXManager").GetComponent<FXManager>().SoundManager_F("Touch");
        //level = Level.STAGE_1;
        //Debug.Log("Stage_1");
        SceneManager.LoadScene("CountDownScene");

        //canvas.enabled = false;
        //SceneManager.LoadScene(1);
    }

    //public void Stage_2()
    //{
    //    level = Level.STAGE_2;
    //    Debug.Log("Stage_2");
    //    SceneManager.LoadScene(1);
    //    canvas.enabled = false;
    //}

    //public void Stage_3()
    //{
    //    level = Level.STAGE_3;
    //    Debug.Log("Stage_3");
    //    SceneManager.LoadScene(1);
    //    canvas.enabled = false;
    //}
}
