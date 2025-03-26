using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FighterAnim : MonoBehaviour
{
    private Animator anim;
    private EnemyDeps enemyInfo;
    private HealthComponent healthComponent;


    // Start is called before the first frame update
    void Start()
    {
        enemyInfo = GetComponent<EnemyDeps>();
        healthComponent = GetComponent<HealthComponent>();
        anim = GetComponent<Animator>();
        anim.applyRootMotion = true;
    }

    private void Update()
    {
        anim.SetBool("canAttack", enemyInfo.canAttack);
    }
}
