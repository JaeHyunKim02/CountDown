using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class TitleLoading : MonoBehaviour
{
    AsyncOperation TitleScene_New;
    // Start is called before the first frame update

    public static TitleLoading instance = null;
    
    public void Awake()
    {
        if (instance != null)
            Destroy(gameObject);

        instance = this;


    }
    void Start()
    {
        StartCoroutine("LoadingTitle");
    }

    IEnumerator LoadingTitle()
    {
        TitleScene_New = SceneManager.LoadSceneAsync("TitleScene_New");
        TitleScene_New.allowSceneActivation = false;

        yield return 0;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SceneChange()
    {
        TitleScene_New.allowSceneActivation = true;
    }
}
