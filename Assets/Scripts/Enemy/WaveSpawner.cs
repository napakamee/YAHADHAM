using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Wave
{
    public string waveName;
    public int noOfEnemies;
    public GameObject[] typeOfEnemies;
    public float spawnInterval;
}
public class WaveSpawner : MonoBehaviour
{
    public Wave[] waves;
    //public Transform[] spawnPoints;
    public Text waveName;

    private Wave currentWave;
    private int currentWaveNumber;
    private float nextSpawnTime;
    private bool canSpawn = true;
    private bool waveEnd = false;
    private Vector2 screenBounds;

    void Start()
    {
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        waveName.text = waves[currentWaveNumber].waveName;
    }
    // Update is called once per frame
    private void Update()
    {
        currentWave = waves[currentWaveNumber];
        SpawnWave();
        GameObject[] totalEnemies = GameObject.FindGameObjectsWithTag("Enemy");
        if (totalEnemies.Length == 0 && !canSpawn)
        {
            if (currentWaveNumber + 1 != waves.Length)
            {
                if (waveEnd)
                {
                    SpawnNextWave();
                    waveName.text = waves[currentWaveNumber].waveName;
                    waveEnd = false;
                }
            }
            else
            {
                Debug.Log("You Win!");
            }
        }
    }
    void SpawnNextWave()
    {
        currentWaveNumber++;
        canSpawn = true;

    }

    void SpawnWave()
    {
        if (canSpawn && nextSpawnTime < Time.time)
        {
            GameObject randomEnemy = currentWave.typeOfEnemies[Random.Range(0, currentWave.typeOfEnemies.Length)];
            randomEnemy.transform.position = new Vector2(screenBounds.x * 2, Random.Range(-screenBounds.y + 1, screenBounds.y - 1));
            Instantiate(randomEnemy);
            currentWave.noOfEnemies--;
            nextSpawnTime = Time.time + currentWave.spawnInterval;

            if (currentWave.noOfEnemies == 0)
            {
                canSpawn = false;
                waveEnd = true;
            }
        }
    }
}
