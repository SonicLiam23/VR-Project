using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemies : MonoBehaviour
{
    [SerializeField] float spawnDelayInS;
    private EnemySpawner[] enemySpawners;

    private void Awake()
    {
        enemySpawners = GetComponentsInChildren<EnemySpawner>();
    }

    private void OnEnable()
    {
        for (int i = 0; i < enemySpawners.Length; i++)
        {
            for (int j = 0; j < enemySpawners[i].enemyPools.Length; j++)
            {
                enemySpawners[i].enemyPools[j].ReturnAllToPool();
            }
        }

        StartCoroutine(spawnEnemies());
    }

    private void OnDisable()
    {   
        StopCoroutine(spawnEnemies());
    }

    IEnumerator spawnEnemies()
    {
        EnemySpawner currentSpawner;
        while (true)
        {
            yield return new WaitForSeconds(spawnDelayInS);
            currentSpawner = enemySpawners[Random.Range(0, enemySpawners.Length)];
            currentSpawner.SpawnEnemy();
        }
    }
}
