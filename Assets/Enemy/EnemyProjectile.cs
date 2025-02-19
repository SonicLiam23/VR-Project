using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{

    [SerializeField] private float delay;
    [SerializeField] private GameObject projectile;
    [SerializeField] private Transform spawnPos;

    private void Start()
    {
        StartCoroutine(StartShooting());
    }

    private IEnumerator StartShooting()
    {
        while (true)
        {
            Instantiate(projectile, spawnPos.position, spawnPos.rotation);
            yield return new WaitForSeconds(delay);
        }
    }
}
