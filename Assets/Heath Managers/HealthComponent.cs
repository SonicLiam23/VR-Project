using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthComponent : MonoBehaviour
{
    [Tooltip("Start Health")][SerializeField] private float health;

    public void Damage(float dmg)
    {
        health -= dmg;

        if (health <= 0f )
        {
            Destroy(gameObject);
        }
    }
}
