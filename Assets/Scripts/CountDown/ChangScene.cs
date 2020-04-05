using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangScene : MonoBehaviour
{
    public void changeTitle()
    {
        GameObject.Find("FXManager").GetComponent<FXManager>().SoundManager_F("Touch");
        SceneManager.LoadScene("TitleScene");
    }

}
