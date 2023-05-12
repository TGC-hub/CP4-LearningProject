using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIEnemyMove : MonoBehaviour
{
    [Header("Move Controller")]
    [Space(10)]
    [SerializeField] protected float enemyMoveSpeed;
    [SerializeField] protected bool isFacingRight;
    protected bool isFlip = false;
    protected float moveHorizontal;

    [Header("Other")]
    [Space(10)]
    [SerializeField] protected GameObject pointRadius;
    [SerializeField] protected LayerMask radiusActive;
    protected Rigidbody2D rb;
    protected Animator animator;

    protected float elapsedTime = 0.0f;
    protected bool isMoving = false;
    protected bool isWaiting = false;
    [SerializeField] private float moveTime;
    [SerializeField] protected float waitTime;

    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        rb.interpolation = RigidbodyInterpolation2D.Interpolate;
        RandomFlip();
        isMoving = true;
    }

    protected virtual void FixedUpdate()
    {
        EnemyMovement();
        CheckRadiusActive();
    }

    protected void RandomFlip()
    {
        int random = Random.Range(1, 5);
        if(random >= 3) { return; }
        else { Flip(); }
    }

    protected virtual void EnemyMovement()
    {
        moveHorizontal = isFacingRight ? 1f : -1f;
        if (isFlip)
        {
            Flip();
        }
        if (isMoving)
        {
            Move();
            Moving();
        }
        else if (isWaiting)
        {
            StopMove();
            Waiting();
        }
    }

    protected virtual void Moving()
    {
        elapsedTime += Time.deltaTime;
        if (elapsedTime >= moveTime)
        {
            isMoving = false;
            isWaiting = true;
            elapsedTime = 0.0f;
        }
    }
    protected virtual void Waiting()
    {
        elapsedTime += Time.deltaTime;
        if (elapsedTime >= waitTime)
        {
            isMoving = true;
            isWaiting = false;
            elapsedTime = 0.0f;
        }
    }

    protected void CheckRadiusActive()
    {
       if(Physics2D.OverlapCircle(pointRadius.transform.position, 0.2f, radiusActive))
        {
            isFlip = false;
        }
        else
        {
            isFlip = true;
        }
    }

    protected virtual void Move()
    {
        rb.velocity = new Vector2(enemyMoveSpeed * moveHorizontal, rb.velocity.y);
    }

    protected virtual void StopMove()
    {
        rb.velocity = Vector2.zero;
    }
    protected virtual void Flip()
    {
            isFacingRight = !isFacingRight;
            transform.Rotate(0f, 180f, 0f);
    }
}
