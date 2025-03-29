using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.XR.Interaction.Toolkit;
using UnityEngine;

public class PlayerHealth : HealthComponent
{
    [SerializeField] private Animation bloodVignette;
    protected override void OnDeath()
    {
        GameManager.Instance.PlayerDied.Invoke();
        health = startHealth;
    }

    public override void Damage(float dmg)
    {
        bloodVignette.Play();
        base.Damage(dmg);
    }
}
