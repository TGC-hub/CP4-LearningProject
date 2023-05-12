using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RinoAttack : EnemiesAttack
{
    protected int enemyRunHash;

    protected override void Start()
    {
        base.Start();
        enemyRunHash = Animator.StringToHash("EnemyRun");
    }


    protected override void Attack()
    {
        base.Attack();
        animator.SetBool(enemyRunHash, true);
    }

    protected override void UnAttack()
    {
        base.UnAttack();
        if (isAttacking == false)
        {
            animator.SetBool(enemyRunHash, false);
        }
        
    }

}
