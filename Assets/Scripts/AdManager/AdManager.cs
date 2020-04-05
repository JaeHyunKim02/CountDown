using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class AdManager : MonoBehaviour
{
    [SerializeField] GameObject AdImage;
	
	private void Awake()
	{
		if(PlayerPrefs.GetInt("NoAd")==1)
		{
			gameObject.SetActive(false);
            AdImage.SetActive(true);

		}
	}
	public void NoAdPurchase()
	{
		PlayerPrefs.SetInt("NoAd", 1);
		Debug.Log("구매완료");
	}
}
