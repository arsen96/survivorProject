using UnityEngine;

public class SpawnController : MonoBehaviour
{
    public GameObject enemies;
    public float spawnCounter;
    private float timeToSpawn;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        timeToSpawn = spawnCounter;
    }

    // Update is called once per frame
    void Update()
    {
        spawnCounter -= Time.deltaTime;
        if (spawnCounter < 0)
        {
            spawnCounter = timeToSpawn;
            Instantiate(enemies, transform.position, transform.rotation);
        }
    }
}
