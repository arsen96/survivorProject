using UnityEngine;
using System.Collections.Generic;

public class SpawnController : MonoBehaviour
{
    public GameObject enemyWrapperPrefab;
    public float spawnCounter;
    private float timeToSpawn;

    public Transform minSpawn, maxSpawn;

    private Transform target;
    private float despawnDistance;

    private List<GameObject> spawnedEnemies = new List<GameObject>();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        timeToSpawn = spawnCounter;

        target = PlayerHealthController.instance.transform;

        despawnDistance = Vector3.Distance(transform.position, maxSpawn.position) + 2f;
    }

    // Update is called once per frame
    void Update()
    {
        spawnCounter -= Time.deltaTime;
        if (spawnCounter < 0)
        {
            spawnCounter = timeToSpawn;
            // Debug.Log("enemyyy " + enemyWrapperPrefab);
            GameObject newEnemy = Instantiate(enemyWrapperPrefab, SpawnPoint(), transform.rotation);
            spawnedEnemies.Add(newEnemy);
        }
        transform.position = target.position;
    }

    void FixedUpdate()
    {
    //   DestroyEnemiesByDistance();
    }

    public void DestroyEnemiesByDistance(){

        for (var i = spawnedEnemies.Count - 1; i >= 0; i--)
        {
            if (Vector3.Distance(transform.position, spawnedEnemies[i].transform.position) > despawnDistance)
            {
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
}



