using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoader_MK2 : MonoBehaviour
{
    AsyncOperation async;
    void Start()
    {
        //async = SceneManager.LoadSceneAsync("CountDownScene");
        //async.allowSceneActivation = false;
        //StartCoroutine("StartScene");
    }

    //IEnumerator StartScene()
    //{
        
    //    if(async.progress >= 0.9f)
    //        yield return async;
    //}

    public void GoStart()
    {
        GameObject.Find("FXManager").GetComponent<FXManager>().SoundManager_F("Touch");
        //async.allowSceneActivation = true;
        SceneManager.LoadScene("CountDownScene");
    }

    public void GoStageSelect()
    {
        GameObject.Find("FXManager").GetComponent<FXManager>().SoundManager_F("Touch");
        SceneManager.LoadScene("StageSelect");
    }
}