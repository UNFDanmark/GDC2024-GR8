using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    private Transform spawnArea;
        
        public GameObject spawnedCreature;
        public bool infiniteSpawnAmount = true; //Overrides totalSpawnAmount
        public bool enableSpawner = false;
        public int totalSpawnAmount = 25;
        public float timeBeforeStarting = 0;
        public int amountPerWave = 1;
        public float timeBetweenWaves = 10;
        
        private int amountSpawned;
        private float timer;
        private float lastTimerChecked = 0;
    
        private Vector3 corner1, corner2;
        // Start is called before the first frame update
        void Start()
        {
            
            spawnArea = gameObject.GetComponent<Transform>();
            Vector3 position = spawnArea.position;
            Vector3 scale = spawnArea.localScale;
            
            corner1.z = position.z + scale.z/2;
            corner1.x = position.x + scale.x/2;
            
            corner2.z = position.z - scale.z/2;
            corner2.x = position.x - scale.x/2;
        }
    
        // Update is called once per frame
        void Update()
        {
            if (enableSpawner) timer += Time.deltaTime;
    
            if (timer > timeBeforeStarting)
            {
                if (timer >= lastTimerChecked + timeBetweenWaves)
                {
                    lastTimerChecked = timer;
                    DoSpawnCycle();
                }
            }
        }
    
        void DoSpawnCycle()
        {
            for (int i = 0; i < amountPerWave; i++)
            {
                if (amountSpawned < totalSpawnAmount || infiniteSpawnAmount)
                {
                    SpawnEnemy();
                    amountSpawned++;
                }
            }
        }
        void SpawnEnemy()
        {
            Vector3 spawnPoint;
            spawnPoint.y = 0;
    
            spawnPoint.x = UnityEngine.Random.Range(corner1.x, corner2.x);
            spawnPoint.z = UnityEngine.Random.Range(corner1.z, corner2.z);
            Instantiate(spawnedCreature,spawnPoint,Quaternion.identity);
        }
}
