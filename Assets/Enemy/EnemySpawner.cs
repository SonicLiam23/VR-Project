using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private ObjectPool enemyPool;
    [SerializeField] int maxEnemies;
    [SerializeField] float spawnRate;

    private void Start()
    {
        StartCoroutine(SpawnEnemies());
    }

    IEnumerator SpawnEnemies()
    {
        while (true)
        {
            if (enemyPool.GetEntirePool(true).Count < maxEnemies)
            {
                GameObject enemy = enemyPool.GetObject(true);
                enemy.transform.position = transform.position;
                enemy.SetActive(true);
            }
            yield return new WaitForSeconds(spawnRate);
        }
    }
}
