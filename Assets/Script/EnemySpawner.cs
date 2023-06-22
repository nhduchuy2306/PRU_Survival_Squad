using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyToSpawn;

    public float timeToSpawn;
    private float spawnCounter;

    public Transform minSpawn, maxSpawn;

    private float despawnDistant;

    private List<GameObject> spawnEnemies = new List<GameObject>();

    public int checkPerFrame;
    private int enemyToCheck;

    public List<WaveInfo> waves;

    private int currentWave;
    private float waveCounter;

    private Transform target;

    // Start is called before the first frame update
    void Start()
    {

        target = PlayerController.instance.transform;

        despawnDistant = Vector3.Distance(transform.position, maxSpawn.position) + 4f;

        currentWave = -1;
        GoToNextWave();
    }

    // Update is called once per frame
    void Update()
    {
        this.SpawnEnemies();
    }

    public Vector3 SelectSpawnPoint()
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

    protected virtual void SpawnEnemies()
    {
        /*spawnCounter -= Time.deltaTime;
        if (spawnCounter <= 0)
        {
            spawnCounter = timeToSpawn;

            *//*Instantiate(enemyToSpawn, transform.position, transform.rotation);*//*

            GameObject newEnemy = Instantiate(enemyToSpawn, SelectSpawnPoint(), transform.rotation);

            spawnEnemies.Add(newEnemy);
        }*/

        if(PlayerHealthController.instance.gameObject.activeSelf)
        {
            if(currentWave < waves.Count)
            {
                waveCounter -= Time.deltaTime;
                if(waveCounter <= 0)
                {
                    GoToNextWave();
                }

                spawnCounter -= Time.deltaTime;
                if(spawnCounter <= 0)
                {
                    spawnCounter = waves[currentWave].timeBetweenSpawn;
                    GameObject newEnemy = Instantiate(waves[currentWave].enemyToSpawn, SelectSpawnPoint(), Quaternion.identity);
                    spawnEnemies.Add(newEnemy);
                }
            }
        }

        transform.position = target.position;

        int checkTarget = enemyToCheck + checkPerFrame;

        while (enemyToCheck < checkTarget)
        {
            if (enemyToCheck < spawnEnemies.Count)
            {
                if (spawnEnemies[enemyToCheck] != null)
                {
                    if (Vector3.Distance(transform.position, spawnEnemies[enemyToCheck].transform.position) > despawnDistant)
                    {
                        Destroy(spawnEnemies[enemyToCheck]);

                        spawnEnemies.RemoveAt(enemyToCheck);
                        checkTarget--;
                    }
                    else
                    {
                        enemyToCheck++;
                    }

                }
                else
                {
                    spawnEnemies.RemoveAt(enemyToCheck);
                    checkTarget--;
                }
            }
            else
            {
                enemyToCheck = 0;
                checkTarget = 0;
            }
        }
    }

    public void GoToNextWave()
    {
        currentWave++;

        if(currentWave >= waves.Count) {
         currentWave = waves.Count - 1;
        }

        waveCounter = waves[currentWave].waveLength;
        spawnCounter = waves[currentWave].timeBetweenSpawn;
    }
}

[System.Serializable]
public class WaveInfo
{
    public GameObject enemyToSpawn;
    public float waveLength = 10f;
    public float timeBetweenSpawn =1f;
}