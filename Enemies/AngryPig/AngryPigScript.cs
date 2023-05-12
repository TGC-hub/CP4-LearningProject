using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AngryPigScript : AIEnemyMove
{
    [SerializeField] protected bool isAngry = false;

    [Header("Animator")]
    protected int walkHash;
    protected int runHash;
    protected override void Start()
    {
        base.Start();
        isAngry = false;
        walkHash = Animator.StringToHash("EnemyWalk");
        runHash = Animator.StringToHash("EnemyRun");
    }
    protected override void EnemyMovement()
    {
        if (isAngry)
        {
            moveHorizontal = isFacingRight ? 1f : -1f;
            if (isFlip)
            {
                Flip();
            }
            rb.velocity = new Vector2(15 * moveHorizontal, rb.velocity.y);
            animator.SetBool(runHash, true);
        }
        else
        {
            base.EnemyMovement();
        }
  
    }

    protected override void Move()
    {
        base.Move();
        animator.SetBool(walkHash, true);
    }

    protected override void StopMove()
    {
        base.StopMove();
        animator.SetBool(walkHash, false);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 9)
        {
            isAngry = true;
        }

    }
}
