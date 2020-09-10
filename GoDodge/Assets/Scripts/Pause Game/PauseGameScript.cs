using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PauseGameScript : MonoBehaviour
{
	public GameObject gamePauseOverlayUI;
	public GameObject gamePauseMenuUI;
	public TextMeshProUGUI highestLevel;
	public TextMeshProUGUI totalTime;

	// Start is called before the first frame update
	void Start()
    {
		gamePauseOverlayUI.SetActive(false);
		gamePauseMenuUI.SetActive(false);
	}

    // Update is called once per frame
    void Update()
    {
        
    }
	
	public void PauseGame()
	{
		DisplayPauseGameWindow();
	}

	public void ResumeGame()
	{
		gamePauseOverlayUI.SetActive(false);
		gamePauseMenuUI.SetActive(false);

		Time.timeScale = 1f;
	}


	public void BackToMapManager()
	{
		GameManager.Instance.ResetAll();
		Time.timeScale = 1f;

		GameManager.Instance.StartMap(GameManager.Map.MapManager);
	}

	public void RestartMap()
	{
		GameManager.Instance.ResetAll();
		Time.timeScale = 1f;

		GameManager.Instance.StartMap(GameManager.Instance.ActiveRunAttemp.Map);
	}

	public void DisplayPauseGameWindow()
	{
		gamePauseOverlayUI.SetActive(true);
		gamePauseMenuUI.SetActive(true);

		highestLevel.text = GameManager.Instance.ActiveRunAttemp.FinishedLevel.ToString();
		totalTime.text = CalculateTotalTime();

		Time.timeScale = 0f;
	}

	private string CalculateTotalTime()
	{
		//set the end time for run attempt
		GameManager.Instance.ActiveRunAttemp.EndTime = DateTime.Now;

		var totalTimeSpan = GameManager.Instance.ActiveRunAttemp.TotalTimeCost;

		string totalTimeText = FirebaseLeaderboardHandler.Instance.TimeSpanToString(totalTimeSpan.TotalMilliseconds);

		return totalTimeText;
	}
}
