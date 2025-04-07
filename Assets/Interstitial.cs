using GoogleMobileAds.Api;
using UnityEngine;

public class Interstitial : MonoBehaviour
{
    private InterstitialAd interstitialAd;

    void Start()
    {
        MobileAds.Initialize(initStatus =>
        {
            Debug.Log("AdMob Initialized");
            LoadInterstitialAd(); // Load after initialization
        });
    }

    public void LoadInterstitialAd()
    {
        string adUnitId = "ca-app-pub-4657651668563196/7227627018"; // Test ad ID

        // Destroy previous ad if any
        if (interstitialAd != null)
        {
            interstitialAd.Destroy();
            interstitialAd = null;
        }

        AdRequest adRequest = new AdRequest();

        InterstitialAd.Load(adUnitId, adRequest, (InterstitialAd ad, LoadAdError error) =>
        {
            if (error != null || ad == null)
            {
                Debug.LogError("Failed to load interstitial ad: " + error);
                return;
            }

            Debug.Log("Interstitial ad loaded.");
            interstitialAd = ad;

            // Ad loaded — now show it
            ShowInterstitialAd();

            // Reload after closing
            interstitialAd.OnAdFullScreenContentClosed += () =>
            {
                Debug.Log("Interstitial ad closed.");
                LoadInterstitialAd();
            };
        });
    }

    public void ShowInterstitialAd()
    {
        if (interstitialAd != null && interstitialAd.CanShowAd())
        {
            interstitialAd.Show();
        }
        else
        {
            Debug.Log("Interstitial ad not ready.");
        }
    }
}
