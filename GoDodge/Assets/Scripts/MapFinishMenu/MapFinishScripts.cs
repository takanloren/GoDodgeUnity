using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MapFinishScripts : MonoBehaviour
{
    public GameObject mapFinishOverlayUI;
    public GameObject mapFinishMenuUI;
    public TextMeshProUGUI highestLevel;
    public TextMeshProUGUI totalTime;
    public TMP_InputField enterNameField;
    public TextMeshProUGUI gemEarned;

    private bool showingFinishMenuUI = false;

    // Start is called before the first frame update
    void Start()
    {
        mapFinishOverlayUI.SetActive(false);
        mapFinishMenuUI.SetActive(false);
    }

    private void PlayMusic()
    {
        GameObject.FindGameObjectWithTag("MusicGamePlay").GetComponent<MusicClass>().PlayMusic();
        GameObject.FindGameObjectWithTag("MusicGameOver").GetComponent<MusicClass>().StopMusic();
    }

    // Update is called once per frame
    void Update()
    {
        try
        {
            if (GameManager.Instance.ActiveRunAttemp.IsMapFinished)
            {
                if (!showingFinishMenuUI)
                {
                    showingFinishMenuUI = true;
                    DisplayMapFinishWindow();
                }
            }
        }
        catch (Exception ex)
        {
            Debug.LogError(ex.Message);
        }

    }

    public void DisplayMapFinishWindow()
    {
        Time.timeScale = 0f;

        try
        {
            PlayMusic();
        }
        catch (Exception ex)
        {
            Debug.Log(ex.Message);
        }

        mapFinishOverlayUI.SetActive(true);
        mapFinishMenuUI.SetActive(true);

        highestLevel.text = GameManager.Instance.ActiveRunAttemp.FinishedLevel.ToString();
        totalTime.text = CalculateTotalTime();

        int gemEarnedValue = GameManager.Instance.ActiveRunAttemp.GemEarned;

        gemEarned.text = gemEarnedValue.ToString();

        GameManager.Instance.PlayerEquipment.GemAmount += gemEarnedValue;

        SavePlayerRecord();
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
        showingFinishMenuUI = false;
        Time.timeScale = 1f;

        PlayMusic();

        SceneManager.LoadScene("Menu");
    }

    public void BackToMapSelection()
    {
        GameManager.Instance.ResetAll();
        showingFinishMenuUI = false;
        Time.timeScale = 1f;

        PlayMusic();

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
                    if (lastRunAttempt.TotalTimeCost.TotalMilliseconds < bestRecorded.bestTime)
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

    public void OnDestroy()
    {
       
    }
}
