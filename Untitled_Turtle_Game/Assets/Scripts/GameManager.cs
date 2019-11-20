using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public GameObject WinScreen;
    public GameObject LoseScreen;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GameWin()
    {
        WinScreen.SetActive(true); 
    }

    public void GameLose()
    {
        LoseScreen.SetActive(true);
    }
}
