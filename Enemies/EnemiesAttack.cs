using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesAttack : MonoBehaviour
{
    [SerializeField] private float attackRange;
    [SerializeField] protected float moveSpeed;
    [SerializeField] protected float moveSpeedAttack;

    [SerializeField] protected Transform target;
    [SerializeField] protected Vector3 startingPosition;
    protected float rotationSpeed = 10f;
    protected bool isAttacking = false;

    protected Animator animator;
    protected Rigidbody2D rb;

    protected float moveHorizontal = -1;
    protected float distanceAttack = 2;

    [SerializeField] private Collider2D radiusActive;
    [SerializeField] private Collider2D playerCol;
    protected virtual void Start()
    {
     
        animator = GetComponent<Animator>();
        startingPosition = transform.position;
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        playerCol = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<CircleCollider2D>();
    }

    protected virtual void Update()
    {
        EnemyAttack();
    }
  

    protected virtual void EnemyAttack()
    {
        float distanceToTarget = Vector3.Distance(transform.position, target.position);
        if (radiusActive.IsTouching(playerCol))
        {
            if (distanceToTarget < attackRange)
            {
                Attack();
            }
            else 
            {
                if (isAttacking) UnAttack();
            }
        }
        else
        {
            UnAttack();
        }
    }

    protected virtual void Attack()
    {
        Vector3 targetPosition = new Vector3(target.position.x - distanceAttack, transform.position.y, transform.position.z);
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeedAttack * Time.deltaTime);
        FLipEnemy();    
        isAttacking = true;
    }

    protected virtual void UnAttack()
    {
        Vector3 startingPositionX = new Vector3(startingPosition.x, transform.position.y, transform.position.z);
        transform.position = Vector3.MoveTowards(transform.position, startingPositionX, moveSpeed * Time.deltaTime);
        FLipStartingPosition();
        if (transform.position == startingPositionX)
        {
            isAttacking = false;
        }
    }

    protected  void FLipEnemy()
    {
        Vector3 targetDir = target.position - transform.position;
        float angle = Mathf.Atan2(0, targetDir.x) * Mathf.Rad2Deg;

        Quaternion targetRotation = Quaternion.Euler(new Vector3(0, angle - 180, 0));

        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed);

        distanceAttack *= -1;
        moveHorizontal *= -1;

    }
    protected void FLipStartingPosition()
    {
        Vector3 targetDir = startingPosition - transform.position;
        float angle = Mathf.Atan2(0, targetDir.x) * Mathf.Rad2Deg;

        Quaternion targetRotation = Quaternion.Euler(new Vector3(0, angle - 180, 0));

        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed);
    }


}
