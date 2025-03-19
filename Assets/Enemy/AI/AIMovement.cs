using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIMovement : MonoBehaviour
{
    private NavMeshAgent agent;
    // most likely player
    private Transform target;

    [SerializeField] private float reaquireTargetDelayInS = 0.05f;


    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        target = GameObject.FindWithTag("Player").transform;

    }

    private void OnEnable()
    {
        if (agent != null && target != null)
        {
            StartCoroutine(UpdateDestination());
        }
    }

    private void OnDisable()
    {
        if (agent != null && target != null) 
        {
            StopCoroutine(UpdateDestination());
        }
    }

    private IEnumerator UpdateDestination()
    {
        yield return new WaitForEndOfFrame();
        while (true)
        {
            yield return new WaitForSeconds(reaquireTargetDelayInS);
            agent.SetDestination(target.position);
        }
    }


}
