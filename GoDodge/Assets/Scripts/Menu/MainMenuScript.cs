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

        //ClearDB();
        SQLiteHelper.INSTANCE.InitAllTables();
        SQLiteHelper.INSTANCE.InitMapDataTable();
        InitPlayerEquipment();

        SQLiteHelper.INSTANCE.CloseDatabase(); // <<<<< CLOSE
    }

    private void InitPlayerEquipment()
    {
        if (SQLiteHelper.INSTANCE.LoadPlayerEquipment() == null)
        {
            //Only init the first time
            SQLiteHelper.INSTANCE.InitPlayerEquipment();
        }

        //GameManager.Instance.PlayerEquipment = SQLiteHelper.INSTANCE.LoadPlayerEquipment();

        PlayerModel GM = new PlayerModel(10000, 100, 100);
        GameManager.Instance.PlayerEquipment = GM;
    }

    private void ClearDB()
    {
        SQLiteHelper.INSTANCE.DeleteTable(Constants.MAP_TABLE);
        SQLiteHelper.INSTANCE.DeleteTable(Constants.PLAYER_MAP_DATA_TABLE);
        SQLiteHelper.INSTANCE.DeleteTable(Constants.PLAYER_TABLE);
    }

    void Start()
    {
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
