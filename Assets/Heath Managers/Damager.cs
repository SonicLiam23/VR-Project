using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damager : MonoBehaviour
{
    [SerializeField] private float damage;
    [SerializeField] private float AOERadius = 0;

    private void OnCollisionEnter(Collision collision)
    {
        if (AOERadius == 0f)
        {
            collision.gameObject.SendMessage("Damage", damage, SendMessageOptions.DontRequireReceiver);
        }
        else
        {
            Collider[] collisions = Physics.OverlapSphere(transform.position, AOERadius);
            foreach (Collider collider in collisions)
            {
                collider.gameObject.SendMessage("Damage", damage, SendMessageOptions.DontRequireReceiver);
            }
        }
    }    

    private void OnTriggerEnter(Collider other)
    {
        if (AOERadius == 0f)
        {
            other.gameObject.SendMessage("Damage", damage, SendMessageOptions.DontRequireReceiver);
        }
        else
        {
            Collider[] collisions = Physics.OverlapSphere(transform.position, AOERadius);
            foreach (Collider collider in collisions)
            {
                collider.gameObject.SendMessage("Damage", damage, SendMessageOptions.DontRequireReceiver);
            }
        }
    }
}
