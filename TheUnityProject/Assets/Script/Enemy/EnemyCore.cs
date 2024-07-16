using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyCore : MonoBehaviour
{
    
    float timer;
    float lastTimerChecked;
    Killstreak killStreak;
    
    // Loaded objects
    Material enemyMAT;
    Color originalColor;
    NavMeshAgent enemyAI;
    Transform playerTransform;
    public GameObject meleeWeapon;
    public Animator meleeWeaponAnimator;
    AnimatorStateInfo animInfo;
    
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
        killStreak = GameObject.FindWithTag("Player").GetComponent<Killstreak>();
        
        currentHealth = maxHealth;
        enemyAI.speed = speed;
        enemyAI.destination = playerTransform.position;
        animInfo = meleeWeaponAnimator.GetCurrentAnimatorStateInfo(0);
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        
        if (currentHealth <= 0)
        {
            killStreak.Kill();
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
                enemyMAT.color = originalColor;
                doingColourChange = false;
            }
        }
        /*
        if ((playerTransform.position - transform.position).magnitude <= range && animInfo.length < 1)
        {
            Debug.Log("I'm in range!!!!");
            MeleeAttack();
        }
        */
    }
    /*
    void MeleeAttack()
    {
        meleeWeaponAnimator.Play("MeleeAttack");
    }
    */
}
