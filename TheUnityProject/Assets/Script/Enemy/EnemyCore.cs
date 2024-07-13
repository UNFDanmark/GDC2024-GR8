using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCore : MonoBehaviour
{
    public int maxHealth = 10;
    public int currentHealth = 10;
    float timer;
    float lastTimerChecked;
    Material enemyMAT;
    Color originalColor;
    bool doingColourChange = false;

    public int speed = 1;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        enemyMAT = gameObject.GetComponent<MeshRenderer>().material;
        originalColor = enemyMAT.color;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        
        if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }

        if (enemyMAT.color != originalColor)
        {
            if (!doingColourChange)
            {
                lastTimerChecked = timer;
                doingColourChange = true;
            }
            
            if (timer >= lastTimerChecked + 0.3f)
            {
                Debug.Log("back  to normal");
                gameObject.GetComponent<MeshRenderer>().material.color = originalColor;
                doingColourChange = false;
            }
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Bullet"))
        {
            lastTimerChecked = timer;
            currentHealth--;
            enemyMAT.color = new Color(1f,0.5f,0.5f);
            
        }
    }
}
