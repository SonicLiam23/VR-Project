using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private Transform enemyPoolsParent;
    [Tooltip("This value is shared accross all enemy spawners. The highest value will be taken, so it reccomended you set it once.")][SerializeField] int _maxEnemies;
    [SerializeField] float spawnRate;

    public static int maxEnemies = 0;

    private GameObjectPool[] enemyPools;

    private void Start()
    {
        if (_maxEnemies > maxEnemies)
        {
            maxEnemies = _maxEnemies;
        }
        enemyPools = enemyPoolsParent.GetComponentsInChildren<GameObjectPool>(true);
        StartCoroutine(SpawnEnemies());
    }

    IEnumerator SpawnEnemies()
    {
        while (true)
        {
            GameObjectPool enemyPool = enemyPools[Random.Range(0, enemyPools.Length)];
            if (enemyPool.GetEntirePool(GameObjectPool.GetEntirePoolMode.GET_ONLY_ACTIVE).Count < maxEnemies)
            {
                GameObject enemy = enemyPool.GetObject(true);
                if (enemy != null)
                {
                    enemy.transform.position = transform.position;
                    enemy.SetActive(true);
                }
            }
            yield return new WaitForSeconds(spawnRate);
        }
    }
}
