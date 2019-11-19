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

    UI_Slider slider;

    void Awake()
    {
        slider = GetComponent<UI_Slider>();
    }

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = startingHealth;
        slider.maxValue = startingHealth;
        slider.value = startingHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (atEgg)
        {
            currentHealth -= Time.deltaTime / 5f;
            currentHealth = Mathf.Clamp(currentHealth, 0, currentHealth);
            TakingEgg();

            UpdateHealthBar(currentHealth);
        }
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
