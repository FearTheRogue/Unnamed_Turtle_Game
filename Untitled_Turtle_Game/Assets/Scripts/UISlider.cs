using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UISlider : MonoBehaviour
{
    public static UISlider instance;

    public Slider slider;
    private float value;

    public void Start()
    {
        instance = this;
        
    }

    public void changeVel()
    {
        value = slider.value;
        LaunchArcRenderer.instance.velocity = value;
        Debug.Log(value);

        LaunchArcRenderer.instance.RenderArc();
    }

}
