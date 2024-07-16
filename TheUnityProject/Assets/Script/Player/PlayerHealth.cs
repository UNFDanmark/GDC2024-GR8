using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    // Health
    public float playerMaxHealth = 20;
    public float playerHealth;
    public float healthBarSliderScale = 1;
    // Stamina
    public float playerMaxStamina = 10;
    public float playerStamina;
    public float staminaBarSliderScale = 1;
    void Start()
    {
        playerHealth = playerMaxHealth;
        playerStamina = playerMaxStamina;
    }

    void Update()
    {
        healthBarSliderScale = playerHealth / playerMaxHealth;
        staminaBarSliderScale = playerStamina / playerMaxStamina;
        Debug.Log(playerHealth);
        if (playerHealth <= 0)
        {
            // Player dies
        }

        if (Input.GetKeyDown(KeyCode.U)) playerHealth++;
        if (Input.GetKeyDown(KeyCode.J)) playerHealth--;
        if (Input.GetKeyDown(KeyCode.I)) playerStamina++;
        if (Input.GetKeyDown(KeyCode.K)) playerStamina--;
    }
}
