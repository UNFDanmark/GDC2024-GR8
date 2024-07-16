using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = System.Random;

public class ShotgunTrigger : MonoBehaviour
{
    public int BS_BulletCount = 8;
    public float BS_Range = 0.3f;
    int BS_remainingAmmo = 2;
    public float reloadSpeed = 2; // In seconds
    bool isReloading = false;
    float timer;
    float lastTimerTrigger = 0;
    
    public GameObject BS_BulletHitParticle;
    public GameObject BS_ShotgunBlastObject;
    
    public bool PLAYSOUND_BS_ShotgunShoot; // Note til Adam: Denne her bool vil blive sat til true i ÉN frame, og kun én, hver gang spilleren skydder.
    public bool PLAYSOUND_ShotgunReload;

    void Start()
    {
    }

    void Update()
    {
        PLAYSOUND_BS_ShotgunShoot = false;
        PLAYSOUND_ShotgunReload = false;
        
        timer += Time.deltaTime;
        
        if (Input.GetKeyDown(KeyCode.R))
        {
            if (!isReloading)
            {
                isReloading = true;
                Reload();
            }
        }
        if (Input.GetKeyDown(KeyCode.Mouse0) && timer >= lastTimerTrigger + 0.2f)
        {
            BuckshotShoot();
        }
        /*
        else if (timer >= lastTimerTrigger + 0.15f)
        {
            BS_ShotgunBlastObject.SetActive(false);
        }
        */
    }
    void Reload()
    {
        StartCoroutine("ResetReload");
    }

    IEnumerator ResetReload()
    {
        PLAYSOUND_ShotgunReload = true;
        yield return new WaitForSeconds(reloadSpeed);
        isReloading = false;
        BS_remainingAmmo = 2;
    }

    public void BuckshotShoot()
    {
        // Shotgun shoots here
        lastTimerTrigger = timer;
        if (BS_remainingAmmo > 0)
        {
            PLAYSOUND_BS_ShotgunShoot = true;
            Instantiate(BS_ShotgunBlastObject, transform.position + (transform.forward * transform.localScale.z),Quaternion.LookRotation(transform.forward,transform.up));
            BS_remainingAmmo--;
            for (int i = 0; i < BS_BulletCount; i++)
            {
                Vector3 spreadVector = transform.right * UnityEngine.Random.Range(-0.35f, 0.35f) + transform.up * UnityEngine.Random.Range(-0.35f, 0.35f);
                Vector3 bulletDirection = transform.forward + spreadVector;
                RaycastHit hit;
                if (Physics.Raycast(transform.position + (transform.forward * transform.localScale.z) / 1.5f, bulletDirection, out hit, BS_Range)) // For origin, it's set to be right in front of the shotgun
                {
                    Debug.DrawRay(transform.position + (transform.forward * transform.localScale.z) / 1.5f, bulletDirection, Color.yellow,1);
                    Instantiate(BS_BulletHitParticle, hit.collider.ClosestPoint(hit.point),new Quaternion(hit.normal.x,hit.normal.y,hit.normal.z,hit.collider.gameObject.transform.rotation.w),hit.collider.gameObject.transform); //Set parent to be enemy
                    if (hit.collider.gameObject.CompareTag("Enemy"))
                    {
                        EnemyCore enemy = hit.collider.gameObject.GetComponent<EnemyCore>();
                        enemy.currentHealth--;
                        //enemy.gameObject.GetComponent<MeshRenderer>().material.color = new Color(1f,0.5f,0.5f);
                        List<Material> enemyMaterials = enemy.gameObject.GetComponent<EnemyCore>().enemyMATS;
                        for (int j = 0; j > enemyMaterials.Count; j++)
                        {
                            enemyMaterials[i].color = new Color(1f,0.5f,0.5f);
                        }
                    }
                }
            }
        }
        else
        {
            if (!isReloading)
            {
                isReloading = true;
                Reload();
            }
        }
    }
}
