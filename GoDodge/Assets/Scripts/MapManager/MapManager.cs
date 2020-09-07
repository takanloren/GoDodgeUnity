using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MapManager : MonoBehaviour
{
    private Transform entryContainer;
    private Transform entryTemplate;
    private List<MapEntry> mapEntrys = new List<MapEntry>();
    private List<Transform> mapEntrysTrans = new List<Transform>();
    // Start is called before the first frame update
    void Start()
    {
        entryContainer = transform.Find("MapContainer");
        entryTemplate = entryContainer.Find("MapButtonEntry");

        entryTemplate.gameObject.SetActive(false);

        SQLiteHelper.INSTANCE.OpenDatabase(); // <<< OPEN DB

        List<MapModel> allMap = SQLiteHelper.INSTANCE.GetAll_MapTable();

        List<PlayerMapRecordedModel> allPlayerRecorded = SQLiteHelper.INSTANCE.GetAll_PlayerMapRecorded();

        SQLiteHelper.INSTANCE.CloseDatabase(); // <<< CLOSE DB

        foreach(var map in allMap)
        {
            string mapName = map.mapName;
            string mapMaxLevel = map.mapMaxLevel.ToString();
            int finishedLevel = 1;
            double bestTime = 0;

            PlayerMapRecordedModel playerRecorded = allPlayerRecorded.Find(p => p.mapID == map.mapID);

            if(playerRecorded != null)
            {
                finishedLevel = playerRecorded.finishedLevel;
                bestTime = playerRecorded.bestTime.TotalMilliseconds;
            }

            MapEntry entry = new MapEntry(mapName, mapMaxLevel, finishedLevel.ToString(), bestTime);
            CreateLeaderboardEntryTransform(entry, entryContainer, mapEntrysTrans);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	public void StartMapDungeon()
	{       
		GameManager.Instance.StartMap(GameManager.Map.DUNGEON);
	}

    public void BackToMainMenu()
    {
        GameManager.Instance.StartMap(GameManager.Map.MainMenu);
    }

    private void CreateLeaderboardEntryTransform(MapEntry entry, Transform container, List<Transform> tranformList)
    {
        try
        {
            float entryUIHeight = 62f;
            Transform entryTrans = Instantiate(entryTemplate, entryContainer);
            RectTransform entryRectTrans = entryTrans.GetComponent<RectTransform>();
            entryRectTrans.anchoredPosition = new Vector2(0, -(entryUIHeight * tranformList.Count));
            entryTrans.gameObject.SetActive(true);
            Debug.Log($"75");
            entryTrans.Find("MapName").GetComponent<Text>().text = entry.mapName;
            Debug.Log($"77");
            entryTrans.Find("TimeValue").GetComponent<Text>().text = FirebaseLeaderboardHandler.Instance.TimeSpanToString(entry.bestTime);
            Debug.Log($"79");
            entryTrans.Find("Highest").GetComponent<Text>().text = entry.finishedLevel;
            Debug.Log($"81");
            entryTrans.Find("Max").GetComponent<Text>().text = entry.maxLevel;
            Debug.Log($"83");
            entryTrans.Find("Avatar").GetComponent<Image>().sprite = Resources.Load<Sprite>("MapAvatar/DungeonMapAvatar");
            tranformList.Add(entryTrans);
        }
        catch (Exception ex)
        {
            Debug.Log($"ERROR! {ex.Message}");
        }

    }
}

internal class MapEntry
{
    public string mapName;
    public string maxLevel;
    public string finishedLevel;
    public double bestTime;

    public MapEntry(string name, string level, string finishedLevel, double bestTime)
    {
        mapName = name;
        maxLevel = level;
        this.finishedLevel = finishedLevel;
        this.bestTime = bestTime;
    }
}
