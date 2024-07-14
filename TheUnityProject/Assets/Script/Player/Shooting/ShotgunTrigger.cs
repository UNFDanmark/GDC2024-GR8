using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class ShotgunTrigger : MonoBehaviour
{
    public GameObject BS_BulletPrefab;
    public int BS_BulletCount = 8;
    public float BS_Range = 0.3f;
    int BS_remainingAmmo = 2;
    public float reloadSpeed = 2; // In seconds
    bool isReloading = false;
    float timer;
    float lastTimerTrigger = 0;

    GameObject shotgunObject;
    Material shotgunMaterial;
    Color defaultColor;

    void Start()
    {
        shotgunObject = GameObject.FindGameObjectWithTag("Shotgun");
        shotgunMaterial = shotgunObject.GetComponent<MeshRenderer>().material;
        defaultColor = shotgunMaterial.color;
    }

    void Update()
    {
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
            /*
            // Shotgun shoots here
            lastTimerTrigger = timer;
            if (BS_remainingAmmo > 0)
            {
                BS_remainingAmmo--;
                for (int i = 0; i < BS_BulletCount; i++)
                {
                    GameObject bulletClone = Instantiate(BS_BulletPrefab,
                        transform.position + (transform.forward * transform.localScale.z)/2, 
                        Quaternion.identity);

                    Vector3 spreadVector = transform.right * UnityEngine.Random.Range(-50.0f, 50.0f) + transform.up * UnityEngine.Random.Range(-50.0f, 50.0f);
                    Vector3 bulletDirection = transform.forward * 500;
                    Debug.Log(transform.forward);
                    bulletDirection += spreadVector;
                    Rigidbody bulletRB = bulletClone.GetComponent<Rigidbody>();
                    bulletRB.AddForce(bulletDirection);
                    GameObject.Destroy(bulletClone, BS_Range);
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
            */
            //Debug.DrawRay(transform.position + (transform.forward * transform.localScale.z) / 1.5f, transform.forward + transform.right * UnityEngine.Random.Range(-0.2f, 0.2f) + transform.up * UnityEngine.Random.Range(-0.2f, 0.2f));
            BuckshotShoot();
            /*
            RaycastHit hit;
            Vector3 spreadVector = transform.right * UnityEngine.Random.Range(-0.2f, 0.2f) + transform.up * UnityEngine.Random.Range(-0.2f, 0.2f);
            Vector3 bulletDirection = transform.forward + spreadVector;
            if (Physics.Raycast(transform.position + (transform.forward * transform.localScale.z) / 1.5f, bulletDirection, out hit, BS_Range)) // For origin, it's set to be right in front of the shotgun
            {
                Debug.DrawRay(transform.position + (transform.forward * transform.localScale.z) / 1.5f, bulletDirection, Color.yellow,1);
                Debug.Log("Raycast hit");
            }
            */
        }
    }
    void Reload()
    {
        shotgunMaterial.color = new Color(0,0,0,255);
        StartCoroutine("ResetReload");
    }

    IEnumerator ResetReload()
    {
        Debug.Log("Coroutine trigger");
        yield return new WaitForSeconds(reloadSpeed);
        isReloading = false;
        shotgunMaterial.color = defaultColor;
        BS_remainingAmmo = 2;
    }

    public void BuckshotShoot()
    {
        // Shotgun shoots here
        lastTimerTrigger = timer;
        if (BS_remainingAmmo > 0)
        {
            BS_remainingAmmo--;
            for (int i = 0; i < BS_BulletCount; i++)
            {
                
                Vector3 spreadVector = transform.right * UnityEngine.Random.Range(-0.2f, 0.2f) + transform.up * UnityEngine.Random.Range(-0.2f, 0.2f);
                Vector3 bulletDirection = transform.forward + spreadVector;
                RaycastHit hit;
                if (Physics.Raycast(transform.position + (transform.forward * transform.localScale.z) / 1.5f, bulletDirection, out hit, BS_Range)) // For origin, it's set to be right in front of the shotgun
                {
                    Debug.DrawRay(transform.position + (transform.forward * transform.localScale.z) / 1.5f, bulletDirection, Color.yellow,1);
                    Debug.Log("Raycast hit");
                    if (hit.collider.gameObject.CompareTag("Enemy"))
                    {
                        Debug.Log("Enemy hit");
                        EnemyCore enemy = hit.collider.gameObject.GetComponent<EnemyCore>();
                        enemy.currentHealth--;
                        enemy.gameObject.GetComponent<MeshRenderer>().material.color = new Color(1f,0.5f,0.5f);
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
