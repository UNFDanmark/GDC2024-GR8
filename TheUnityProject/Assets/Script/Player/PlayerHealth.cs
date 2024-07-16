using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public float playerHealth = 10;
    void Start()
    {
        
    }

    void Update()
    {
        Debug.Log(playerHealth);
        if (playerHealth <= 0)
        {
            // Player dies
        }
    }
}
