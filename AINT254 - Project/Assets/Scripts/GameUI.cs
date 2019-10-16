using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    public static GameUI instance;

    public Text text;

    void Start()
    {
        instance = this;

        text.text = "";
    }

    void Update()
    {
        
    }
}
