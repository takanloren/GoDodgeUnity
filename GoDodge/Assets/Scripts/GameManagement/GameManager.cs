using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class GameManager
{
	private static GameManager _instance;

	public int CurrentLevel { get; set; } = 1;

    public bool IsGameOver { get; set; } = false;

    public DateTime StartGameTime { get; set; }

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

    public void ResetAll()
    {
        IsGameOver = false;
        CurrentLevel = 1;
    }

    public async void SetGameOverWithDelayTime(bool state, int time)
    {
        await Task.Delay(time);

        IsGameOver = state;
    }
}
