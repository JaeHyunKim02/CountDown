using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoading : MonoBehaviour
{
    AsyncOperation CountDownScene;
    // Start is called before the first frame update

    public static SceneLoading instance = null;
    bool isChange = false;

    public void Awake()
    {
        if (instance != null)
            Destroy(gameObject);
        instance = this;

    }

    private void Start()
    {
        StartCoroutine(LoadScene());
       
    }

    private void Update()
    {
        if(!isChange)
        CountDownScene.allowSceneActivation = false;
        else
            CountDownScene.allowSceneActivation = true;
    }

    public void StartSceneChange()
    {
        myStatic.stageC = PlayerPrefs.GetInt("LastStage", 1);
        isChange = true;
    }

    public void SceneChange()
    {
        Debug.Log("Go SceneChange");
        isChange = true;
    }

    IEnumerator LoadScene()
    {
        CountDownScene = SceneManager.LoadSceneAsync("CountDownScene");
        CountDownScene.allowSceneActivation = false;
        yield return true;
    }
}
