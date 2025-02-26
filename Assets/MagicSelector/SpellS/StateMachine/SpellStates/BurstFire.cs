using System;
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
        StartCoroutine(SpawnProjectiles(position.gameObject));
    }

    public override void OnStateLeave()
    {
        OnCast(spawnedRune.transform);
        if (runePrefab != null)
        {
            Destroy(spawnedRune, firingDelay * (projectileAmount + 1));
        }
        // if the spell actually costs mana and mana isnt already being regenerated from a previous spell
        if (manaCost > 0 && !ISpellState.stateMachine.manaSystem.isRegeneratingMana)
        {
            StartCoroutine(RegenManaAfterComplete());
        }
    }

    private IEnumerator SpawnProjectiles(GameObject spawnObjectPos)
    {
        GameObject closestEnemy = GetClosestEnemy();
        int manaCostPerProj = manaCost / projectileAmount;
        for (int i = 0; i < projectileAmount; i++)
        {
            projSpawn = spawnObjectPos.transform;
            GameObject spawnedProj = projectilePool.GetObject();
            spawnedProj.transform.SetPositionAndRotation(spawnObjectPos.transform.position, spawnObjectPos.transform.rotation);
            ISpellState.stateMachine.manaSystem.SpendMana(manaCostPerProj);
            StartCoroutine(HomeInAfterDelay(closestEnemy, spawnedProj));
            yield return new WaitForSeconds(firingDelay);
        }
    }

    private IEnumerator HomeInAfterDelay(GameObject target, GameObject projectile)
    {
        yield return new WaitForSeconds(homingDelay);


        if (projectile != null)
        {

            if (target != null)
            {
                projectile.GetComponent<ProjectileMoveScript>().SetTarget(target);
            }
            else
            {
                // first target died, get a new one :D
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

    IEnumerator RegenManaAfterComplete()
    {
        yield return new WaitForSeconds(firingDelay * (projectileAmount + 1));
        StartCoroutine(ISpellState.stateMachine.manaSystem.RegenMana());
    }
}