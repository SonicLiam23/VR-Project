using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{

    [SerializeField] private float delay;
    [SerializeField] private ObjectPool projectilePool;
    [SerializeField] private Transform spawnPos;

    private void Start()
    {
        StartCoroutine(StartShooting());
    }

    private IEnumerator StartShooting()
    {
        while (true)
        { 
            yield return new WaitForSeconds(delay);
            projectilePool.GetObject().transform.SetPositionAndRotation(spawnPos.position, spawnPos.rotation);

        }
    }
}
