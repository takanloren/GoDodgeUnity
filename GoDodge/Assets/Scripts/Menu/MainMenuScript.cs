using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{

    void Awake()
    {
        SQLiteHelper.INSTANCE.OpenDatabase(); // <<<<< OPEN

        //SQLiteHelper.INSTANCE.DeleteTable(Constants.MAP_TABLE);
        //SQLiteHelper.INSTANCE.DeleteTable(Constants.PLAYER_MAP_DATA_TABLE);
        //SQLiteHelper.INSTANCE.DeleteTable(Constants.PLAYER_TABLE);
        SQLiteHelper.INSTANCE.InitAllTables();
        SQLiteHelper.INSTANCE.InitMapDataTable();

        SQLiteHelper.INSTANCE.CloseDatabase(); // <<<<< CLOSE
    }

    void Start()
    {
        Debug.Log("MainMenuScript Start");
        GameObject.FindGameObjectWithTag("MusicGamePlay").GetComponent<MusicClass>().PlayMusic();

        FirebaseLeaderboardHandler.Instance.SetupDBReference();
    }

	public void PlayGame()
	{
        GameManager.Instance.StartMap(GameManager.Map.MapManager);
	}

    public void OpenLeaderboard()
    {
        GameManager.Instance.StartMap(GameManager.Map.Leaderboard);
    }
}
