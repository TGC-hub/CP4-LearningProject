using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveMushroom : AIEnemyMove
{
    protected int runHash;

    protected override void Start()
    {
        base.Start();
        runHash = Animator.StringToHash("EnemyMove");
    }

    protected override void Move()
    {
        base.Move();
        animator.SetBool(runHash, true);
    }

    protected override void StopMove()
    {
        base.StopMove();
        animator.SetBool(runHash, false);
    }

}
