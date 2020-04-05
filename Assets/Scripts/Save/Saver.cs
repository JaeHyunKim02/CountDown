using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Saver : MonoBehaviour
{
    bool bPaused = false;  // 어플리케이션이 내려진 상태인지 아닌지의 스테이트를 저장하기 위한 변수
    public GameObject PauseWindow;

    public float value;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnApplicationQuit()
    {
        PlayerPrefs.SetInt("LastStage", myStatic.stageC - 1);//세이브
    }

    void OnApplicationPause(bool pause)
    {

        if (pause)

        {

            bPaused = true;

            // todo : 어플리케이션을 내리는 순간에 처리할 행동들 /
            PlayerPrefs.SetInt("LastStage", myStatic.stageC - 1);//세이브

        }
        else
		{
            if (bPaused)
            {

                bPaused = false;
                PauseWindow.SetActive(true);//일시정지창 띄움
            }

        }
    }
}
