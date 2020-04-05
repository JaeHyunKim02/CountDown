using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PreLoading : MonoBehaviour
{
    public static PreLoading instance= null;

    public string SceneName;//불러올 씬 이름

    private void Awake()
    {
        if (instance)//인스턴스 생성
            Destroy(gameObject);

        instance = this;
    }

    public void LoadingStart()
    {
        StartCoroutine(LoadScene());
    }

    IEnumerator LoadScene()
    {
        yield return null;

        AsyncOperation op = SceneManager.LoadSceneAsync(SceneName);
        op.allowSceneActivation = false;

        float timer = 0.0f;
        while (!op.isDone)
        {
            yield return null;

            timer += Time.deltaTime;

            if (op.progress >= 0.9f)
            {
                if (op.progress >= 1.0f)
                    op.allowSceneActivation = true;
            }
        }
    }
}