using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
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
		try
		{
            entryContainer = transform.Find("MapContainer");
			entryTemplate = entryContainer.Find("MapButtonEntry");

			entryTemplate.gameObject.SetActive(false);

			SQLiteHelper.INSTANCE.OpenDatabase(); // <<< OPEN DB

			List<MapModel> allMap = SQLiteHelper.INSTANCE.GetAll_MapTable();

			List<PlayerMapRecordedModel> allPlayerRecorded = SQLiteHelper.INSTANCE.GetAll_PlayerMapRecorded();

			SQLiteHelper.INSTANCE.CloseDatabase(); // <<< CLOSE DB

			foreach (var map in allMap)
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

				MapEntry entry = new MapEntry(mapID, mapName, mapMaxLevel, finishedLevel.ToString(), bestTime);
				CreateMapEntryTransform(entry, entryContainer, mapEntrysTrans);
			}
		}
		catch (Exception ex)
		{
			Debug.Log(ex.Message);
		}
		
	}

    // Update is called once per frame
    void Update()
    {
        
    }

    public void BackToMainMenu()
    {
        GameManager.Instance.StartMap(GameManager.Map.MainMenu);
    }

	public void GoToShop()
	{
		GameManager.Instance.StartMap(GameManager.Map.Shop);
	}

	private void CreateMapEntryTransform(MapEntry entry, Transform container, List<Transform> tranformList)
    {
        try
        {
            float entryUIHeight = 62f;
            Transform entryTrans = Instantiate(entryTemplate, entryContainer);
            RectTransform entryRectTrans = entryTrans.GetComponent<RectTransform>();
            entryRectTrans.anchoredPosition = new Vector2(0, -(entryUIHeight * tranformList.Count));
            entryTrans.gameObject.SetActive(true);
            entryTrans.Find("MapName").GetComponent<Text>().text = entry.mapName;
            entryTrans.Find("TimeValue").GetComponent<Text>().text = FirebaseLeaderboardHandler.Instance.TimeSpanToString(entry.bestTime);
            entryTrans.Find("Highest").GetComponent<Text>().text = entry.finishedLevel;
            entryTrans.Find("Max").GetComponent<Text>().text = entry.maxLevel;
			entryTrans.GetComponent<Button>().onClick.AddListener(() =>
			{
				StartMap(entry.mapID);
			});

			if (entry.mapName.Equals(GameManager.Map.DUNGEON.ToString()))
			{
				entryTrans.Find("Avatar").GetComponent<Image>().sprite = Resources.Load<Sprite>("MapAvatar/DungeonMapAvatar");
			}
			else
			{
				//TODO: Handle for other map
			}

            tranformList.Add(entryTrans);
        }
        catch (Exception ex)
        {
            Debug.Log($"ERROR! {ex.Message}");
        }

    }

	private void StartMap(int mapID)
	{
		switch (mapID)
		{
			case 1:
				GameManager.Instance.StartMap(GameManager.Map.DUNGEON);
				break;
		}
	}
}

internal class MapEntry
{
	public int mapID;
	public string mapName;
    public string maxLevel;
    public string finishedLevel;
    public double bestTime;

    public MapEntry(int mapID, string name, string level, string finishedLevel, double bestTime)
    {
		this.mapID = mapID;
        mapName = name;
        maxLevel = level;
        this.finishedLevel = finishedLevel;
        this.bestTime = bestTime;
    }
}
