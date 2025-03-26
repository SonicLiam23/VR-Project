using System.Collections;
using UnityEngine;

// info the enemy needs (enemyDependancies)
[RequireComponent(typeof(HealthComponent))]
public class EnemyDeps : MonoBehaviour
{
    public float distanceToPlayer {get; private set;}
    public bool canAttack { get; private set; }
    static private Transform player;


    [SerializeField] private float updateInfoDelayInS = 0.05f;
    [SerializeField] private float attackDistance = 2.0f;
    
    public HealthComponent healthComponent { get; private set; }
   

    private void Awake()
    {
        if (player == null)
        {
            player = Camera.main.transform;

        }
        healthComponent = GetComponent<HealthComponent>();
    }

    private void OnEnable()
    {
        StartCoroutine(UpdateInfo());
    }

    private void OnDisable()
    {
        StopCoroutine(UpdateInfo());
    }

    public void Attacked()
    {
        if (canAttack)
        {
            GameManager.Instance.mainPlayersHealth.Damage(5f);
        }
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
