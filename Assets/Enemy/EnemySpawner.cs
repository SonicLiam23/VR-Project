using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private Transform enemyPoolsParent;
    [Tooltip("This value is shared accross all enemy spawners. The highest value will be taken, so it reccomended you set it once.")][SerializeField] int _maxEnemies;
    [SerializeField] float spawnRate;

    private bool canSpawn;

    public static int maxEnemies = 0;

    private GameObjectPool[] enemyPools;

    private void Start()
    {
        if (_maxEnemies > maxEnemies)
        {
            maxEnemies = _maxEnemies;
        }
        enemyPools = enemyPoolsParent.GetComponentsInChildren<GameObjectPool>(true);
        canSpawn = true;
        StartCoroutine(SpawnEnemies());
    }

    IEnumerator SpawnEnemies()
    {
        while (true)
        {
            if (canSpawn)
            {
                GameObjectPool enemyPool = enemyPools[Random.Range(0, enemyPools.Length)];
                if (enemyPool.GetEntirePool(GameObjectPool.GetEntirePoolMode.GET_ONLY_ACTIVE).Count < maxEnemies)
                {
                    GameObject enemy = enemyPool.GetObject(true);
                    if (enemy != null)
                    {
                        enemy.transform.position = transform.position;
                    }
                }
            }
            yield return new WaitForSeconds(spawnRate);
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            canSpawn = false;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            canSpawn = true;
        }
    }
}
