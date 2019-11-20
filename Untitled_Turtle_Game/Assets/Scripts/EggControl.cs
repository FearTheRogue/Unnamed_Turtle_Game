using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EggControl : MonoBehaviour
{
    public float startingHealth = 10;
    public float currentHealth;

    public bool atEgg = false;
    public bool isAlive = true;

    private float searchCountdown;

    UI_Slider slider;
    WaveSpawner waveSpawner;

    void Awake()
    {
        slider = GetComponent<UI_Slider>();
    }

    void Start()
    {
        currentHealth = startingHealth;
        slider.maxValue = startingHealth;
        slider.value = startingHealth;

        waveSpawner = GetComponent<WaveSpawner>();
    }

    void Update()
    {
        if (atEgg)
        {
            currentHealth -= Time.deltaTime / 1f;
            currentHealth = Mathf.Clamp(currentHealth, 0, currentHealth);
            TakingEgg();

            UpdateHealthBar(currentHealth);
        }

        //IsEggAlive();
        //Debug.Log(IsEggAlive());
    }

    public void TakingEgg()
    {
        atEgg = true;

        if(currentHealth == 0.0f)
        {
            Destroy(gameObject);
            isAlive = false;
        }
    }

    //public bool IsEggAlive()
    //{
    //    searchCountdown -= Time.deltaTime;

    //    if(searchCountdown <= 0f)
    //    {
    //        searchCountdown = 1f;
    //        if(GameObject.FindGameObjectWithTag("item") == null)
    //        {
    //            GameManager.instance.GameLose();

    //            //return false;
    //        }
    //    }
    //    return true;
    //}

    public void UpdateHealthBar(float val)
    {
        if (isAlive)
        {
            slider.value = val;
            slider.text.text = slider.sliderName + " - " + val.ToString("F1") + " / " + startingHealth;
            slider.fill.color = Color.Lerp(slider.minHealthColor, slider.maxHealthColor, val / startingHealth);

            if(val <= 1)
            {
                slider.text.text = slider.sliderName + " - " + val.ToString("F2") + " / " + startingHealth;
            }
        }

        if (!isAlive)
        {
            slider.text.text = "Deceased";
        }
    }
}
