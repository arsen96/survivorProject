using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class SpawnController : MonoBehaviour
{
    public GameObject enemyWrapperPrefab;
    public GameObject bossWrapperPrefab;
    public float spawnCounter;
    private float timeToSpawn;

    public Transform minSpawn, maxSpawn;
    public GameObject enemies;

    private Transform target;
    private float despawnDistance;

    public float durationToSpawn = 15f;

    public float bossAppearTime = 3f;
    private float _bossAppearTime;

    private float _durationToSpawn;

    private List<GameObject> spawnedEnemies = new List<GameObject>();

    private bool isDestroying = false;

    private bool stopEnemiesComing = false;

    public List<WaveInfo> waves;

    private int currentWave;
    private float waveCounter;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        timeToSpawn = spawnCounter;

        _bossAppearTime = bossAppearTime;

        target = PlayerHealthController.instance.transform;

        despawnDistance = Vector3.Distance(transform.position, maxSpawn.position) + 2f;

        _durationToSpawn = durationToSpawn;
        currentWave = -1;
        // spawnCounter = waves[currentWave].timeBetweenSpawns;
    }

    // Update is called once per frame
    void Update()
    {
        // if(stopEnemiesComing == false){
        //     spawnCounter -= Time.deltaTime;
        //     if (spawnCounter < 0 && _durationToSpawn > 0)
        //     {
        //         spawnCounter = timeToSpawn;
        //         // Debug.Log("enemyyy " + enemyWrapperPrefab);
        //         GameObject newEnemy = Instantiate(enemyWrapperPrefab, SpawnPoint(), transform.rotation, enemies.transform);
        //         spawnedEnemies.Add(newEnemy);
        //     }else if(_durationToSpawn < 0){
        //         _bossAppearTime -= Time.deltaTime;
        //         // Debug.Log("_bossAppearTime " + _bossAppearTime);
        //         //  Debug.Log("stopEnemiesComing " + stopEnemiesComing);
        //         if(_bossAppearTime < 0){
        //             GameObject newEnemy = Instantiate(bossWrapperPrefab, SpawnPoint(), transform.rotation,  enemies.transform);
        //             spawnedEnemies.Add(newEnemy);
        //             stopEnemiesComing = true;
        //         }
        //     }
        //     _durationToSpawn -= Time.deltaTime;
        // }

        if(stopEnemiesComing == false){
            if(currentWave < waves.Count){
                waveCounter -= Time.deltaTime;

                if(waveCounter <= 0){
                    GoToNextWave();
                }

                if(spawnCounter <= 0)
                    {
                        spawnCounter = waves[currentWave].timeBetweenSpawns;
                        GameObject newEnemy = Instantiate(waves[currentWave].enemyToSpawn, SpawnPoint(), Quaternion.identity, enemies.transform);
                        spawnedEnemies.Add(newEnemy);
                    }



                spawnCounter -= Time.deltaTime;
            }
        }

   

        transform.position = target.position;
    }

    void FixedUpdate()
    {
    //   DestroyEnemies();
    }

    public void DestroyEnemies(bool checkDistance = true){

        for (var i = spawnedEnemies.Count - 1; i >= 0; i--)
        {

            if(checkDistance){
                if (Vector3.Distance(transform.position, spawnedEnemies[i].transform.position) > despawnDistance){
                        Destroy(spawnedEnemies[i]);
                        spawnedEnemies.RemoveAt(i);
               }
            }else{
                Destroy(spawnedEnemies[i]);
                spawnedEnemies.RemoveAt(i);
            }
           
        }
    }

    public Vector3 SpawnPoint()
    {
        Vector3 spawnPoint = Vector3.zero;
        bool spawnVerticalEdge = Random.Range(0f, 1f) > .5f;
        if (spawnVerticalEdge)
        {
            spawnPoint.y = Random.Range(minSpawn.position.y, maxSpawn.position.y);
            if (Random.Range(0f, 1f) > .5f)
            {
                spawnPoint.x = maxSpawn.position.x;
            }
            else
            {
                spawnPoint.x = minSpawn.position.x;
            }
        }
        else
        {
            spawnPoint.x = Random.Range(minSpawn.position.x, maxSpawn.position.x);
            if (Random.Range(0f, 1f) > .5f)
            {
                spawnPoint.y = maxSpawn.position.y;
            }
            else
            {
                spawnPoint.y = minSpawn.position.y;
            }
        }

        return spawnPoint;
    }

    // private IEnumerator DestroyEnemiesWithDelay()
    // {
    //     isDestroying = true;
    //     float totalDespawnTime = durationToSpawn - _durationToSpawn;
    //     float delay = totalDespawnTime / spawnedEnemies.Count;

    //     for (var i = spawnedEnemies.Count - 1; i >= 0; i--)
    //     {
    //         Destroy(spawnedEnemies[i]);
    //         spawnedEnemies.RemoveAt(i);
    //         yield return new WaitForSeconds(delay);
    //     }
    // }


 

    public void GoToNextWave()
    {
        currentWave++;
        
        if(currentWave >= waves.Count)
        {
            currentWave = waves.Count - 1;
        }
        
        waveCounter = waves[currentWave].waveLength;
        spawnCounter = waves[currentWave].timeBetweenSpawns;
    }
}



[System.Serializable]
public class WaveInfo
{
    public GameObject enemyToSpawn;
    public float waveLength = 10f;
    public float timeBetweenSpawns = 1f;
}






















    // if(PlayerHealthController.instance.gameObject.activeSelf)
        // {
        //     if(currentWave < waves.Count)
        //     {
        //         waveCounter -= Time.deltaTime;
        //         if(waveCounter <= 0)
        //         {
        //             GoToNextWave();
        //         }
                
        //         spawnCounter -= Time.deltaTime;
        //         if(spawnCounter <= 0)
        //         {
        //             spawnCounter = waves[currentWave].timeBetweenSpawns;
                    
        //             GameObject newEnemy = Instantiate(waves[currentWave].enemyToSpawn, SelectSpawnPoint(), Quaternion.identity);
                    
        //             spawnedEnemies.Add(newEnemy);
        //         }
        //     }
        // }



// Cette classe servira de modèle pour toutes les armes du jeu
// public class Weapon : MonoBehaviour
// {
//     public List<WeaponStats> stats; // stocker les données de chaque niveau d'arme
//     public int weaponLevel;
// }

// [System.Serializable]
// public class WeaponStats
// {
//     public float speed, damage, range, timeBetweenAttacks, amount, duration;
// }

// Toutes les armes partagent des fonctionnalités communes (statistiques, niveaux) sans avoir à réécrire ce code pour chaque arme


// public void SetStats()
// {
//     damager.damageAmount = stats[weaponLevel].damage;  // 
    
//     transform.localScale = Vector3.one * stats[weaponLevel].range;
    
//     timeBetweenSpawn = stats[weaponLevel].timeBetweenAttacks;
    
//     damager.lifetime = stats[weaponLevel].duration;
    
//     spawnCounter = 0f;
// }