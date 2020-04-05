using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
    public string NextSceneName;

    public void NextScene()
    {
        GameObject.Find("FXManager").GetComponent<FXManager>().SoundManager_F("Touch");
        SceneManager.LoadScene(NextSceneName);
    }

    public void Restart()
    {
        GameObject.Find("FXManager").GetComponent<FXManager>().SoundManager_F("Touch");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Locked()
    {
        GameObject.Find("FXManager").GetComponent<FXManager>().SoundManager_F("WindowOff");
    }

        public void GoHome()
    {
        GameObject.Find("FXManager").GetComponent<FXManager>().SoundManager_F("Touch");
        if (PauseButton.isPause)
        {
            PauseButton.isPause = false;
        }
        SceneManager.LoadScene("TitleScene_New");
        myStatic.isHome = true;
        //myStatic.stageC -= 1;

    }
    public void GoHomeGermanytesting()
    {
        GameObject.Find("FXManager").GetComponent<FXManager>().SoundManager_F("Touch");

        if (PauseButton.isPause)
        {
            PauseButton.isPause = false;
        }
        SceneManager.LoadScene("TitleScene_New");

        myStatic.isHome = true;
    }
}