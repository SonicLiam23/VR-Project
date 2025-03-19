using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.XR.Interaction.Toolkit.AR;

public class AIMovement : MonoBehaviour
{
    private NavMeshAgent agent;
    // most likely player
    private Transform target;

    private bool hasTargetAndAgent = false;
    private EnemyDeps enemyInfo;

    private void Awake()
    {
        enemyInfo = GetComponent<EnemyDeps>();
        agent = GetComponent<NavMeshAgent>();
        target = Camera.main.transform;
        if (agent != null && target != null)
        {
            hasTargetAndAgent = true;
        }
    }

    private void OnEnable()
    {
        if (hasTargetAndAgent)
        {
            StartCoroutine(EnemySpawned());
        }
    }

    private void OnDisable()
    {
        if (hasTargetAndAgent) 
        {
            StopCoroutine(UpdateDestination());
            agent.enabled = false;
        }
    }

    private IEnumerator UpdateDestination()
    {
        while (true)
        {
            yield return null;
            agent.isStopped = enemyInfo.canAttack;
            if (!enemyInfo.canAttack)
            {
                agent.isStopped = false;
                agent.SetDestination(target.position);
            }
        }

    }

    private IEnumerator EnemySpawned()
    {
        yield return 0;
        NavMesh.SamplePosition(transform.position, out NavMeshHit hit, 100, NavMesh.AllAreas);
        agent.Warp(transform.position);
        agent.enabled = true;
        StartCoroutine(UpdateDestination());
    }
}
