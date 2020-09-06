using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverScript : MonoBehaviour
{
    public GameObject gameOverlayUI;
    public GameObject gameOverMenuUI;
    public TextMeshProUGUI highestLevel;
    public TextMeshProUGUI totalTime;
    public TMP_InputField enterNameField;

    private bool showingGOMenuUI = false;

    // Start is called before the first frame update
    void Start()
    {
        gameOverlayUI.SetActive(false);
        gameOverMenuUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.IsGameOver)
        {
            if (!showingGOMenuUI)
            {
                showingGOMenuUI = true;
                DisplayGameOverWindow();
            }
        }
    }

    public void DisplayGameOverWindow()
    {
		try
		{
			GameObject.FindGameObjectWithTag("MusicGamePlay").GetComponent<MusicClass>().StopMusic();
			GameObject.FindGameObjectWithTag("MusicGameOver").GetComponent<MusicClass>().PlayMusic();
		}
        catch (Exception ex)
		{
            Debug.Log(ex.Message);
		}

        

        gameOverlayUI.SetActive(true);
        gameOverMenuUI.SetActive(true);

        highestLevel.text = GameManager.Instance.ActiveRunAttemp.FinishedLevel.ToString();

        totalTime.text = CalculateTotalTime();

        Time.timeScale = 0f;
    }

    private string CalculateTotalTime()
    {
        //set the end time for run attempt
        GameManager.Instance.ActiveRunAttemp.EndTime = DateTime.Now;

        var totalTimeSpan = GameManager.Instance.ActiveRunAttemp.TotalTimeCost;

        string totalTimeText = "";

        if (totalTimeSpan.TotalMinutes < 1.0)
        {
            totalTimeText = String.Format("{0}s", totalTimeSpan.Seconds);
        }
        else if (totalTimeSpan.TotalHours < 1.0)
        {
            totalTimeText = String.Format("{0}m:{1:D2}s", totalTimeSpan.Minutes, totalTimeSpan.Seconds);
        }
        else // more than 1 hour
        {
            totalTimeText = String.Format("{0}h:{1:D2}m:{2:D2}s", (int)totalTimeSpan.TotalHours, totalTimeSpan.Minutes, totalTimeSpan.Seconds);
        }

        return totalTimeText;
    }

    public void OK()
    {
        GameManager.Instance.ResetAll();
        showingGOMenuUI = false;
        Time.timeScale = 1f;

        GameObject.FindGameObjectWithTag("MusicGameOver").GetComponent<MusicClass>().StopMusic();
        GameObject.FindGameObjectWithTag("MusicGamePlay").GetComponent<MusicClass>().PlayMusic();

        SceneManager.LoadScene("Menu");
    }

	public void BackToMapSelection()
	{
		GameManager.Instance.ResetAll();
		showingGOMenuUI = false;
		Time.timeScale = 1f;

		GameObject.FindGameObjectWithTag("MusicGameOver").GetComponent<MusicClass>().StopMusic();
		GameObject.FindGameObjectWithTag("MusicGamePlay").GetComponent<MusicClass>().PlayMusic();

		SceneManager.LoadScene("MapManager");
	}

    public void SubmitScore()
    {
        string name = enterNameField.text;
        int lvel = GameManager.Instance.ActiveRunAttemp.FinishedLevel;
        double time = GameManager.Instance.ActiveRunAttemp.TotalTimeCost.TotalMilliseconds;
        string map = GameManager.Instance.ActiveRunAttemp.Map.ToString();

        LeaderboardEntry entry = new LeaderboardEntry(name, map, lvel, time);

        Debug.Log($"Submitted entry = {entry.ToString()}");

        FirebaseLeaderboardHandler.Instance.SubmitScore(entry);

        enterNameField.gameObject.SetActive(false);
    }

	public void PlayAgain()
	{
		GameManager.Instance.ResetAll();
		showingGOMenuUI = false;
		Time.timeScale = 1f;

		GameObject.FindGameObjectWithTag("MusicGameOver").GetComponent<MusicClass>().StopMusic();
		GameObject.FindGameObjectWithTag("MusicGamePlay").GetComponent<MusicClass>().PlayMusic();

		GameManager.Instance.StartMap(GameManager.Instance.ActiveRunAttemp.Map);
	}

	public void GetExtraLife()
    {
        //TODO Show Ads here for extra life
    }
}
