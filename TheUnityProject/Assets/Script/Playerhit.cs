using UnityEngine;

public class Playerhit : MonoBehaviour
{
    public PlayerHealth playerHealth;
    void Start()
    {
        
    }

    void Update()
    {
    }

    void OnTriggerEnter(Collider other)
    {
        playerHealth.playerHealth -= other.gameObject.GetComponent<EnemyWeapon>().damage;
    }
}
