using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Slider : MonoBehaviour
{
    [Header("Slider Object")]
    public Slider slider;

    [Header("Slider Color")]
    public Color maxHealthColor;
    public Color minHealthColor;
    
    [Header("Slider Image")]
    public Image fill;

    [Header("Slider Text")]
    public Text text;

    [Header("Slider Name")]
    public string sliderName = "";

    public float maxValue;
    public float value;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        slider.maxValue = maxValue;
        slider.value = value;
    }
}
