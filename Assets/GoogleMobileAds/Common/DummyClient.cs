using UnityEngine;
using GoogleMobileAds.Api;

namespace GoogleMobileAds.Common
{
    internal class DummyClient : IGoogleMobileAdsBannerClient, IGoogleMobileAdsInterstitialClient
    {
        public bool DebugOn = false;

        public DummyClient(IAdListener listener)
        {
            if(DebugOn) Debug.Log("Created DummyClient");
        }

        public void CreateBannerView(string adUnitId, AdSize adSize, AdPosition position)
        {
            if (DebugOn) Debug.Log("Dummy CreateBannerView");
        }

        public void LoadAd(AdRequest request)
        {
            if (DebugOn) Debug.Log("Dummy LoadAd");
        }

        public void ShowBannerView()
        {
            if (DebugOn) Debug.Log("Dummy ShowBannerView");
        }

        public void HideBannerView()
        {
            if (DebugOn) Debug.Log("Dummy HideBannerView");
        }

        public void DestroyBannerView()
        {
            if (DebugOn) Debug.Log("Dummy DestroyBannerView");
        }

        public void CreateInterstitialAd(string adUnitId) {
            if (DebugOn) Debug.Log("Dummy CreateIntersitialAd");
        }

        public bool IsLoaded() {
            if (DebugOn) Debug.Log("Dummy IsLoaded");
            return true;
        }

        public void ShowInterstitial() {
            if (DebugOn) Debug.Log("Dummy ShowInterstitial");
        }

        public void DestroyInterstitial() {
            if (DebugOn) Debug.Log("Dummy DestroyInterstitial");
        }
    }
}
