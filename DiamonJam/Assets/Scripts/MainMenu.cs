﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public AudioClip mainMusic;


    public GameObject mainPanel;
    public GameObject optionsPanel;
    public GameObject creditsPanel;

    private void Start()
    {
        SoundManager.Instance.PlayMusic(mainMusic);
    }

    public void StartGame()
    {
        // Need name of the scene or number
        SceneManager.LoadScene(1);
        SoundManager.Instance.FadeMusic();
        //Debug.Log("StartGame");
    }

    public void OpenMain()
    {
        mainPanel.SetActive(true);
        creditsPanel.SetActive(false);
        optionsPanel.SetActive(false);
    }

    public void OpenOptions()
    {
        mainPanel.SetActive(false);
        creditsPanel.SetActive(false);
        optionsPanel.SetActive(true);
    }

    public void OpenCredits()
    {
        mainPanel.SetActive(false);
        creditsPanel.SetActive(true);
        optionsPanel.SetActive(false);
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
#if UNITY_ANDROID || UNITY_IOS
        Application.Quit();
#endif
    }
}
