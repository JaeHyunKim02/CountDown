using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;
using System;
public class AdmobBanner : MonoBehaviour
{
	private string appId = "ca-app-pub-5708876822263347~7435877324";

	public readonly string unitID_Banner = "ca-app-pub-5708876822263347/7709656138";//배너광고 실제 아이디

	public readonly string test_unitID_Banner = "ca-app-pub-3940256099942544/6300978111";//배너광고 테스트용 광고 아이디 


	private BannerView banner;

	[SerializeField]
	bool IsTest;

	public static AdmobBanner instance= null;

	//private InterstitialAd frontAd;

	private void Start()
	{
		MobileAds.Initialize(appId);

		if (instance != null)
			Destroy(gameObject);
		instance = this;

		this.RequestBanner(); 
		
	}
	public void RequestBanner()
	{
		string AdUnitID;
		if (IsTest)
		{
			AdUnitID = test_unitID_Banner;
		}
		else
		{
			AdUnitID = unitID_Banner;
		}
		banner = new BannerView(AdUnitID, AdSize.SmartBanner, AdPosition.Bottom);

		// Called when an ad request has successfully loaded.
		banner.OnAdLoaded += HandleOnAdLoaded_banner;
		// Called when an ad request failed to load.
		banner.OnAdFailedToLoad += HandleOnAdFailedToLoad_banner;
		// Called when an ad is clicked.
		banner.OnAdOpening += HandleOnAdOpened_banner;
		// Called when the user returned from the app after an ad click.
		banner.OnAdClosed += HandleOnAdClosed_banner;
		// Called when the ad click caused the user to leave the application.
		banner.OnAdLeavingApplication += HandleOnAdLeavingApplication_banner;

		AdRequest request = new AdRequest.Builder().Build();

		banner.LoadAd(request);
	}
	public void ShowBanner()
	{
		banner.Show();
	}

	public void HideBanner()
	{
		banner.Hide();
	}

	public void HandleOnAdLoaded_banner(object sender, EventArgs args)
	{
		MonoBehaviour.print("HandleAdLoaded event received_banner");
	}

	public void HandleOnAdFailedToLoad_banner(object sender, AdFailedToLoadEventArgs args)
	{
		MonoBehaviour.print("HandleFailedToReceiveAd_banner event received with message: "
							+ args.Message);
	}

	public void HandleOnAdOpened_banner(object sender, EventArgs args)
	{
		MonoBehaviour.print("HandleAdOpened event received_banner");
	}

	public void HandleOnAdClosed_banner(object sender, EventArgs args)
	{
		MonoBehaviour.print("HandleAdClosed event received_banner");
	}

	public void HandleOnAdLeavingApplication_banner(object sender, EventArgs args)
	{
		MonoBehaviour.print("HandleAdLeavingApplication event received_banner");
	}

}
