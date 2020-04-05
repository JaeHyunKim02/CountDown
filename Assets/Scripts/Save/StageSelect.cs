using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Sprites;
using UnityEngine.UI;

public class StageSelect : MonoBehaviour
{
	public static int value;
	public Sprite NoClear, Star1, Star2, Star3;
	SpriteRenderer spriteRenderer;


	bool isClear;


	public void ClickStageButton()//누른 번호의 스테이지로 이동함
	{
		GameObject.Find("FXManager").GetComponent<FXManager>().SoundManager_F("Touch");
		value = int.Parse(gameObject.name);

		if (PlayerPrefs.HasKey("StageLevel_" + value))
		{
			Debug.Log("asdfasdf");
		}
		if (PlayerPrefs.GetInt("StageLevel_" + value) > 0)
		{

			Debug.Log(PlayerPrefs.GetInt("StageLevel_" + value));

			value = int.Parse(gameObject.name);
			myStatic.stageC = value;

			SceneLoading.instance.SceneChange();
			Debug.Log("isLoading");
		}
		//value = int.Parse(gameObject.name);
		//myStatic.stageC = value;
		//SceneLoading.instance.SceneChange();
		//Debug.Log("isLoading");
	}
}