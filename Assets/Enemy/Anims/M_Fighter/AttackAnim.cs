using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackAnim : MonoBehaviour
{
    private Animator anim;
    private Transform player;
    private EnemyDeps enemyInfo;


    // Start is called before the first frame update
    void Start()
    {
        enemyInfo = GetComponent<EnemyDeps>();
        anim = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        anim.SetBool("isPlayerNear", enemyInfo.canAttack);
    }
}
