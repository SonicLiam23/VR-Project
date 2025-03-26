using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// info the enemy needs (enemyDependancies)
public class EnemyDeps : MonoBehaviour
{
    public float distanceToPlayer {get; private set;}
    public bool canAttack { get; private set; }
    static private Transform player;


    [SerializeField] private float updateInfoDelayInS = 0.05f;
    [SerializeField] private float attackDistance = 2.0f;

    private void Awake()
    {
        if (player == null)
        {
            player = Camera.main.transform;
        }
    }

    private void OnEnable()
    {
        StartCoroutine(UpdateInfo());
    }

    private void OnDisable()
    {
        StopCoroutine(UpdateInfo());
    }

    private IEnumerator UpdateInfo()
    {
        while (true)
        {
            distanceToPlayer = Vector3.Distance(player.position, transform.position);

            if (distanceToPlayer <= attackDistance)
            {
                canAttack = true;
            }
            else
            {
                canAttack = false;
            }   

            yield return new WaitForSeconds(updateInfoDelayInS);
        }
    }
}
