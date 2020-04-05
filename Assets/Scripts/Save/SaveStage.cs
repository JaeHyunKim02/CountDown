using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SaveStage : MonoBehaviour
{
	int MaxButtonCount = 0;
	//GameObject[] MaxCount;
	[SerializeField]
	private GameObject StageCanvas;
	private void Awake()
	{
		PlayerPrefs.SetInt("OneTime", PlayerPrefs.GetInt("OneTime", 0));
		if(PlayerPrefs.GetInt("OneTime") == 0)
		{
			Debug.Log("한번만 할 일 실행");

			for(int i= 1; i <= FindButton(); i ++)
			{
				PlayerPrefs.SetInt("StageLevel_" + i, 0);
			}
			PlayerPrefs.SetInt("OneTime", 1);
			PlayerPrefs.SetInt("NoAd", 0);
		}
		else if(PlayerPrefs.GetInt("OneTime") != 0)
		{
			Debug.Log("메인 게임으로 바로");
		}
		StageCanvas.SetActive(false);
		Debug.Log(PlayerPrefs.GetInt("OneTime"));
	}

	int FindButton()
	{
		GameObject[] MaxCount = GameObject.FindGameObjectsWithTag("StageSelectButton");

		if (MaxButtonCount >= MaxCount.Length)
			MaxButtonCount = 0;

		for (int i = 0; i< MaxCount.Length; i++)
		{
			MaxButtonCount++;
		}
		Debug.Log(MaxButtonCount);

		return MaxButtonCount;

	}

	public void SetButton()
	{
			
	}

}