using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AdMob_Check : MonoBehaviour
{
	[SerializeField]
	bool IsGameScene = false;

	private void Update()
	{
		if (PlayerPrefs.GetInt("NoAd") != 1)//광고제거를 안샀더라면
		{
			if (SceneManager.GetActiveScene().name == "CountDownScene")
			{
				AdmobBanner.instance.ShowBanner();
				IsGameScene = true;
			}
			else
			{
				AdmobBanner.instance.HideBanner();
				IsGameScene = false;
			}
		}
	}
}
