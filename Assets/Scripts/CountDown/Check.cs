using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.TextCore;
using UnityEngine.Sprites;

public class Check : MonoBehaviour
{
	public GameObject ResultWindow;
	public GameObject ResultWindowStar;


	[SerializeField]
	private Text ShowLevel;
	[SerializeField]
	private Text NextLevel;

	[SerializeField]
	private Sprite Star_1;

	[SerializeField]
	private Sprite Star_2;

	[SerializeField]
	private Sprite Star_3;

	[SerializeField]
	private Image ResultWindowImage;

	public float LastAdShowTime;
	public float AdTimer;


	public static bool Once = false;
	bool isFinish;
	public static bool isOneSheck = true;
	private void Awake()
	{
		isOneSheck = true;
	}
	private void Start()
	{
		if (PauseButton.isPause)
			PauseButton.isPause = false;


		if (ResultWindow.activeSelf == true)
			ResultWindow.SetActive(false);



	}
	// Update is called once per frame
	void Update()
	{
		//if (!NodeManager.isClear)
		//{
		//	ResultWindowImage.sprite = null;
		//}
		
		if (NodeManager.isClear)//NodeController.FinishCh && !isFinish)
		{

		ResultWindow.SetActive(true);
			if (PlayerPrefs.GetInt("NoAd")!=1)//광고제거를 안샀더라면
			{//광고 재생
				if (myStatic.IsAddShowOne)
				{
				}
				else if (!myStatic.IsAddShowOne)
				{
					AdMobInterstitial.instance.ShowAd();
					myStatic.IsAddShowOne = true;
				}
			}
			isFinish = true;
			//Debug.Log("Finsh");
			
			if (!PlayerPrefs.HasKey("StageLevel_" + (myStatic.stageC + 1)))//다음스테이지가 열려있는게 아니라면
			{
				PlayerPrefs.SetInt("StageLevel_" + (myStatic.stageC + 1), 4);//열어줌

				PlayerPrefs.SetInt("LastStage", (myStatic.stageC + 1));//Start버튼에서 진입하는거
			}


			if (myStatic.SwipeCount <= myStatic.MinimumConut)
			{
				Debug.Log("3");

				ResultWindowStar.GetComponent<Image>().sprite = Star_3;
				//sprite = Star_3;
				PlayerPrefs.SetInt("StageLevel_" + (myStatic.stageC), 3);

			}
			else if (myStatic.SwipeCount <= myStatic.MinimumConut + 2)
			{
				Debug.Log("2");

				int temp = PlayerPrefs.GetInt(("StageLevel_") + (myStatic.stageC));
				Debug.Log("temp : " + temp);
				if (temp < 3)//3이 아니면
				{
					PlayerPrefs.SetInt("StageLevel_" + (myStatic.stageC), 2);
				}
				else if (temp == 4)//열리기만했을때
				{
					PlayerPrefs.SetInt("StageLevel_" + (myStatic.stageC), 2);
				}

				ResultWindowStar.GetComponent<Image>().sprite = Star_2;

			}
			else if (myStatic.SwipeCount >= myStatic.MinimumConut + 3)
			{
				Debug.Log("1");
				int temp = PlayerPrefs.GetInt(("StageLevel_") + (myStatic.stageC));

				if (temp < 2)
				{
					Debug.Log("temp : " + temp);
					PlayerPrefs.SetInt("StageLevel_" + (myStatic.stageC), 1);

				}
				else if (temp == 4)//열리기만했을때
				{
					PlayerPrefs.SetInt("StageLevel_" + (myStatic.stageC), 1);
				}

				ResultWindowStar.GetComponent<Image>().sprite = Star_1;
			}

			if (!Once)
			{
				GetComponent<AudioSource>().Play();
				Once = true;
			}

			if (!PauseButton.isPause)
				PauseButton.isPause = true;

			//if (OptionButtons.isVibration)

			if (PlayerPrefs.GetFloat("VibeProgress") == 1 && isOneSheck)
			{
				//Handheld.Vibrate();
				isOneSheck = false;
				//Debug.Log("Doit");
			}

			ShowLevel.text = "LEVEL " + (myStatic.stageC);

		}

	}
}
