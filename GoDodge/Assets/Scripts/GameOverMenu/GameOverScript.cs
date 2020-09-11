using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverScript : MonoBehaviour, IUnityAdsListener
{
    public GameObject gameOverlayUI;
    public GameObject gameOverMenuUI;
    public TextMeshProUGUI highestLevel;
    public TextMeshProUGUI totalTime;
    public TMP_InputField enterNameField;
	public GameObject respawnButton;
	public GameObject watchAdsButton;
	public TextMeshProUGUI gemEarned;

	string respawnRewardPlacementID = Constants.Respawn_Reward_PlacementID;
	bool testMode = true;
	private bool showingGOMenuUI = false;

    // Start is called before the first frame update
    void Start()
    {
		respawnButton.SetActive(false);
		watchAdsButton.SetActive(true);

		Advertisement.AddListener(this);
		Advertisement.Initialize(Constants.GOOGLE_PLAY_GAME_ID, testMode);

		gameOverlayUI.SetActive(false);
        gameOverMenuUI.SetActive(false);
    }

	private void PlayMusic(bool isGamePlayMusic)
	{
		if (isGamePlayMusic)
		{
			GameObject.FindGameObjectWithTag("MusicGamePlay").GetComponent<MusicClass>().PlayMusic();
			GameObject.FindGameObjectWithTag("MusicGameOver").GetComponent<MusicClass>().StopMusic();
		}
		else
		{
			GameObject.FindGameObjectWithTag("MusicGamePlay").GetComponent<MusicClass>().StopMusic();
			GameObject.FindGameObjectWithTag("MusicGameOver").GetComponent<MusicClass>().PlayMusic();
		}
	}

    // Update is called once per frame
    void Update()
    {
		try
		{
			if (GameManager.Instance.ActiveRunAttemp.IsGameOver)
			{
				if (!showingGOMenuUI)
				{
					showingGOMenuUI = true;
					DisplayGameOverWindow();
				}
			}
		}
		catch(Exception ex)
		{
			Debug.LogError(ex.Message);
		}
        
    }

    public void DisplayGameOverWindow()
    {
		Time.timeScale = 0f;

		try
		{
			PlayMusic(false);
		}
        catch (Exception ex)
		{
            Debug.Log(ex.Message);
		}

        gameOverlayUI.SetActive(true);
        gameOverMenuUI.SetActive(true);

        highestLevel.text = GameManager.Instance.ActiveRunAttemp.FinishedLevel.ToString();
        totalTime.text = CalculateTotalTime();

		int gemEarnedValue = GameManager.Instance.ActiveRunAttemp.GemEarned;

		gemEarned.text = gemEarnedValue.ToString();

		GameManager.Instance.PlayerEquipment.GemAmount += gemEarnedValue;

		SavePlayerRecord();

		//Maximum respawn 5 times in 1 run attempt
		if(GameManager.Instance.ActiveRunAttemp.TotalTimeRevival >= 5)
		{
			respawnButton.SetActive(false);
			watchAdsButton.SetActive(true);
			watchAdsButton.GetComponent<Button>().interactable = false;
		}
    }

    private string CalculateTotalTime()
    {
        //set the end time for run attempt
        GameManager.Instance.ActiveRunAttemp.EndTime = DateTime.Now;

        var totalTimeSpan = GameManager.Instance.ActiveRunAttemp.TotalTimeCost;

        string totalTimeText = FirebaseLeaderboardHandler.Instance.TimeSpanToString(totalTimeSpan.TotalMilliseconds);

        return totalTimeText;
    }

    public void OK()
    {
        GameManager.Instance.ResetAll();
        showingGOMenuUI = false;
        Time.timeScale = 1f;

		PlayMusic(true);

		SceneManager.LoadScene("Menu");
    }

	public void BackToMapSelection()
	{
		GameManager.Instance.ResetAll();
		showingGOMenuUI = false;
		Time.timeScale = 1f;

		PlayMusic(true);

		SceneManager.LoadScene("MapManager");
	}

	private LeaderboardEntry GetLeaderboardEntry()
	{
		string name = enterNameField.text;
		int lvel = GameManager.Instance.ActiveRunAttemp.FinishedLevel;
		double time = GameManager.Instance.ActiveRunAttemp.TotalTimeCost.TotalMilliseconds;
		string map = GameManager.Instance.ActiveRunAttemp.Map.ToString();

		return new LeaderboardEntry(name, map, lvel, time);
	}

	public void SubmitScore()
    {
		LeaderboardEntry entry = GetLeaderboardEntry();

		Debug.Log($"Submitted entry = {entry.ToString()}");

        FirebaseLeaderboardHandler.Instance.SubmitScore(entry);

        enterNameField.gameObject.SetActive(false);
    }

	public void PlayAgain()
	{
		GameManager.Instance.ResetAll();
		showingGOMenuUI = false;
		Time.timeScale = 1f;

		PlayMusic(true);

		GameManager.Instance.StartMap(GameManager.Instance.ActiveRunAttemp.Map);
	}

	private void SavePlayerRecord()
	{
		Debug.Log("Saving player record...");

		SQLiteHelper.INSTANCE.OpenDatabase(); // <<< OPEN DB

		List<MapModel> allMap = SQLiteHelper.INSTANCE.GetAll_MapTable();

		List<PlayerMapRecordedModel> allPlayerRecorded = SQLiteHelper.INSTANCE.GetAll_PlayerMapRecorded();

		SQLiteHelper.INSTANCE.CloseDatabase(); // <<< CLOSE DB

		foreach (var map in allMap)
		{
			if (map.mapName.Equals(GameManager.Instance.ActiveRunAttemp.Map.ToString()))
			{
				int mapID = map.mapID;
				string mapName = map.mapName;
				string mapMaxLevel = map.mapMaxLevel.ToString();
				int finishedLevel = 1;
				double bestTime = 0;

				PlayerMapRecordedModel playerRecorded = allPlayerRecorded.Find(p => p.mapID == map.mapID);

				if (playerRecorded != null)
				{
					finishedLevel = playerRecorded.finishedLevel;
					bestTime = playerRecorded.bestTime.TotalMilliseconds;
				}

				MapEntry bestRecorded = new MapEntry(mapID, mapName, mapMaxLevel, finishedLevel.ToString(), bestTime);

				RunAttempt lastRunAttempt = GameManager.Instance.ActiveRunAttemp;

				SQLiteHelper.INSTANCE.OpenDatabase(); // <<<<<<<<<<<<<<< OPEN DB

				if (lastRunAttempt.FinishedLevel > int.Parse(bestRecorded.finishedLevel))
				{
					//OK last run attempt is better the best record, save it
					SQLiteHelper.INSTANCE.InsertDataToPlayerMapTable(new PlayerMapRecordedModel(mapID, lastRunAttempt.FinishedLevel, lastRunAttempt.TotalTimeCost));
					SQLiteHelper.INSTANCE.UpdatePlayerMapRecord(new PlayerMapRecordedModel(mapID, lastRunAttempt.FinishedLevel, lastRunAttempt.TotalTimeCost));
				}
				else if (lastRunAttempt.FinishedLevel == int.Parse(bestRecorded.finishedLevel))
				{
					//OK last run attempt finished level is equal the best record
					//Let's check the time cost
					if(lastRunAttempt.TotalTimeCost.TotalMilliseconds < bestRecorded.bestTime)
					{
						//Time cost is lower, save it
						SQLiteHelper.INSTANCE.InsertDataToPlayerMapTable(new PlayerMapRecordedModel(mapID, lastRunAttempt.FinishedLevel, lastRunAttempt.TotalTimeCost));
						SQLiteHelper.INSTANCE.UpdatePlayerMapRecord(new PlayerMapRecordedModel(mapID, lastRunAttempt.FinishedLevel, lastRunAttempt.TotalTimeCost));
					}
				}
				else
				{
					//We keep the current best record
				}

				SQLiteHelper.INSTANCE.CloseDatabase(); // <<<<<<<<<<<< CLOSE DB
			}

			break;
		}
	}

	public void GetExtraLife()
    {
		//TODO Show Ads here for extra life
		ShowRewardedVideo();

	}

	// Implement IUnityAdsListener interface methods:
	public void OnUnityAdsReady(string placementId)
	{
		// If the ready Placement is rewarded, activate the button: 
		if (placementId == respawnRewardPlacementID)
		{
			watchAdsButton.GetComponent<Button>().interactable = true;
		}
	}

	public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
	{
		// Define conditional logic for each ad completion status:
		if (showResult == ShowResult.Finished)
		{
			//TODO display respawn button
			if(respawnButton != null)
			{
				respawnButton.SetActive(true);
			}

			if (watchAdsButton != null)
			{
				watchAdsButton.SetActive(false);
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
		if (Advertisement.IsReady(respawnRewardPlacementID))
		{
			Advertisement.Show(respawnRewardPlacementID);
		}
		else
		{
			Debug.Log("Rewarded video is not ready at the moment! Please try again later!");
		}
	}

	public void ResumeWithReloadLevel()
	{
		ReloadMapWithRunAttempt();
	}

	private void ReloadMapWithRunAttempt()
	{
		Time.timeScale = 1f;
		GameManager.Instance.ActiveRunAttemp.IsGameOver = false;
		GameManager.Instance.ActiveRunAttemp.TotalTimeRevival++;
		PlayMusic(true);

		if (GameManager.Instance.ActiveRunAttemp.Map.ToString().Equals(GameManager.Map.DUNGEON.ToString()))
		{
			SceneManager.LoadScene("Dungeon_Level_"+GameManager.Instance.ActiveRunAttemp.FinishedLevel);
		}
	}

	public void OnDestroy()
	{
		Advertisement.RemoveListener(this);
	}
}
