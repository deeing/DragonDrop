using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class AdsManager : MonoBehaviour, IUnityAdsListener
{
    // create variables to hold iOS or Android gameIds/rewardedVideo Ids
#if UNITY_IOS
    string gameId = "4393808";
    string rewardedVideo = "Rewarded_iOS";
#else
    string gameId = "4393809";
    string rewardedVideo = "Rewarded_Android";
#endif

    // Start is called before the first frame update
    void Start()
    {
        Advertisement.Initialize(gameId);
        Advertisement.AddListener(this);
    }

    // Rewarded Ad
    public void PlayRewardedAd()
    {
        if (Advertisement.IsReady(rewardedVideo))
        {
            Advertisement.Show(rewardedVideo);
        } 
        else
        {
            Debug.Log("Rewarded ad is not ready!");
        }
    }

    public void OnUnityAdsReady(string placementId)
    {
        Debug.Log("Ads are ready!");
    }

    public void OnUnityAdsDidError(string message)
    {
        Debug.Log("Error: " + message);
    }

    public void OnUnityAdsDidStart(string placementId)
    {
        Debug.Log("Video Started");
    }

    public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
    {
        //should give users the reward here. Placing debug message for now.
        if (placementId == rewardedVideo && showResult == ShowResult.Finished)
        {
            Debug.Log("PLAYER SHOULD BE REWARDED!");
        }
    }

}
