using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TutorialManager : MonoBehaviour
{
    enum Tutorial_Turn
    {
        HORIZONTAL,//0
        VERTICAL,//1
        TRY,//2
        GO,//3
    }
    [SerializeField]
    private float speed = 10;

    [SerializeField]
    private SpriteRenderer spriteRenderer;

    [SerializeField]
    private Sprite text_1;
    [SerializeField]
    private Sprite text_2;
    [SerializeField]
    private Sprite text_3;
    [SerializeField]
    private Sprite text_4;

    [SerializeField]
    private Tutorial_Turn turn;

    public GameObject Hor;
    public GameObject Ver;

    [SerializeField]
    private GameObject LetsGo;
    bool IsOne;


    public static bool ising;//이미 게임중인지
    public GameObject _GameManager;


    public static bool isClear;

	[SerializeField]
	bool IsGoTitle = false;

    private void Awake()
    {
        ising = false;
		InitTutorial();
    }
	void InitTutorial()
	{
		IsGoTitle = false;
	}
    private void Start()
    {

        //turn = Tutorial_Turn.HORIZONTAL;

        if (myStatic.TutorialStage == 0)
            turn = Tutorial_Turn.HORIZONTAL;
        else if (myStatic.TutorialStage == 1)
            turn = Tutorial_Turn.VERTICAL;
        else if (myStatic.TutorialStage == 2)
            turn = Tutorial_Turn.TRY;
        else if (myStatic.TutorialStage == 3)
            turn = Tutorial_Turn.GO;
        //spriteRenderer.sprite = text_1;
        
        Turn();
        IsOne = false;
        Debug.Log(turn);
    }

    private void Update()
    {
        //if(turn == Tutorial_Turn.asdf && !ising)
        //{
        //    if(Input.GetMouseButtonDown(0))
        //    {
        //        turn = Tutorial_Turn.TRY;
        //        ising = true;
        //        Turn();
        //    }
        //}
        //else if (turn != Tutorial_Turn.TRY )
        //{
        //    if (Input.GetMouseButtonDown(0))
        //    {
        //        Turn();
        //    }
        //}

        if(isClear)
        {
            isClear = false;
            Debug.Log(turn);
            SceneManager.LoadScene("Tutorial");
        }
		if(IsGoTitle)
		{
			if(Input.GetMouseButtonDown(0))
			{
                SceneLoading.instance.StartSceneChange();
			}
		}


    }
    void Turn()
    {
        
        //Input.GetTouch(0).phase == TouchPhase.Began ||
        switch (turn)
        {
            case Tutorial_Turn.HORIZONTAL:
                Horizontal_Tutorial();
                //TutorialGame.instance.InitNodeArr(5);
                //TutorialGame.instance.InitStage(0);
                Debug.Log("옴");
                break;

            case Tutorial_Turn.VERTICAL:
                Vertical_Tutorial();
                Destroy(GameObject.Find("Hor(Clone)"));
                //TutorialGame.instance.InitStage(1);

                break;

            case Tutorial_Turn.TRY:
                Destroy(GameObject.Find("Ver(Clone)"));
                spriteRenderer.sprite = text_3;
                //TutorialGame.instance.InitStage(2);

                break;

            case Tutorial_Turn.GO:
				GoTitle();
				IsGoTitle = true;

				break;

            default:
                turn = Tutorial_Turn.HORIZONTAL;

                Debug.Log("??");
                break;
        }
    }

    void Horizontal_Tutorial()//수평
    {
        Instantiate(Hor, new Vector3(0,-3.08f, 0), Quaternion.identity);
        spriteRenderer.sprite = text_2;
    }
    void Vertical_Tutorial()//수직
    {
        //3.03
        Instantiate(Ver, new Vector3(3.03f, 0, 0), Quaternion.identity);
        spriteRenderer.sprite = text_1;
    }
    void GoTitle()
    {
        spriteRenderer.sprite = null;
        Instantiate(LetsGo,GameObject.Find("Canvas").transform);
    }
}
