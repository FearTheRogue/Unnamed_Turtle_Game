using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public GameObject WinScreen;
    public GameObject LoseScreen;
    public GameObject InfoPanel;

    WaveSpawner waveSpawner;



    // Start is called before the first frame update
    void Start()
    {
        instance = this;

        waveSpawner = GetComponent<WaveSpawner>();

        StartGame();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameObject.FindGameObjectWithTag("item") == null)
        {
            GameLose();
        }
    }

    public void StartGame()
    {
        InfoPanel.SetActive(true);

        Time.timeScale = 0f;
    }

    public void ExitPanel()
    {
        InfoPanel.SetActive(false);

        Time.timeScale = 1f;
    }

    public void GameWin()
    {
        WinScreen.SetActive(true); 
    }

    public void GameLose()
    {
        LoseScreen.SetActive(true);

        waveSpawner.isGameOver = true;
    }
}
