using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    AsyncOperation TitleScene;
    public static AsyncOperation CountDownScene;

    public GameObject TitleCanvas;
    public GameObject SelectCanvas;

    public static bool isLoadingCDScene;





    private void Awake()
    {

        isLoadingCDScene = false;
    }

    void Start()
    {

        // StartCoroutine("LoadingScene");
        //StartCoroutine("LoadingMainScene");

//CountDownScene = SceneManager.LoadSceneAsync("CountDownScene");
       // CountDownScene.allowSceneActivation = false;

       // StartCoroutine("LoadingMainScene");
    }

    //IEnumerator LoadingScene()
    //{
    //    TitleScene = SceneManager.LoadSceneAsync("TitleScene");
    //    TitleScene.allowSceneActivation = false;

    //    if (CountDownScene.progress >= 0.9f)
    //        yield return TitleScene;
    //}

    //IEnumerator LoadingMainScene()
    //{
    //    Debug.Log("Just start");
    //    CountDownScene = SceneManager.LoadSceneAsync("CountDownScene");
    //    CountDownScene.allowSceneActivation = false;
    //    if (CountDownScene.progress >= 0.9f)
    //        yield return null;
    //}

    void Update()
    {
        //if (isLoadingCDScene)
        //{
        //    CountDownScene.allowSceneActivation = true;
        //    Debug.Log(isLoadingCDScene);
        //}
    }

    public void GoTitleScene()
    {
        GameObject.Find("FXManager").GetComponent<FXManager>().SoundManager_F("Touch");
        TitleCanvas.SetActive(true);
        SelectCanvas.SetActive(false);

        //CountDownScene.allowSceneActivation = true;
    }
}
