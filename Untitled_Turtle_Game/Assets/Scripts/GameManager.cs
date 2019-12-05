using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public GameObject WinScreen;
    public GameObject LoseScreen;
    public GameObject InfoPanel;

    public bool isInfoPanelActive = false;

    WaveSpawner waveSpawner;

    //public AudioSource currentMusic;
   // public AudioSource WinningMusic;
    //public AudioSource LostMusic;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;

        waveSpawner = GetComponent<WaveSpawner>();

        StartGame();
    }

    void Update()
    {
        //if (GameObject.FindGameObjectWithTag("item") == null)
        //{
          //  Debug.LogError("No Enemies Found");
            //FindObjectOfType<AudioManager>().Play("Game Over");
       // }
    }

    public void StartGame()
    {
        InfoPanel.SetActive(true);
        isInfoPanelActive = true;

        Time.timeScale = 0f;

        //currentMusic.Play();
    }

    public void ExitPanel()
    {
        InfoPanel.SetActive(false);
        isInfoPanelActive = false;

        Time.timeScale = 1f;
    }

    public void GameWin()
    {
        WinScreen.SetActive(true);
        FindObjectOfType<AudioManager>().Play("Game Win");

        Time.timeScale = 0f;
    }

    public void GameLose()
    {
        LoseScreen.SetActive(true);
        FindObjectOfType<AudioManager>().Play("Game Over");

        waveSpawner.isGameOver = true;
    }
}
