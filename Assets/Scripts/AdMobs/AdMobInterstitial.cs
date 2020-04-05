using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;
using System;
public class AdMobInterstitial : MonoBehaviour
{
	private string appId = "ca-app-pub-5708876822263347~7435877324";//앱 아이디
	public readonly string unitID = "ca-app-pub-5708876822263347/3113488931";//실제 우리 애드몹 전면광고 아이디 그
																			 //테스트 아이디로 하지 않고 그냥 사용해서 테스트 할 경우 약관 위반으로 계정 정지먹을 수도 있음

	public readonly string unitID_Banner = "ca-app-pub-5708876822263347/7709656138";//배너광고 실제 아이디

	public readonly string test_unitID = "ca-app-pub-3940256099942544/1033173712";//전면광고 테스트용 광고 아이디
	public readonly string test_unitID_Banner = "ca-app-pub-3940256099942544/6300978111";//배너광고 테스트용 광고 아이디 

	public readonly string test_DeviceID = "D555BB9FAF714874";//재현 폰 디바이스 아이디

	private InterstitialAd frontAd;

	[SerializeField]
	private bool isTest;

	public static AdMobInterstitial instance = null;

	private void Awake()
	{
		if (instance != null)
			Destroy(gameObject);
		
		instance = this;
		
	}
	private void Start()
	{
		MobileAds.Initialize(appId);

		if (isTest)
			frontAd = new InterstitialAd(test_unitID);
		else
			frontAd = new InterstitialAd(unitID);

		frontAd.OnAdClosed += OnAdCloseListener;

		LoadAd();
	}

	public void OnAdCloseListener(object s, EventArgs args)
	{
		LoadAd();
	}

	public void LoadAd()
	{
		AdRequest request = new AdRequest.Builder().Build();
		frontAd.LoadAd(request);
	}

	public void ShowAd()
	{
		if (frontAd.IsLoaded())
			frontAd.Show();
		else
			print("Loaded Yet");
	}

	private IEnumerator ShowInterstitialAd()
	{
		while (!frontAd.IsLoaded())
		{
			yield return null;
		}
		frontAd.Show();
	}

}
