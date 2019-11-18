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

    public Slider eggHealhSlider;
    public Color maxHealthColor = Color.green;
    public Color minHealthColor = Color.red;
    public Image Fill;
    public Text eggText;
    public string name;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = startingHealth;
        eggHealhSlider.maxValue = startingHealth;
        eggHealhSlider.value = startingHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (atEgg)
        {
            Debug.Log("Update being called");
            currentHealth -= Time.deltaTime;
            currentHealth = Mathf.Clamp(currentHealth, 0, currentHealth);
            TakingEgg();

            UpdateHealthBar(currentHealth);
        }
    }

    public void TakingEgg()
    {
        Debug.Log("Taking egg being called");
        atEgg = true;
        Debug.Log(atEgg + name);

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
            eggHealhSlider.value = val;
            eggText.text = name + " - " + Mathf.FloorToInt(val) + " / " + startingHealth;
            Fill.color = Color.Lerp(minHealthColor, maxHealthColor, Mathf.FloorToInt(val / startingHealth));
            if(val <= 1)
            {
                eggText.text = name + " - " + val.ToString("F1") + " / " + startingHealth;
            }
        }
        if (!isAlive)
        {
            eggText.text = "Deceased";
        }
    }
}
