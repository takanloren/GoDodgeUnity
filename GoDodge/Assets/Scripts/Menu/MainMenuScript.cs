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
    }

	public void PlayGame()
	{
        //GameManager.Instance.StartGameTime = DateTime.Now;
		SceneManager.LoadScene("MapManager");
	}
}
