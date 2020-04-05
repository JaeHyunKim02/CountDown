using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LoadCDScene : MonoBehaviour
{
    AsyncOperation CountDownScene;
    // Start is called before the first frame update

    public static LoadCDScene instance = null;

    public void Awake()
    {
        //if (instance != null)
        //    Destroy(gameObject);

        //instance = this;


    }
    void Start()
    {
        //StartCoroutine("LoadingTitle");
    }

    IEnumerator LoadingTitle()
    {
        CountDownScene = SceneManager.LoadSceneAsync("CountDownScene");
        CountDownScene.allowSceneActivation = false;

        yield return 0;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void CDSceneChange()
    {
        CountDownScene.allowSceneActivation = true;
    }
}
