using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    private PlayerHealth playerHealth;
    public ShotgunTrigger shotgunScript;

    public GameObject
        healthBar,
        staminaBar,
        ammo1Trans,
        ammo1Opaque,
        ammo2Trans,
        ammo2Opaque;

    private Slider healthSlider, staminaSlider;
    
    
    void Start()
    {
        playerHealth = GameObject.FindWithTag("Player").GetComponent<PlayerHealth>();
        
        healthSlider = healthBar.GetComponent<Slider>();
        staminaSlider = staminaBar.GetComponent<Slider>();
    }

    void Update()
    {
        if (shotgunScript.currentSpellRemainingAmmo == 2)
        {
            ammo1Opaque.SetActive(true);
            ammo2Opaque.SetActive(true);
        }
        if (shotgunScript.currentSpellRemainingAmmo == 1)
        {
            ammo1Opaque.SetActive(false);
            ammo2Opaque.SetActive(true);
        }
        if (shotgunScript.currentSpellRemainingAmmo == 0)
        {
            ammo1Opaque.SetActive(false);
            ammo2Opaque.SetActive(false);
        }
        healthSlider.value = playerHealth.healthBarSliderScale;
        staminaSlider.value = playerHealth.staminaBarSliderScale;

        //healthBar.GetComponentInChildren<Image>().color = gradient.Evaluate(playerHealth.healthBarSliderScale);
    }
}
