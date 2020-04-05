using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextScene : MonoBehaviour
{
    public void Next()
    {
        SceneManager.LoadScene("New_EditingScene");
        //SceneManager.LoadScene("EditingScene");
    }

    public void Prov()
    {
        myStatic.stageC = 1;
        for (int y = 0; y <= 4; y++)
        {
            for (int x = 0; x <= 4; x++)
            {
                EditManager.TestArray[x, y] = 0;    
            }
        }
        SceneManager.LoadScene("TestScene");
    }
}
