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

    // renomme la variable plus cours c le temps d'apparition de boss apres le dernier enemy apparu  
    public float bossAppearTime = 3f;
    private float _bossAppearTime;

    private float _durationToSpawn;

    private List<GameObject> spawnedEnemies = new List<GameObject>();

    private bool isDestroying = false;

    private bool stopEnemiesComing = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        timeToSpawn = spawnCounter;

        _bossAppearTime = bossAppearTime;

        target = PlayerHealthController.instance.transform;

        despawnDistance = Vector3.Distance(transform.position, maxSpawn.position) + 2f;

        _durationToSpawn = durationToSpawn;
    }

    // Update is called once per frame
    void Update()
    {
        if(stopEnemiesComing == false){
            spawnCounter -= Time.deltaTime;
            if (spawnCounter < 0 && _durationToSpawn > 0)
            {
                spawnCounter = timeToSpawn;
                // Debug.Log("enemyyy " + enemyWrapperPrefab);
                GameObject newEnemy = Instantiate(enemyWrapperPrefab, SpawnPoint(), transform.rotation, enemies.transform);
                spawnedEnemies.Add(newEnemy);
            }else if(_durationToSpawn < 0){
                _bossAppearTime -= Time.deltaTime;
                // Debug.Log("_bossAppearTime " + _bossAppearTime);
                //  Debug.Log("stopEnemiesComing " + stopEnemiesComing);
                if(_bossAppearTime < 0){
                    GameObject newEnemy = Instantiate(bossWrapperPrefab, SpawnPoint(), transform.rotation,  enemies.transform);
                    spawnedEnemies.Add(newEnemy);
                    stopEnemiesComing = true;
                }
            }
            _durationToSpawn -= Time.deltaTime;
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
}



