using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager
{
	private static GameManager _instance;

	public int CurrentLevel { get; set; } = 1;

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
}
