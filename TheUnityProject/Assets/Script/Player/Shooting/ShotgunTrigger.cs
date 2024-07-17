using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class ShotgunTrigger : MonoBehaviour
{
    public int BS_BulletCount = 8;
    public float BS_Range = 0.3f;
    public int BS_remainingAmmo = 2;
    public int PL_RemainingAmmo = 2;
    public float BS_DMG = 3;
    public int currentSpellRemainingAmmo = 0;
    public float reloadSpeed = 2; // In seconds
    bool isReloading = false;
    float timer;
    float lastTimerTrigger = 0;
    
    public GameObject BS_BulletHitParticle;
    public GameObject BS_ShotgunBlastObject;
    
    public bool PLAYSOUND_BS_ShotgunShoot; // Note til Adam: Denne her bool vil blive sat til true i ÉN frame, og kun én, hver gang spilleren skydder.
    public bool PLAYSOUND_ShotgunReload;
    public bool PLAYSOUND_PiercingLight;
    public bool PLAYSOUND_HitEnemy;
    List<GameObject> enemyHitList;
    public float PL_damage = 15;
    Vector3 lastHitPosition;
    public GameObject PL_Ray;
    bool PLKeepGoing = true;
    public Animator shotgunReloadAnimator;

    public int selectedSpell;
    // 0 = Buckshot
    // 1 = Piercing Light

    void Start()
    {
        enemyHitList = new List<GameObject>();
    }

    void Update()
    {
        PLAYSOUND_BS_ShotgunShoot = false;
        PLAYSOUND_ShotgunReload = false;
        timer += Time.deltaTime;
        if (selectedSpell == 0)
        {
            currentSpellRemainingAmmo = BS_remainingAmmo;
        }
        else if (selectedSpell == 1)
        {
            currentSpellRemainingAmmo = PL_RemainingAmmo;
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            selectedSpell = 0;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            selectedSpell = 1;
        }
        
        if (Input.GetKeyDown(KeyCode.R))
        {
            if (!isReloading)
            {
                isReloading = true;
                StartCoroutine(ResetReload(selectedSpell));
            }
        }
        if (Input.GetKeyDown(KeyCode.Mouse0) && timer >= lastTimerTrigger + 0.2f)
        {
            if (selectedSpell == 0)
            {
                BuckshotShoot();
            }
            else if (selectedSpell == 1)
            {
                PiercingLightShoot();
            }
        }
        else
        {
            if (selectedSpell == 0 && BS_remainingAmmo == 0)
            {
                BuckshotShoot();
            }
            else if (selectedSpell == 1 && PL_RemainingAmmo == 0)
            {
                PiercingLightShoot();
            }
        }
        /*
        else if (timer >= lastTimerTrigger + 0.15f)
        {
            BS_ShotgunBlastObject.SetActive(false);
        }
        */
    }

    void ResetSounds()
    {
        PLAYSOUND_HitEnemy = false;
        PLAYSOUND_PiercingLight = false;
        PLAYSOUND_ShotgunReload = false;
        PLAYSOUND_BS_ShotgunShoot = false;
    }

    IEnumerator ResetReload(int spell)
    {
        shotgunReloadAnimator.SetTrigger("Reload");
        PLAYSOUND_ShotgunReload = true;
        yield return new WaitForSeconds(reloadSpeed);
        isReloading = false;
        if (spell == 0)
        {
            BS_remainingAmmo = 2;
        }
        else if (spell == 1)
        {
            PL_RemainingAmmo = 2;
        }
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
                Vector3 spreadVector = transform.right * UnityEngine.Random.Range(-0.2f, 0.2f) + transform.up * UnityEngine.Random.Range(-0.2f, 0.2f);
                Vector3 bulletDirection = transform.forward + spreadVector;
                RaycastHit hit;
                if (Physics.Raycast(transform.position + (transform.forward * transform.localScale.z) / 1.5f, bulletDirection, out hit, BS_Range)) // For origin, it's set to be right in front of the shotgun
                {
                    Debug.DrawRay(transform.position + (transform.forward * transform.localScale.z) / 1.5f, bulletDirection, Color.yellow,1);
                    Instantiate(BS_BulletHitParticle, hit.collider.ClosestPoint(hit.point),new Quaternion(hit.normal.x,hit.normal.y,hit.normal.z,hit.collider.gameObject.transform.rotation.w),hit.collider.gameObject.transform); //Set parent to be enemy
                    if (hit.collider.gameObject.CompareTag("Enemy"))
                    {
                        EnemyCore enemy = hit.collider.gameObject.GetComponent<EnemyCore>();
                        enemy.currentHealth -= BS_DMG;
                        PLAYSOUND_HitEnemy = true;
                        //enemy.gameObject.GetComponent<MeshRenderer>().material.color = new Color(1f,0.5f,0.5f);
                        List<Material> enemyMaterials = enemy.gameObject.GetComponent<EnemyCore>().enemyMATS;
                        for (int j = 0; j < enemyMaterials.Count; j++)
                        {
                            enemyMaterials[j].color = new Color(1f,0.5f,0.5f);
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
                StartCoroutine(ResetReload(0));
            }
        }
    }

    public void PiercingLightShoot()
    {
        if (PL_RemainingAmmo > 0)
        {
            PLAYSOUND_PiercingLight = true;
            enemyHitList.Clear();
            lastHitPosition = new Vector3(100000,0,0);
            PL_RemainingAmmo--;
            RaycastHit[] hits;
            hits = Physics.RaycastAll(transform.position + (transform.forward * transform.localScale.z) / 1.5f,
                transform.forward, Mathf.Infinity);
            PLKeepGoing = true;
            for (int i = 0; i < hits.Length; i++)
            {
                if (PLKeepGoing)
                {
                    if (hits[i].collider.CompareTag("Enemy"))
                    {
                        enemyHitList.Add(hits[i].collider.gameObject);
                        lastHitPosition = hits[i].transform.position;
                    }
                    else
                    {
                        lastHitPosition = hits[i].transform.position;
                        PLKeepGoing = false;
                    }
                }
            }

            if (lastHitPosition == new Vector3(100000,0,0))
            {
                lastHitPosition = transform.forward * 100;
            }

            for (int i = 0; i < enemyHitList.Count; i++)
            {
                enemyHitList[i].GetComponent<EnemyCore>().currentHealth -= (PL_damage * 1/i * UnityEngine.Random.Range(0.9f,1.1f));
                PLAYSOUND_HitEnemy = true;
                List<Material> enemyMaterials = enemyHitList[i].gameObject.GetComponent<EnemyCore>().enemyMATS;
                for (int j = 0; j < enemyMaterials.Count; j++)
                {
                    Debug.Log("SHOULD BE RED");
                    enemyMaterials[j].color = new Color(1f,0.5f,0.5f);
                }
            }

            if (lastHitPosition != null)
            {
                float distanceFromShotgunToDisplay = (lastHitPosition - transform.position).magnitude /2;
                Vector3 RayPosition = (transform.forward * distanceFromShotgunToDisplay) + transform.position;
                GameObject PLRay = Instantiate(PL_Ray, RayPosition, UnityEngine.Quaternion.identity);
                PLRay.transform.up = transform.forward;
                Vector3 PLRayScale = PLRay.transform.localScale;
                PLRayScale.y = (lastHitPosition - transform.position).magnitude;
                PLRay.transform.localScale = PLRayScale;
                Debug.Log(RayPosition);
            }
        }
        else
        {
            if (!isReloading)
            {
                isReloading = true;
                StartCoroutine(ResetReload(1));
            }
        }
    }
}