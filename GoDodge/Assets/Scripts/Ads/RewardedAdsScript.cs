using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class RewardedAdsScript: MonoBehaviour, IUnityAdsListener
{
	string rewardPlacementID = "rewardedVideo";
	string gameId = "3808815";
	bool testMode = true;
	void Start()
	{
		Advertisement.AddListener(this);
		Advertisement.Initialize(gameId, testMode);
	}

	// Implement IUnityAdsListener interface methods:
	public void OnUnityAdsReady(string placementId)
	{
		// If the ready Placement is rewarded, activate the button: 
		if (placementId == rewardPlacementID)
		{
			//myButton.interactable = true;
		}
	}

	public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
	{
		// Define conditional logic for each ad completion status:
		if (showResult == ShowResult.Finished)
		{
			// Reward the user for watching the ad to completion.
		}
		else if (showResult == ShowResult.Skipped)
		{
			// Do not reward the user for skipping the ad.
		}
		else if (showResult == ShowResult.Failed)
		{
			Debug.LogWarning("The ad did not finish due to an error.");
		}
	}

	public void OnUnityAdsDidError(string message)
	{
		// Log the error.
	}

	public void OnUnityAdsDidStart(string placementId)
	{
		// Optional actions to take when the end-users triggers an ad.
	}

	public void ShowRewardedVideo()
	{
		// Check if UnityAds ready before calling Show method:
		if (Advertisement.IsReady(rewardPlacementID))
		{
			Advertisement.Show(rewardPlacementID);
		}
		else
		{
			Debug.Log("Rewarded video is not ready at the moment! Please try again later!");
		}
	}

	// Start is called before the first frame update


    // Update is called once per frame
    void Update()
    {
        
    }
}
