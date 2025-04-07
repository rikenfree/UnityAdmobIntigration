using UnityEngine;

using GoogleMobileAds.Api;

public class Banner : MonoBehaviour
{
    private BannerView bannerView;

    void Start()
    {
        // Initialize the Google Mobile Ads SDK.
        MobileAds.Initialize(initStatus =>
        {
            // Call only after SDK initialization.
            RequestBanner();
        });
    }

    private void RequestBanner()
    {
        string adUnitId;

#if UNITY_ANDROID
        adUnitId = "ca-app-pub-4657651668563196/1034524261"; // Test banner ad for Android
#elif UNITY_IPHONE
        adUnitId = "ca-app-pub-3940256699942544/2934735716"; // Test banner ad for iOS
#else
        adUnitId = "unexpected_platform";
#endif

        // Clean up before creating a new banner
        if (bannerView != null)
        {
            bannerView.Destroy();
        }

        // Create a 320x50 banner at the top of the screen.
        bannerView = new BannerView(adUnitId, AdSize.Banner, AdPosition.Top);

        // create our request used to load the ad.
        var adRequest = new AdRequest();

        // send the request to load the ad.
        Debug.Log("Loading banner ad.");
        bannerView.LoadAd(adRequest);
    }

  
}
