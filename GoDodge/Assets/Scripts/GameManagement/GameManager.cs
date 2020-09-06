using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager
{
	private static GameManager _instance;

    public bool IsGameOver { get; set; } = false;

    public RunAttempt ActiveRunAttemp;

	public static GameManager Instance
	{
		get
		{
			if (_instance == null)
			{
				_instance = new GameManager();
			}

			return _instance;
		}
	}

	public void StartMap(Map map)
	{
		switch (map)
		{
            case Map.MainMenu:
                SceneManager.LoadScene("Menu");
                break;
            case Map.Leaderboard:
                SceneManager.LoadScene("Highscore");
                break;
            case Map.MapManager:
                SceneManager.LoadScene("MapManager");
                break;
			case Map.DUNGEON:
				StartMapDungeon();
				break;
		}
	}

	private void StartMapDungeon()
	{
        RunAttempt dungeonRunAttempt = new RunAttempt(Map.DUNGEON, DateTime.Now);
        GameManager.Instance.ActiveRunAttemp = dungeonRunAttempt;
        SceneManager.LoadScene("Dungeon_Level_1");
	}

    public void ResetAll()
    {
        IsGameOver = false;
    }

    public async void SetGameOverWithDelayTime(bool state, int time)
    {
        await Task.Delay(time);

        IsGameOver = state;
    }

	public enum Map
	{
		MainMenu, HowToPlay, Leaderboard, MapManager, DUNGEON, 
	}
}
