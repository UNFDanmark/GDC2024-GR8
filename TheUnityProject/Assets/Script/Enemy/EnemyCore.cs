using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyCore : MonoBehaviour
{
    
    float timer;
    float lastTimerChecked;
    
    // Loaded objects
    Material enemyMAT;
    Color originalColor;
    NavMeshAgent enemyAI;
    Transform playerTransform;
    public GameObject meleeWeapon;
    
    // Added traits
    public int maxHealth = 10;
    public int currentHealth;
    public float speed = 0.1f;
    public float range = 2;
    
    bool doingColourChange = false;
    // Start is called before the first frame update
    void Awake()
    {
        
        // Load the objects
        enemyMAT = gameObject.GetComponent<MeshRenderer>().material;
        originalColor = enemyMAT.color;
        enemyAI = gameObject.GetComponent<NavMeshAgent>();
        playerTransform = GameObject.FindWithTag("Player").GetComponent<Transform>();
        
        currentHealth = maxHealth;
        enemyAI.speed = speed;
        enemyAI.destination = playerTransform.position;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        
        if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }
        
        enemyAI.speed = speed;
        enemyAI.destination = playerTransform.position;

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
                enemyMAT.color = originalColor;
                doingColourChange = false;
            }
        }

        if ((playerTransform.position - transform.position).magnitude >= range)
        {
            
        }
    }

    void MeleeAttack()
    {
        
    }
}
