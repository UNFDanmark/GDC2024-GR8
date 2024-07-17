using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyWeapon : MonoBehaviour
{
    public float damage;
    public EnemyCore enemyScript;
    public bool isFireBall = false;
    void Awake()
    {
        if (!isFireBall)
        {
            damage = enemyScript.damage + Random.Range(-enemyScript.damage/4,enemyScript.damage/4);

        }
    }

    void Update()
    {
        
    }

    void OnCollisionEnter(Collision other)
    {
        if (isFireBall)
        {
            Destroy(gameObject);
        }
    }
}
