using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] GameObjectPool[] enemyPools;
    private bool canSpawn;



    private void Start()
    {
        canSpawn = true;
    }

    // returns if an enemy has been spawned
    public bool SpawnEnemy()
    {
        if (canSpawn)
        {
            GameObjectPool enemyPool = enemyPools[Random.Range(0, enemyPools.Length)];
            if (enemyPool.GetEntirePool(GameObjectPool.GetEntirePoolMode.GET_ONLY_ACTIVE).Count < GameManager.Instance.maxEnemies)
            {
                GameObject enemy = enemyPool.GetObject(true);
                if (enemy != null)
                {
                    enemy.transform.position = transform.position;
                }
            }
        }
        return canSpawn;
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
