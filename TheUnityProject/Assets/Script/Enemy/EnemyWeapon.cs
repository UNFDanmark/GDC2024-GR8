using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeapon : MonoBehaviour
{
    public float damage;
    public EnemyCore enemyScript;
    void Awake()
    {
        damage = enemyScript.damage + Random.Range(-enemyScript.damage/4,enemyScript.damage/4);
    }

    void Update()
    {
    }
}
