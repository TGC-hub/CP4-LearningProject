using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChameleonAI : AIEnemyMove
{
    protected int stoptime = 3;
    private int runHash;
    private int attackHash;

    [SerializeField] private GameObject pointRaycast;
    [SerializeField] private LayerMask targetRaycast;


    protected override void Start()
    {
        base.Start();
        runHash = Animator.StringToHash("EnemyRun");
        attackHash = Animator.StringToHash("EnemyAttack");
        targetRaycast = LayerMask.GetMask("Player");
    }

    protected override void FixedUpdate()
    {
        AttackChameleon();
        CheckRadiusActive();
    }

    protected override void EnemyMovement()
    {
        base.EnemyMovement();
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

    protected virtual void AttackChameleon()
    {
        float maxDistance = 2.5f;
        RaycastHit2D hit = Physics2D.Raycast(pointRaycast.transform.position, Vector2.right * new Vector2(moveHorizontal, 0), maxDistance, targetRaycast);

        if (hit.collider != null)
        {
            rb.velocity = Vector2.zero;
            animator.SetBool(attackHash, true);
            Debug.DrawRay(pointRaycast.transform.position, Vector2.right * hit.distance * new Vector2(moveHorizontal, 0), Color.red);
        }
        else
        {
            EnemyMovement();
            animator.SetBool(attackHash, false);
            Debug.DrawRay(pointRaycast.transform.position, Vector2.right * maxDistance * new Vector2(moveHorizontal, 0), Color.yellow);
        }
    }

    protected virtual void SetDamage()
    {

    }
}
