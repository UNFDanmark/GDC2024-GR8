using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    private PlayerHealth playerHealth;
    private GameObject 
        healthBar, 
        staminaBar,
        ammo1,
        ammo2;
    private Slider healthSlider, staminaSlider;
    public Gradient gradient;
    
    void Start()
    {
        playerHealth = GameObject.FindWithTag("Player").GetComponent<PlayerHealth>();
        healthBar = GameObject.FindWithTag("HealthBar");
        staminaBar = GameObject.FindWithTag("StaminaBar");
        ammo1 = GameObject.FindWithTag("ammo1");
        ammo1 = GameObject.FindWithTag("ammo2");
        
        healthSlider = healthBar.GetComponent<Slider>();
        staminaSlider = staminaBar.GetComponent<Slider>();
    }

    void Update()
    {
        healthSlider.value = playerHealth.healthBarSliderScale;
        staminaSlider.value = playerHealth.staminaBarSliderScale;

        //healthBar.GetComponentInChildren<Image>().color = gradient.Evaluate(playerHealth.healthBarSliderScale);
    }
}
