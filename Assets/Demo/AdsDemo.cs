using System;
using UnityEngine;
using GoogleMobileAds;
using GoogleMobileAds.Api;
using System.Collections;

public class AdsDemo : MonoBehaviour {
	public string AndroidBannerAdsUnit;
	public string AndroidIntersAdsUnit;
	public string iOSBannerAdsUnit;
	public string iOSIntersAdsUnit;
	public AdPosition AdsPosition;
	private BannerView bannerView;
	private InterstitialAd interstitial;
	
	bool _isBannerFailedToLoad = false;
	bool _isBannerLoaded = false;

	void Awake(){
		#if UNITY_ANDROID && !UNITY_EDITOR
		DontDestroyOnLoad(gameObject);
		//add event handler
		G.AddEventHandler(BaseEvent.AppStart, HandleStartSceneLoading);
		G.AddEventHandler(BaseEvent.UserUnPaused,HandleIntersAdsEvent);
		#endif
	}
	
	void OnDisable(){
		//remove event handler
		G.RemoveEventHandler(BaseEvent.AppStart, HandleStartSceneLoading);
		G.RemoveEventHandler(BaseEvent.UserUnPaused,HandleIntersAdsEvent);
	}
	
	#region Ads_Event
	void HandleIntersAdsEvent(GEvent e){
		ShowInterstitial();
	}
	
	void HandleBannerShowEvent(GEvent e){
		ShowBanner();
	}
	
	void HandleStartSceneLoading(GEvent e){
		StartCoroutine("FirstAdServing");
	}
	
	IEnumerator FirstAdServing(){
		RequestBanner();
		RequestInterstitial();
		var start = Time.unscaledTime;
		while(Time.unscaledTime - start < 5f)
		{
			if(_isBannerFailedToLoad)
				break;
			
			if(_isBannerLoaded){
				ShowBanner();
				break;
			}
			
			yield return null;
		}
	}
	#endregion
	
	private void RequestBanner(){
		_isBannerFailedToLoad = false;
		#if UNITY_EDITOR
		string adUnitId = "unused";
		#elif UNITY_ANDROID
		string adUnitId = AndroidBannerAdsUnit;
		#elif UNITY_IPHONE
		string adUnitId = iOSBannerAdsUnit;
		#else
		string adUnitId = "unexpected_platform";
		#endif
		
		// Create a 320x50 banner at the top of the screen.
		bannerView = new BannerView(adUnitId, AdSize.Banner, AdsPosition);
		// Register for ad events.
		bannerView.AdLoaded += HandleAdLoaded;
		bannerView.AdFailedToLoad += HandleAdFailedToLoad;
		bannerView.AdOpened += HandleAdOpened;
		bannerView.AdClosing += HandleAdClosing;
		bannerView.AdClosed += HandleAdClosed;
		bannerView.AdLeftApplication += HandleAdLeftApplication;
		// Load a banner ad.
		bannerView.LoadAd(createAdRequest());
	}
	
	private void RequestInterstitial()
	{
		#if UNITY_EDITOR
		string adUnitId = "unused";
		#elif UNITY_ANDROID
		string adUnitId = AndroidIntersAdsUnit;
		#elif UNITY_IPHONE
		string adUnitId = iOSIntersAdsUnit";
		#else
		string adUnitId = "unexpected_platform";
		#endif
		
		// Create an interstitial.
		interstitial = new InterstitialAd(adUnitId);
		// Register for ad events.
		interstitial.AdLoaded += HandleInterstitialLoaded;
		interstitial.AdFailedToLoad += HandleInterstitialFailedToLoad;
		interstitial.AdOpened += HandleInterstitialOpened;
		interstitial.AdClosing += HandleInterstitialClosing;
		interstitial.AdClosed += HandleInterstitialClosed;
		interstitial.AdLeftApplication += HandleInterstitialLeftApplication;
		// Load an interstitial ad.
		interstitial.LoadAd(createAdRequest());
	}
	
	// Returns an ad request with custom ad targeting.
	private AdRequest createAdRequest()
	{
		return new AdRequest.Builder()
			.AddTestDevice(AdRequest.TestDeviceSimulator)
				.AddTestDevice("0123456789ABCDEF0123456789ABCDEF")
				.AddKeyword("game")
				.SetGender(Gender.Male)
				.SetBirthday(new DateTime(1985, 1, 1))
				.TagForChildDirectedTreatment(false)
				.AddExtra("color_bg", "9B30FF")
				.Build();
		
	}
	
	public void ShowBanner()
	{
		//use to show Banner
		if (bannerView != null)
			bannerView.Show();
	}
	
	public void HideBanner()
	{
		if (bannerView != null)
			bannerView.Hide();
	}
	
	private void ShowInterstitial()
	{
		if (interstitial.IsLoaded())
		{
			interstitial.Show();
			RequestInterstitial();
		}
		else
		{
			print("Interstitial is not ready yet.");
		}
	}
	
	#region Banner callback handlers
	
	public void HandleAdLoaded(object sender, EventArgs args)
	{
		_isBannerLoaded = true;
		print("HandleAdLoaded event received.");
	}
	
	public void HandleAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
	{
		_isBannerFailedToLoad = true;
		print("HandleFailedToReceiveAd event received with message: " + args.Message);
	}
	
	public void HandleAdOpened(object sender, EventArgs args)
	{
		print("HandleAdOpened event received");
	}
	
	void HandleAdClosing(object sender, EventArgs args)
	{
		print("HandleAdClosing event received");
	}
	
	public void HandleAdClosed(object sender, EventArgs args)
	{
		print("HandleAdClosed event received");
	}
	
	public void HandleAdLeftApplication(object sender, EventArgs args)
	{
		print("HandleAdLeftApplication event received");
	}
	
	#endregion
	
	#region Interstitial callback handlers
	
	public void HandleInterstitialLoaded(object sender, EventArgs args)
	{
		print("HandleInterstitialLoaded event received.");
	}
	
	public void HandleInterstitialFailedToLoad(object sender, AdFailedToLoadEventArgs args)
	{
		print("HandleInterstitialFailedToLoad event received with message: " + args.Message);
	}
	
	public void HandleInterstitialOpened(object sender, EventArgs args)
	{
		print("HandleInterstitialOpened event received");
	}
	
	void HandleInterstitialClosing(object sender, EventArgs args)
	{
		print("HandleInterstitialClosing event received");
	}
	
	public void HandleInterstitialClosed(object sender, EventArgs args)
	{
		print("HandleInterstitialClosed event received");
	}
	
	public void HandleInterstitialLeftApplication(object sender, EventArgs args)
	{
		print("HandleInterstitialLeftApplication event received");
	}
	
	#endregion
}
