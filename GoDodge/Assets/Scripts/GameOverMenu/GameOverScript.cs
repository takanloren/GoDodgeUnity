using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverScript : MonoBehaviour
{
    public GameObject gameOverlayUI;
    public GameObject gameOverMenuUI;
    public TextMeshProUGUI highestLevel;
    public TextMeshProUGUI totalTime;
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
        GameObject.FindGameObjectWithTag("MusicGamePlay").GetComponent<MusicClass>().StopMusic();
        GameObject.FindGameObjectWithTag("MusicGameOver").GetComponent<MusicClass>().PlayMusic();

        gameOverlayUI.SetActive(true);
        gameOverMenuUI.SetActive(true);

        highestLevel.text = GameManager.Instance.CurrentLevel.ToString();
        totalTime.text = CalculateTotalTime();

        Time.timeScale = 0f;
    }

    private string CalculateTotalTime()
    {
        var currentTime = DateTime.Now;

        Debug.Log($"GameStart : {GameManager.Instance.StartGameTime.ToString("MM/dd/yyyy HH:mm:ss")}");
        Debug.Log($"Current Time : {currentTime.ToString("MM/dd/yyyy HH:mm:ss")}");
        var totalTimeSpan = currentTime - GameManager.Instance.StartGameTime;

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

    public void GetExtraLife()
    {
        //TODO Show Ads here for extra life
    }
}
