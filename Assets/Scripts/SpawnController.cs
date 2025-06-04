using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class SpawnController : MonoBehaviour
{
    public static event System.Action<int> newLevel;
    public GameObject ennemyHealthContainer;
    public GameObject enemyWrapperPrefab;

    public GameObject LimitYTop;
    public GameObject LimitYBottom;
    public GameObject LimitXLeft;
    public GameObject LimitXRight;
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

    [System.Serializable]
    public class WaveGroup
    {
        public WaveInfo[] waves;
    }
    
    [SerializeField]
    public List<WaveGroup> waves = new List<WaveGroup>();

    private int currentWave = 0;
    private int levelWaveGroup = 0;
    private float waveCounter; // Timer pour la durée de la vague actuelle
    
    [HideInInspector]
    public int nombreDeGroupes = 0;

    private bool isBossHere = false;
    private GameObject currentBoss;
    public PlayerController playerController;


    public delegate void BossDeathHandler();
    public static event BossDeathHandler OnBossDefeated;

    private void HandleBossDeath()
    {
        DestroyEnemies();
        // Passer au groupe de vagues suivant
        if (levelWaveGroup + 1 < waves.Count)
        {
            levelWaveGroup++;
            Debug.Log("Nouveau niveau - levelWaveGroup: " + levelWaveGroup);
            newLevel?.Invoke(levelWaveGroup);
            initLevel();
        }
        else
        {
            Debug.Log("Tous les niveaux terminés !");
            stopEnemiesComing = true;
        }
    }

    void Start()
    {
        EnemyHealthContainer.OnBossDeath += HandleBossDeath;
        timeToSpawn = spawnCounter;
        _bossAppearTime = bossAppearTime;
        target = PlayerHealthController.instance.transform;
        despawnDistance = Vector3.Distance(transform.position, maxSpawn.position) + 2f;
        _durationToSpawn = durationToSpawn;

        if (PlayerPrefs.HasKey("levelIndex"))
        {
            int levelIndex = PlayerPrefs.GetInt("levelIndex");
            if(!PlayerPrefs.HasKey("highestLevelDone")){
                PlayerPrefs.SetInt("highestLevelDone", levelIndex);
            }else{
                int highestLevelDone = PlayerPrefs.GetInt("highestLevelDone");
                if(levelIndex > highestLevelDone){
                    PlayerPrefs.SetInt("highestLevelDone", levelIndex);
                }
            }
            LoadLevel(levelIndex);
        }
        initLevel();
    }

    void OnDestroy()
    {
        EnemyHealthContainer.OnBossDeath -= HandleBossDeath;
    }

    void Update()
    {
        if (!stopEnemiesComing && levelWaveGroup < waves.Count)
        {
            waveCounter -= Time.deltaTime;
            
            if (waveCounter <= 0)
            {
                GoToNextWave();
            }
            
            if (spawnCounter <= 0 && currentWave < waves[levelWaveGroup].waves.Length)
            {
                SpawnEnemy();
            }
            
            spawnCounter -= Time.deltaTime;
        }

        transform.position = target.position;
    }

    private void SpawnEnemy()
    {
        // Reset du compteur de spawn
        spawnCounter = waves[levelWaveGroup].waves[currentWave].timeBetweenSpawns;
        
        GameObject newEnemy = Instantiate(
            waves[levelWaveGroup].waves[currentWave].enemyToSpawn, 
            SpawnPoint(), 
            Quaternion.identity, 
            enemies.transform
        );
        
        if (levelWaveGroup > 0)
        {
            EnemyController enemyController = newEnemy.GetComponent<EnemyController>();
            if (enemyController != null)
            {
                enemyController._levelMoveSpeedMultiplier = levelWaveGroup * enemyController.levelMoveSpeedMultiplier;
                enemyController._levelDamageMultiplier = levelWaveGroup * enemyController.levelDamageMultiplier;
            }
        }
        
        if (newEnemy.GetComponent<BossFireController>() != null)
        {
            currentBoss = newEnemy;
            isBossHere = true;
        }
        
        spawnedEnemies.Add(newEnemy);
    }



        // // Adjust spawn point if it exceeds Y limits
        // if (spawnPoint.y > LimitYTop.transform.position.y)
        // {
        //     spawnPoint.y = LimitYTop.transform.position.y;
        // }
        // else if (spawnPoint.y < LimitYBottom.transform.position.y)
        // {
        //     spawnPoint.y = LimitYBottom.transform.position.y;
        // }


    public List<int> getUntilDoneLevelIndex()
    {
        List<int> untilDoneLevelIndex = new List<int>();
        for (int i = 0; i < levelWaveGroup; i++)
        {
            untilDoneLevelIndex.Add(i);
        }

        return untilDoneLevelIndex;
    }

    public void initLevel()
    {
        isBossHere = false;
        stopEnemiesComing = false;
        currentWave = 0; 
        if (waves[levelWaveGroup].waves.Length > 0)
        {
            waveCounter = waves[levelWaveGroup].waves[currentWave].waveLength;
            spawnCounter = waves[levelWaveGroup].waves[currentWave].timeBetweenSpawns;
        }
        
    }

    public void DestroyEnemies()
    {
        for (var i = spawnedEnemies.Count - 1; i >= 0; i--)
        {
            if (spawnedEnemies[i] != null)
            {
                Destroy(spawnedEnemies[i]);
            }
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


       if (spawnPoint.y > LimitYTop.transform.position.y)
        {
            spawnPoint.y = LimitYTop.transform.position.y - 3f;
        }
        else if (spawnPoint.y < LimitYBottom.transform.position.y)
        {
            spawnPoint.y = LimitYBottom.transform.position.y + 3f;
        }



       if (spawnPoint.x > LimitXRight.transform.position.x)
        {
            spawnPoint.x = LimitXRight.transform.position.x - 3f;
        }
        else if (spawnPoint.x < LimitXLeft.transform.position.x)
        {
            spawnPoint.x = LimitXLeft.transform.position.x + 3f;
        }


        return spawnPoint;
    }


    

    public void GoToNextWave()
    {
        currentWave++;
        
        if (currentWave >= waves[levelWaveGroup].waves.Length)
        {
            
            if (levelWaveGroup + 1 < waves.Count)
            {
                // levelWaveGroup++;
                // newLevel?.Invoke(levelWaveGroup);
                // initLevel();
            }
            else
            {
                stopEnemiesComing = true;
                isBossHere = true; // Peut-être spawner un boss final
            }
        }
        else
        {
            waveCounter = waves[levelWaveGroup].waves[currentWave].waveLength;
            spawnCounter = waves[levelWaveGroup].waves[currentWave].timeBetweenSpawns;
            // Debug.Log($"Passage à la vague {currentWave} du groupe {levelWaveGroup}");
        }
    }




     public void RestartLevel()
    {
        // if (shouldAdvanceToNextLevel)
        // {
        //     // Passer au niveau suivant
        //     levelWaveGroup++;
        //     Debug.Log("Passage au niveau suivant - levelWaveGroup: " + levelWaveGroup);
        //     newLevel?.Invoke(levelWaveGroup);
        //     shouldAdvanceToNextLevel = false;
        // }
        GameObject endGame = GameObject.Find("endGame");
        endGame.SetActive(false);
        initLevel();
    }

    public int GetCurrentLevelWaveGroup()
    {
        return levelWaveGroup;
    }

    public void LoadLevel(int levelIndex)
    {
        levelWaveGroup = levelIndex;
        // Debug.Log("levelIndex Here: " + levelIndex);
        // PlayerPrefs.DeleteKey("levelIndex");
    }


       public void GoToNextLevel()
        {
            // Vérifier s'il y a un niveau suivant
            if (levelWaveGroup + 1 < waves.Count)
            {
                levelWaveGroup++;
              
                newLevel?.Invoke(levelWaveGroup);
                initLevel();
            }
            else
            {
                Debug.Log("Pas de niveau suivant disponible !");
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