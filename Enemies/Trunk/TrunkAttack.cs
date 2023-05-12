using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrunkAttack : AIEnemyMove
{
    [SerializeField] private GameObject bulletSpawn;
    [SerializeField] private float speedBullet;

    [SerializeField] private Transform pointFire;
    [SerializeField] private LayerMask playerMask;
    [SerializeField] private GameObject ob;
    
    protected int attackHash;
    protected int runHash;

    protected override void Start()
    {
        base.Start();
        playerMask = LayerMask.GetMask("Player");
        attackHash = Animator.StringToHash("EnemyAttack");
        runHash = Animator.StringToHash("EnemyMove");
    }
    protected override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    protected override void EnemyMovement()
    {
            float maxDistance = 12f;
            RaycastHit2D hit = Physics2D.Raycast(ob.transform.position, Vector2.right * new Vector2(moveHorizontal, 0), maxDistance, playerMask);
            
            if (hit.collider != null)
            {
                rb.velocity = Vector2.zero;
                animator.SetBool(attackHash, true);
                Debug.DrawRay(ob.transform.position, Vector2.right * hit.distance * new Vector2(moveHorizontal, 0) , Color.red);
             }
            else
            {
                base.EnemyMovement();
                animator.SetBool(runHash, true);
                animator.SetBool(attackHash, false);
                Debug.DrawRay(ob.transform.position, Vector2.right * maxDistance * new Vector2(moveHorizontal, 0) , Color.yellow);
            }
    }

    protected virtual void Attack()
    {
        GameObject bullet = Instantiate(bulletSpawn, pointFire.position, pointFire.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.interpolation = RigidbodyInterpolation2D.Interpolate;
        rb.velocity = -pointFire.right * speedBullet;
    }
}
