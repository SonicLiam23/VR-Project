using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableSword : MonoBehaviour
{
    CapsuleCollider sword;
    EnemyDeps enemyInfo;

    private void Awake()
    {
        enemyInfo = transform.parent.parent.parent.parent.parent.parent.parent.parent.parent.GetComponent<EnemyDeps>(); 
        // enemyInfo = transform.root.GetComponent<EnemyDeps>(); 
        sword = GetComponent<CapsuleCollider>();
    }

    private void Update()
    {
        if (enemyInfo.canAttack)
        {
            sword.enabled = true;
        }
        else
        {
            sword.enabled = false;
        }
    }
}
