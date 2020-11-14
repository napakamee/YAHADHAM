using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    public GameObject[] Enemy;
    public float respawnTime = 3.0f;
    private Vector2 screenBounds;

    // Use this for initialization
    void Start()
    {
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        StartCoroutine(enemyWave());
    }
    private void spawnEnemy()
    {
        GameObject a = Lean.Pool.LeanPool.Spawn(Enemy[Random.Range(0, Enemy.Length)]) as GameObject;
        a.transform.position = new Vector2(screenBounds.x * 2, Random.Range(-screenBounds.y, screenBounds.y));
    }
    IEnumerator enemyWave()
    {
        while (true)
        {
            yield return new WaitForSeconds(respawnTime);
            spawnEnemy();
        }
    }
}
