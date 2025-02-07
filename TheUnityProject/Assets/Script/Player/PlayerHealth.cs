using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    // Health
    public float playerMaxHealth = 100;
    public float playerHealth;
    public float healthBarSliderScale = 1;
    public bool playerDied;
    // Stamina
    public float playerMaxStamina = 100;
    public float playerStamina;
    public float staminaBarSliderScale = 1;
    // Objects
    public GameObject deathScreen;
    void Start()
    {
        playerHealth = playerMaxHealth;
        playerStamina = playerMaxStamina;
        playerDied = false;
    }

    void Update()
    {
        healthBarSliderScale = playerHealth / playerMaxHealth;
        staminaBarSliderScale = playerStamina / playerMaxStamina;
        if (playerHealth <= 0)
        {
            deathScreen.SetActive(true);
            playerDied = true;
            Cursor.lockState = CursorLockMode.None;
        }

        if (Input.GetKeyDown(KeyCode.U)) playerHealth++;
        if (Input.GetKeyDown(KeyCode.J)) playerHealth--;
        if (Input.GetKeyDown(KeyCode.I)) playerStamina++;
        if (Input.GetKeyDown(KeyCode.K)) playerStamina--;
    }
}
