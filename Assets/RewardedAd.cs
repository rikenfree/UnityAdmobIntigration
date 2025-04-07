using UnityEngine;
using GoogleMobileAds.Api;
using System;

public class RewardedAdExample : MonoBehaviour
{
    private RewardedAd rewardedAd;
    private string adUnitId = "ca-app-pub-4657651668563196/1329676343"; // Test unit

    void Start()
    {
        MobileAds.Initialize(initStatus =>
        {
            Debug.Log("AdMob initialized");
            LoadRewardedAd();
        });
    }

    public void LoadRewardedAd()
    {
        // Clean up before loading new ad
        if (rewardedAd != null)
        {
            rewardedAd.Destroy();
            rewardedAd = null;
        }

        AdRequest request = new AdRequest();

        RewardedAd.Load(adUnitId, request, (RewardedAd ad, LoadAdError error) =>
        {
            if (error != null || ad == null)
            {
                Debug.LogError("Rewarded ad failed to load: " + error);
                return;
            }

            Debug.Log("Rewarded ad loaded successfully.");
            rewardedAd = ad;

            // Register events
            rewardedAd.OnAdFullScreenContentOpened += () =>
            {
                Debug.Log("Rewarded ad opened.");
            };

            rewardedAd.OnAdFullScreenContentClosed += () =>
            {
                Debug.Log("Rewarded ad closed. Reloading...");
                LoadRewardedAd();
            };

            rewardedAd.OnAdFullScreenContentFailed += (AdError adError) =>
            {
                Debug.LogError("Ad failed to show: " + adError.GetMessage());
                LoadRewardedAd();
            };

            rewardedAd.OnAdPaid += (AdValue adValue) =>
            {
                Debug.Log($"Ad paid: {adValue.Value} {adValue.CurrencyCode}");
            };

            rewardedAd.OnAdClicked += () =>
            {
                Debug.Log("Ad clicked.");
            };

            ShowRewardedAd();
        });
    }


    public void ShowRewardedAd()
    {
        if (rewardedAd != null && rewardedAd.CanShowAd())
        {
            rewardedAd.Show((Reward reward) =>
            {
                Debug.Log($"User earned reward! Type: {reward.Type}, Amount: {reward.Amount}");

                // TODO: Add your reward logic here (coins, lives etc.)
            });
        }
        else
        {
            Debug.Log("Rewarded ad not ready yet.");
        }
    }
}
