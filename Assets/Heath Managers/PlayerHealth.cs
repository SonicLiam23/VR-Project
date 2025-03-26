using System.Collections;
using System.Collections.Generic;
using UnityEditor.XR.Interaction.Toolkit;
using UnityEngine;

public class PlayerHealth : HealthComponent
{
    [SerializeField] private GameObject[] spawnPoints;
    [SerializeField] private GameObject enemyPoolParent;
    [SerializeField] private Animation anim;
    protected override void OnDeath()
    {
        foreach (GameObject spawnPoint in spawnPoints)
        {
            spawnPoint.SetActive(false);
        }
        enemyPoolParent.SetActive(false);
        health = startHealth;
    }

    public override void Damage(float dmg)
    {
        anim.Play();
        base.Damage(dmg);
    }
}
