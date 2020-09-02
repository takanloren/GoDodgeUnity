using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverScript : MonoBehaviour
{
    public GameObject gameOverlayUI;
    public GameObject gameOverMenuUI;
    private bool showingGOMenuUI = false;
    // Start is called before the first frame update
    void Start()
    {
        gameOverlayUI.SetActive(false);
        gameOverMenuUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.IsGameOver)
        {
            if (!showingGOMenuUI)
            {
                showingGOMenuUI = true;
                Pause();
            }
        }
    }

    public void Pause()
    {
        gameOverlayUI.SetActive(true);
        gameOverMenuUI.SetActive(true);
        Time.timeScale = 0f;
    }

    public void OK()
    {
        GameManager.Instance.ResetAll();
        showingGOMenuUI = false;
        Time.timeScale = 1f;

        SceneManager.LoadScene("Menu");
    }

    public void GetExtraLife()
    {
        //TODO Show Ads here for extra life
    }
}
