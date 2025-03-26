using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthComponent : MonoBehaviour
{
    [Tooltip("Start Health")][SerializeField] protected float startHealth;
    public float health { get; set; }
    public bool isDead { get; protected set; } 
    Animator animator = null;

    private void Start()
    {
        if (TryGetComponent<Animator>(out Animator a))
        {
            animator = a;
        }
    }

    private void OnEnable()
    {
        health = startHealth;
        isDead = false;
    }

    public virtual void Damage(float dmg)
    {
        health -= dmg;

        if (health <= 0f)
        {
            isDead = true;
            OnDeath();
        }
    }

    protected virtual void OnDeath()
    {
        StartCoroutine(delayDisable(5f));
        if (animator != null)
        {
            animator.SetBool("isDead", true);
        }
   
    }


    IEnumerator delayDisable(float time)
    {
        yield return new WaitForSeconds(time);
        gameObject.SetActive(false);
    }
}
