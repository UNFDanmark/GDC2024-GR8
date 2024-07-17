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
    public List<GameObject> enemyModels;
    public List<Material> enemyMATS;
    NavMeshAgent enemyAI;
    Transform playerTransform;
    AnimatorStateInfo animInfo;
    public Animator enemyAnimator;
    Score score;
    
    
    // Added traits
    public float maxHealth = 10;
    public float currentHealth;
    public float speed = 0.1f;
    public float range = 2;
    public float damage = 5;
    public float scoreBaseValue = 20;
    
    bool doingColourChange = false;
    // Start is called before the first frame update
    void Awake()
    {
        // Load the objects
        for (int i = 0; i < enemyModels.Count; i++)
        {
            enemyMATS.Add(enemyModels[i].GetComponent<SkinnedMeshRenderer>().material);
        }
        enemyAI = gameObject.GetComponent<NavMeshAgent>();
        playerTransform = GameObject.FindWithTag("Player").GetComponent<Transform>();
        killStreak = GameObject.FindWithTag("Player").GetComponent<Killstreak>();
        score = GameObject.FindWithTag("ScoreText").GetComponent<Score>();
        
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
            killStreak.Kill();
            score.score += scoreBaseValue * score.scoreMultiplier;
            Destroy(gameObject);
        }
        
        enemyAI.speed = speed;
        if ((playerTransform.position - transform.position).magnitude <= range)
        {
            //transform.forward = new Vector3(playerTransform.position.x - transform.position.x,0,playerTransform.position.z - transform.position.z);
            enemyAnimator.SetTrigger("Attack");
            enemyAnimator.SetBool("IsRunning", false);
        }
        else if ((playerTransform.position - transform.position).magnitude <= range / 3)
        {
            enemyAnimator.SetTrigger("Attack");
            enemyAI.destination = transform.position;
            //transform.forward = (playerTransform.position - transform.position).normalized;
        }
        else
        {
            enemyAI.destination = playerTransform.position;
            enemyAnimator.SetBool("IsRunning", true);
        }

        for (int i = 0; i < enemyMATS.Count; i++)
        {
            if (enemyMATS[i].color != Color.white)
            {
                if (!doingColourChange)
                {
                    lastTimerChecked = timer;
                    doingColourChange = true;
                }
            
                if (timer >= lastTimerChecked + 0.4f)
                {
                    enemyMATS[i].color = Color.white;
                    doingColourChange = false;
                }
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
