using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public GameObject WinScreen;
    public GameObject LoseScreen;

    WaveSpawner waveSpawner;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;

        waveSpawner = GetComponent<WaveSpawner>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameObject.FindGameObjectWithTag("item") == null)
        {
            GameLose();
        }
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
