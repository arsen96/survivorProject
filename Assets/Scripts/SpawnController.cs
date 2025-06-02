using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class SpawnController : MonoBehaviour
{

    public static event System.Action<int> newLevel;
    public GameObject ennemyHealthContainer;
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
    // [HideInInspector]

    [System.Serializable]
    public class WaveGroup
    {
        public WaveInfo[] waves;
    }
    [SerializeField]
    private List<WaveGroup> waves = new List<WaveGroup>();

    private int currentWave = 0;
    private int levelWaveGroup = 0;
    private float waveCounter;
    [HideInInspector]
    public int nombreDeGroupes = 0;

    private bool isBossHere = false;

    private GameObject currentBoss;

    public delegate void BossDeathHandler();
    public static event BossDeathHandler OnBossDefeated;



     private void HandleBossDeath()
    {
        DestroyEnemies();
        if(waves[levelWaveGroup + 1] != null && waves[levelWaveGroup + 1].waves != null && waves[levelWaveGroup + 1].waves.Length > 0){
            levelWaveGroup++;
            Debug.Log("levelWaveGroup " + levelWaveGroup);
            // Debug.Log("invoked: " + levelWaveGroup);
            newLevel?.Invoke(levelWaveGroup);  
            initLevel();
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
       EnemyHealthContainer.OnBossDeath += HandleBossDeath;
        timeToSpawn = spawnCounter;

        _bossAppearTime = bossAppearTime;

        target = PlayerHealthController.instance.transform;

        despawnDistance = Vector3.Distance(transform.position, maxSpawn.position) + 2f;

        _durationToSpawn = durationToSpawn;
        initLevel();

    }

     void OnDestroy()
    {
        EnemyHealthContainer.OnBossDeath -= HandleBossDeath;
    }

    // Update is called once per frame
    void Update()
    {

        if(stopEnemiesComing == false ){
            if(levelWaveGroup < waves.Count){
                waveCounter -= Time.deltaTime;

                if(waveCounter <= 0){
                    GoToNextWave();         
                }

                if(spawnCounter <= 0 && stopEnemiesComing == false)
                    {
                        spawnCounter = waves[levelWaveGroup].waves[currentWave].timeBetweenSpawns;

                        GameObject newEnemy = Instantiate(waves[levelWaveGroup].waves[currentWave].enemyToSpawn, SpawnPoint(), Quaternion.identity, enemies.transform);
                        if(levelWaveGroup > 0){
                            newEnemy.GetComponent<EnemyController>()._levelMoveSpeedMultiplier = levelWaveGroup * newEnemy.GetComponent<EnemyController>().levelMoveSpeedMultiplier;
                            newEnemy.GetComponent<EnemyController>()._levelDamageMultiplier = levelWaveGroup * newEnemy.GetComponent<EnemyController>().levelDamageMultiplier;
                        }
                        if(newEnemy.gameObject.GetComponent<BossController>() != null){
                            currentBoss = newEnemy;
                        }
                        
                        spawnedEnemies.Add(newEnemy);
                    }



                spawnCounter -= Time.deltaTime;
            }
        }


        transform.position = target.position;
    }

    void FixedUpdate()
    {
    }

    public void initLevel(){
        isBossHere = false;
        stopEnemiesComing = false;
        // spawnedEnemies = new List<GameObject>();
        currentWave = -1;
        spawnCounter = waves[levelWaveGroup].waves[0].timeBetweenSpawns;
    }

    public void DestroyEnemies(){

        for (var i = spawnedEnemies.Count - 1; i >= 0; i--)
        {
                Destroy(spawnedEnemies[i]);
                spawnedEnemies.RemoveAt(i);
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


    public void GoToNextWave()
    {
        currentWave++;
        isBossHere = true;
        if(currentWave >= waves[levelWaveGroup].waves.Length || currentWave >= waves[levelWaveGroup].waves.Length - 1)
        {
            stopEnemiesComing = true;
            isBossHere = true;
        }else {
            waveCounter = waves[levelWaveGroup].waves[currentWave].waveLength;
            spawnCounter = waves[levelWaveGroup].waves[currentWave].timeBetweenSpawns;
        }
    }
}



[System.Serializable]
public class WaveInfo
{
    public GameObject enemyToSpawn;
    public float waveLength = 10f;
    public float timeBetweenSpawns = 1f;
}













