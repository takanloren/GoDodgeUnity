using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
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
