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
        Debug.Log(playerHealth);
        Debug.Log(other);
//        Debug.Log(other.gameObject.GetComponent<EnemyWeapon>().damage);
        playerHealth.playerHealth -= other.gameObject.GetComponent<EnemyWeapon>().damage;
    }
}
