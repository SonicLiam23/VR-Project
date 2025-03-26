using System.Collections;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(EnemyDeps))]
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
            agent.isStopped = enemyInfo.canAttack || enemyInfo.healthComponent.isDead;
            if (!enemyInfo.canAttack && !enemyInfo.healthComponent.isDead)
            {
                agent.SetDestination(target.position);
            }
            yield return null;
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
