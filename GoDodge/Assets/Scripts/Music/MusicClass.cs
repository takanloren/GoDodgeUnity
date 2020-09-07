using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicClass : MonoBehaviour
{
    private static GameObject instanceGameOn;
    private static GameObject instanceGameOver;
    private AudioSource _audioSource;
    private void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);

        if (instanceGameOn == null && gameObject.tag.Equals("MusicGamePlay"))
        {
            instanceGameOn = gameObject;
            _audioSource = GetComponent<AudioSource>();
        }
        else if (instanceGameOver == null && gameObject.tag.Equals("MusicGameOver"))
        {
            instanceGameOver = gameObject;
            _audioSource = GetComponent<AudioSource>();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlayMusic()
    {
        if (_audioSource.isPlaying) return;
        _audioSource.Play();
    }

    public void StopMusic()
    {
        _audioSource.Stop();
    }
}
