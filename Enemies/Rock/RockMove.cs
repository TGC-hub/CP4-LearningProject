using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockMove : AIEnemyMove
{
    [SerializeField] private float rockMoveSpeed;
    [SerializeField] private float jumpForceEnemy;

    [SerializeField] protected LayerMask wallMask;
    [SerializeField] protected Collider2D pointActive;

    private int runHash;
    private bool isGround = false;
   
    private float randomRockSpeedMove;
    private float randomJumpForce;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        rb.interpolation = RigidbodyInterpolation2D.Interpolate;
        RandomFlip();
        RandomRockspeed();
    }

    protected override void Start()
    {
        Bounciness();
        runHash = Animator.StringToHash("EnemyRun");
        rockMoveSpeed = randomRockSpeedMove;
        jumpForceEnemy = randomJumpForce;
    }


    protected override void EnemyMovement()
    {
        moveHorizontal = isFacingRight ? 1f : -1f;

        if (pointActive.IsTouchingLayers(wallMask))
        {
            Flip();
        }
        else
        {
            Move();
            if (isGround == true)
            {
                animator.SetBool(runHash, true);
            }
        }
    }

    protected virtual void RandomRockspeed()
    {
        randomRockSpeedMove = Random.Range(1, 12);
        randomJumpForce = Random.Range(3, 7);
    }
    protected override void Move()
    {
        rb.velocity = new Vector2(rockMoveSpeed * moveHorizontal, rb.velocity.y);
    }


    protected virtual void Bounciness()
    {
        rb.velocity = new Vector2(rockMoveSpeed * moveHorizontal, jumpForceEnemy);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "ground")
        {
            isGround = true;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "ground")
        {
            isGround = false;
        }
    }
}
