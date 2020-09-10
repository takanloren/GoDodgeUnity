using Assets.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour, IUnityAdsListener
{
	//Watch Ads button
	public Button ShieldWatchAds;
	public Button SpeedPointWatchAds;
	public Button GemWatchAds;

	public Button ShieldBuy;
	public Button SpeedPointBuy;
	public Button Gem099Buy;
	public Button Gem499Buy;
	public Button Gem999Buy;

	string gameId = Constants.GOOGLE_PLAY_GAME_ID;
	bool testMode = true;
	void Start()
	{
		Advertisement.AddListener(this);
		Advertisement.Initialize(gameId, testMode);
	}

	void Update()
	{
		CheckingGemAmount();
	}

	public void OnUnityAdsDidError(string message)
	{
		Util.ShowAndroidToastMessage(message);
	}

	public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
	{
		// Define conditional logic for each ad completion status:
		if (showResult == ShowResult.Finished)
		{
			if (placementId == Constants.Shield_Reward_PlacementID)
			{
				Util.ShowAndroidToastMessage("You have earned a shield reward");
				GameManager.Instance.PlayerEquipment.ShieldAmount++;
			}
			else if (placementId == Constants.SpeedPotion_Reward_PlacementID)
			{
				Util.ShowAndroidToastMessage("You have earned a speed potion reward");
				GameManager.Instance.PlayerEquipment.SpeedPotionAmount++;
			}
			else if (placementId == Constants.Gem_Reward_PlacementID)
			{
				Util.ShowAndroidToastMessage("You have earned 50 gems reward");
				GameManager.Instance.PlayerEquipment.GemAmount += Constants.GEM_ADS_REWARD;
			}
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

	public void OnUnityAdsDidStart(string placementId)
	{
		//
	}

	public void OnUnityAdsReady(string placementId)
	{
		// If the ready Placement is rewarded, activate the button: 
		if (placementId == Constants.Shield_Reward_PlacementID)
		{
			ShieldWatchAds.interactable = true;
		}
		else if (placementId == Constants.SpeedPotion_Reward_PlacementID)
		{
			SpeedPointWatchAds.interactable = true;
		}
		else if (placementId == Constants.Gem_Reward_PlacementID)
		{
			GemWatchAds.interactable = true;
		}
	}

	public void ShowRewardedVideo(string placementID)
	{
		// Check if UnityAds ready before calling Show method:
		if (Advertisement.IsReady(placementID))
		{
			Advertisement.Show(placementID);
		}
		else
		{
			Debug.Log("Rewarded video is not ready at the moment! Please try again later!");
		}
	}

	public void ClickedOnShieldAds()
	{
		ShowRewardedVideo(Constants.Shield_Reward_PlacementID);
	}

	public void ClickedOnSpeedPotionAds()
	{
		ShowRewardedVideo(Constants.SpeedPotion_Reward_PlacementID);
	}

	public void ClickedOnGemAds()
	{
		ShowRewardedVideo(Constants.Gem_Reward_PlacementID);
	}

	public void ClickedOnBuyShield()
	{
		if (ableToBuyItem(Constants.SHIELD_PRICE))
		{
			GameManager.Instance.PlayerEquipment.GemAmount -= Constants.SHIELD_PRICE;

			GameManager.Instance.PlayerEquipment.ShieldAmount++;

			Util.ShowAndroidToastMessage("You have bougth a shield!");
		}
	}

	public void ClickedOnBuySpeedPotion()
	{
		if (ableToBuyItem(Constants.SPEED_POTION_PRICE))
		{
			GameManager.Instance.PlayerEquipment.GemAmount -= Constants.SPEED_POTION_PRICE;

			GameManager.Instance.PlayerEquipment.SpeedPotionAmount++;

			Util.ShowAndroidToastMessage("You have bought a speed boost potion!");
		}
	}

	public void ClickedOnBuyGem099()
	{
		IAPManager.INSTANCE.BuyGem099();
	}

	public void ClickedOnBuyGem499()
	{
		IAPManager.INSTANCE.BuyGem499();
	}

	public void ClickedOnBuyGem999()
	{
		IAPManager.INSTANCE.BuyGem999();
	}

	public void BackToMapManager()
	{
		GameManager.Instance.StartMap(GameManager.Map.MapManager);
	}

	private void CheckingGemAmount()
	{
		if (ableToBuyItem(Constants.SHIELD_PRICE))
		{
			ShieldBuy.interactable = true;
		}
		else
		{
			ShieldBuy.interactable = false;
		}

		if (ableToBuyItem(Constants.SPEED_POTION_PRICE))
		{
			SpeedPointBuy.interactable = true;
		}
		else
		{
			SpeedPointBuy.interactable = false;
		}
	}

	private bool ableToBuyItem(int price)
	{
		int playerGemAmount = GameManager.Instance.PlayerEquipment.GemAmount;

		if(playerGemAmount >= price)
		{
			return true;
		}

		return false;
	}

	public void OnDestroy()
	{
		Advertisement.RemoveListener(this);
	}
}
