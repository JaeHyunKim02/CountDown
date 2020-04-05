using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class M_InputManager : MonoBehaviour
{
    public GameObject Option;
    public GameObject PauseWindow;

    // Start is called before the first frame update
    void Start()
    {

    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if (PauseWindow.activeSelf == false)
                PauseWindow.SetActive(true);
            else if (PauseWindow.activeSelf == true)
                PauseWindow.SetActive(false);
        }

        if (Input.GetKeyDown(KeyCode.Escape)&&PauseWindow.activeSelf == true)//뒤로가기 키
        {
            if (Option.activeSelf == false)
                Option.SetActive(true);
            else if (Option.activeSelf == true)
                Option.SetActive(false);
        }
        if (Input.GetKeyDown(KeyCode.Escape) && PauseWindow.activeSelf == true)//뒤로가기 키
        {
            if (Option.activeSelf == false)
                Option.SetActive(true);
            else if (Option.activeSelf == true)
                Option.SetActive(false);
        }
    }
}
