using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemies : MonoBehaviour
{
    [SerializeField] float spawnDelayInS;
    private EnemySpawner[] enemySpawners;

    private void Start()
    {
        enemySpawners = GetComponentsInChildren<EnemySpawner>();
        StartCoroutine(spawnEnemies());
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
