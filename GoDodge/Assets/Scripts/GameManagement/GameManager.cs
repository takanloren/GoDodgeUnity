using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager
{
	private static GameManager _instance;

	private RunAttempt _activeRunAttempt;
	private PlayerModel _playerEquipment;
    public RunAttempt ActiveRunAttemp
	{
		get
		{
			//Just use for debug and testing
			if(_activeRunAttempt == null)
			{
				_activeRunAttempt = new RunAttempt(Map.DUNGEON, DateTime.Now);
			}

			return _activeRunAttempt;
		}

		set
		{
			_activeRunAttempt = value;
		}
	}
    public PlayerModel PlayerEquipment
	{
		get
		{
			//Just use for debug and testing
			if (_playerEquipment == null)
			{
				_playerEquipment = new PlayerModel(1, 10000, 100, 100);
			}

			return _playerEquipment;
		}

		set
		{
			_playerEquipment = value;
		}
	}

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
			case Map.Shop:
				SceneManager.LoadScene("Shop");
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
        if(ActiveRunAttemp != null)
		{
			ActiveRunAttemp.IsGameOver = false;
		}
    }

	public int CalculateGemEarned(int finishedLevel)
	{
		int gemEarned = 0;
		if(finishedLevel == 1)
		{
			return 0;
		}

		gemEarned = Constants.BASE_GEM_EARN_PER_LEVEL * (finishedLevel - 1);

		//Bonus gem on each 10lv finished - each 10lv bonus 20 gem
		int bonusMultiplier = finishedLevel / 10;
		int bonusGems = bonusMultiplier * 20;

		return gemEarned + bonusGems;
	}

    public async void SetGameOverWithDelayTime(bool state, int time)
    {
        await Task.Delay(time);

		if (ActiveRunAttemp != null)
		{
			ActiveRunAttemp.IsGameOver = state;
		}
	}

	public enum Map
	{
		MainMenu, HowToPlay, Leaderboard, MapManager, Shop, DUNGEON, 
	}

    public enum BuffEffects
    {
        OnNormal, OnSpeedPotion, OnShield,
    }

    public enum DebuffEffects
    {
        None, Slow,
    }
}
