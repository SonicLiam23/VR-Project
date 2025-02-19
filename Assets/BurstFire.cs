using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.GraphicsBuffer;

public class BurstFire 
    : SpellBase
{
    [SerializeField] private float homingDelay;
    [SerializeField] private int projectileAmount;
    [SerializeField] private float firingDelay;
    public override void OnCast(Transform position)
    {
        StartCoroutine(SpawnProjectiles(projectileAmount, firingDelay, position.gameObject));
    }

    public override void OnStateLeave()
    {
        OnCast(spawnedRune.transform);
        if (runePrefab != null)
        {
            Destroy(spawnedRune, firingDelay * (projectileAmount + 1));
        }
    }

    private IEnumerator SpawnProjectiles(int amt, float delay, GameObject spawnObjectPos)
    {
        GameObject closestEnemy = GetClosestEnemy();
        for (int i = 0; i < amt; i++)
        {
            projSpawn = spawnObjectPos.transform;
            GameObject spawnedProj = Instantiate(projectilePrefab, projSpawn.position, projSpawn.rotation);
            StartCoroutine(HomeInAfterDelay(homingDelay, closestEnemy, spawnedProj));
            yield return new WaitForSeconds(delay);
        }
    }

    private IEnumerator HomeInAfterDelay(float delay, GameObject target, GameObject projectile)
    {
        yield return new WaitForSeconds(delay);



        if (target != null)
        {
            projectile.GetComponent<ProjectileMoveScript>().SetTarget(target);
        }
        else
        {
            // first target died, get a new one :D
            if (projectile != null)
            {
                projectile.GetComponent<ProjectileMoveScript>().SetTarget(GetClosestEnemy());
            }
        
        }
    }

    private GameObject GetClosestEnemy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject closestEnemy = null;
        float closestDist = Mathf.Infinity;
        foreach (GameObject enemy in enemies)
        {
            float dist = Vector3.Distance(spawnedRune.transform.position, enemy.transform.position);
            if (dist < closestDist)
            {
                closestEnemy = enemy;
                closestDist = dist;
            }
        }

        return closestEnemy;
    }
}
