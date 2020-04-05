using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoadIng : MonoBehaviour
{
    AsyncOperation async_Shop;
    AsyncOperation async_Title;

    AsyncOperation ao;

    // Start is called before the first frame update
    public void Start()
    {
        async_Shop = SceneManager.LoadSceneAsync("ShopScene");
        async_Shop.allowSceneActivation = false;
        async_Title = SceneManager.LoadSceneAsync("TitleScene");
        async_Title.allowSceneActivation = false;
        //ao = SceneManager.LoadSceneAsync("DemoScene");
        //ao.allowSceneActivation = false;
    }

    // 0~0.9 로딩 0.9~1.0 로딩 한거 띄우기

    public void SceneChageShop()
    {
        if(async_Shop.progress >= 0.9)
        {
            async_Shop.allowSceneActivation = true;
        }
       // if (ao.progress >= 0.9f)
       // {
       //     ao.allowSceneActivation = true;
       // }
    }
    public void SceneChageTitle()
    {
        if (async_Shop.progress >= 0.9)
        {
            async_Title.allowSceneActivation = true;
        }
    }
}
