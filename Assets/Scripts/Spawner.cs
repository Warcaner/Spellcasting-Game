using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    private int waveNumber = 0;
    public int enemySpawnAmount = 0;
    public int enemiesKilled = 0;

    public GameObject[] spawners;
    public GameObject enemy;

    void Start()
    {
        spawners = new GameObject[4];

        for(int i = 0; i < spawners.Length; i++)
        {
            spawners[i] = transform.GetChild(i).gameObject;

        }

        StartWave();
    }

    private void Update()
    {
        /*if (enemiesKilled >= enemySpawnAmount)
        {
            NextWave();
        }*/
    
    }


    private void SpawnEnemy()
    {
        int spawnerID = Random.Range(0, spawners.Length);
        Instantiate(enemy, spawners[Random.Range(0,spawners.Length)].transform.position, spawners[Random.Range(0,spawners.Length)].transform.rotation);

    }

    private void StartWave() 
    {
        waveNumber = 1;
        enemySpawnAmount = 2;
        enemiesKilled = 0;

        for (int i = 0; i < enemySpawnAmount; i++)
        {
            SpawnEnemy();

        }

    }


    public void NextWave()
    {
        waveNumber++;
        enemySpawnAmount += 2;
        enemiesKilled = 0;

        for (int i = 0; i < enemySpawnAmount; i++)
        {
            SpawnEnemy();
        }

    }


}  
