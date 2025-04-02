using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.XR.Interaction.Toolkit;
using UnityEngine;

public class PlayerHealth : HealthComponent
{
    [SerializeField] private Animation bloodVignette;

    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Sprite[] bloodTextures;
    int hits = 0;
    bool immune = false;

    protected override void OnDeath()
    {
        GameManager.Instance.PlayerDied.Invoke();
        health = startHealth;
        hits = 0;
    }

    public override void Damage(float dmg)
    {
        if (!immune)
        {
            immune = true;
            if (hits < bloodTextures.Length)
            {
                spriteRenderer.sprite = bloodTextures[hits];
                hits++;

            }
            bloodVignette.Play();
            base.Damage(dmg);

            StartCoroutine(Immunity(1f));
        }
    }

    IEnumerator Immunity(float seconds)
    {
        
        yield return new WaitForSeconds(seconds);
        immune = false;
    }
}
