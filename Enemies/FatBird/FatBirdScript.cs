using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FatBirdScript : MonoBehaviour
{
    [Header("Other")]
    [Space(10)]
    [SerializeField] protected Transform targetFall;
    [SerializeField] protected GameObject targetPlayer;
    [SerializeField] protected Vector2 startingPosition;
    [SerializeField] protected LayerMask radiusActive;

    [Header("Controller")]
    [Space(10)]
    [SerializeField] protected float moveSpeedAttack;
    [SerializeField] protected float moveSpeed;
    [SerializeField] protected bool isAttack;
    protected bool resetAttack;

    [Header("Animator Hash")]
    [Space(10)]
    protected Animator animator;
    protected int fallHash;
    protected int groundHash;

    private void Start()
    {
        animator = GetComponent<Animator>();
        startingPosition = transform.position;
        isAttack = false;
        resetAttack = true;
        fallHash = Animator.StringToHash("FallState");
        groundHash = Animator.StringToHash("GroundState");
    }

    private void Update()
    {
        IsAttack();
    }

    protected void IsAttack()
    {
        if(resetAttack == true) { CheckRadiusActive(); }

        if (isAttack)
        {
            Attack();
        }
        else
        {
            UnAttack();
        }
    }

    protected virtual void Attack()
    {
        animator.SetBool(fallHash, true);
        Vector3 targetPosition = new Vector3(transform.position.x , targetFall.position.y, transform.position.z);
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeedAttack * Time.deltaTime);
        if (transform.position == targetPosition) { isAttack = false; resetAttack = false; animator.SetBool(groundHash, true); }
    }

    protected virtual void UnAttack()
    {
        Vector3 startingPositionX = new Vector3(transform.position.x, startingPosition.y, transform.position.z);
        transform.position = Vector3.MoveTowards(transform.position, startingPositionX, moveSpeed * Time.deltaTime);
        if(transform.position == startingPositionX) { resetAttack = true; }
        animator.SetBool(groundHash, false);
        animator.SetBool(fallHash, false);
    }

    protected void CheckRadiusActive()
    {
        if (Physics2D.OverlapCircle(targetPlayer.transform.position, 0.2f, radiusActive))
        {
            isAttack = true;
        }
    }
}
