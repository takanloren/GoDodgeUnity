using Firebase.Database;
using Firebase.Extensions;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Leaderboard : MonoBehaviour
{
    public Text MapName;
    private Transform entryContainer;
    private Transform entryTemplate;
    private List<Transform> leaderboardTransList = new List<Transform>();

    private void Awake()
    {
        entryContainer = transform.Find("EntryContainer");
        entryTemplate = entryContainer.Find("Entry");

        entryTemplate.gameObject.SetActive(false);
        Debug.Log("Awake leaderboard");
    }

    public void ShowDungeonRank()
    {
        ClearTransformEntryList();
        GetAndShowRankByMap(GameManager.Map.DUNGEON);
    }

    private void ClearTransformEntryList()
    {
        foreach (var trans in leaderboardTransList)
        {
            Destroy(trans.gameObject);
        }

        leaderboardTransList.Clear();
    }

    private void GetAndShowRankByMap(GameManager.Map targetMap)
    {
        MapName.text = targetMap.ToString();

        FirebaseDatabase.DefaultInstance
        .GetReference("Leaderboard")
        .GetValueAsync().ContinueWithOnMainThread(task =>
        {
            if (task.IsFaulted)
            {
                // Handle the error...
            }
            else if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;
                foreach (DataSnapshot map in snapshot.Children)
                {
                    //Check for the map we want to get
                    if (targetMap.ToString().Equals(map.Key))
                    {
                        Debug.Log($"We are in {map.Key} - Total user: {map.ChildrenCount}");
                        foreach (DataSnapshot playerScore in map.Children)
                        {
                            LeaderboardEntry playerScoreEntry = JsonUtility.FromJson<LeaderboardEntry>(playerScore.GetRawJsonValue());
                            Debug.Log($"Received data: {playerScoreEntry.ToString()}");

                            CreateLeaderboardEntryTransform(playerScoreEntry, entryContainer, leaderboardTransList);
                        }
                    }
                }
            }
        });
        Debug.Log("End of getting rank");   
    }

    void Update()
    {
       
    }

    private string RankConverter(int index)
    {
        switch (index)
        {
            case 1:
                return "1st";
            case 2:
                return "2nd";
            case 3:
                return "3rd";
            default:
                return index + "th";
        }
    }

    private void CreateLeaderboardEntryTransform(LeaderboardEntry entry, Transform container, List<Transform> tranformList)
    {
        try
        {
            float entryUIHeight = 62f;
            Transform entryTrans = Instantiate(entryTemplate, entryContainer);
            RectTransform entryRectTrans = entryTrans.GetComponent<RectTransform>();
            entryRectTrans.anchoredPosition = new Vector2(0, -(entryUIHeight * tranformList.Count));
            entryTrans.gameObject.SetActive(true);

            string rank = RankConverter(tranformList.Count + 1);
            entryTrans.Find("Pos").GetComponent<Text>().text = rank;
            entryTrans.Find("Name").GetComponent<Text>().text = entry.name;
            entryTrans.Find("Level").GetComponent<Text>().text = entry.level.ToString();
            entryTrans.Find("TotalTime").GetComponent<Text>().text = FirebaseLeaderboardHandler.Instance.TimeSpanToString(entry.totalTime);

            tranformList.Add(entryTrans);
        }
        catch(Exception ex)
        {
            Debug.Log($"ERROR! {ex.Message}");
        }
       
    }
}