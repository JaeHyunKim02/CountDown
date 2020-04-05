using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class _SceneChange : MonoBehaviour
{
    public string NextSceneName;

    public void NextScene()
    {
        GameObject.Find("FXManager").GetComponent<FXManager>().SoundManager_F("Touch");
        SceneManager.LoadScene(NextSceneName);
    }

    public void NextSceneLoad()
    {
        GameObject.Find("FXManager").GetComponent<FXManager>().SoundManager_F("Touch");
        Debug.Log("_SceneChange");
        SceneLoader.CountDownScene.allowSceneActivation = true;
    }
}