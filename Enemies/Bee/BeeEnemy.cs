using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeeEnemy : EnemiesAttack
{
    [Header("Setting Bullet Bee")]
    [Space(10)]
    [SerializeField] private GameObject bulletSpawn;
    [SerializeField] private Transform pointBulletSpawn;
    [SerializeField] protected float speedBullet;

    [Header("Animator")]
    private int attackHash;

    protected override void Start()
    {
        base.Start();
        attackHash = Animator.StringToHash("EnemyAttack");
    }
    protected override void Attack()
    {
        base.Attack();
        animator.SetBool(attackHash, true);
    }

    protected override void UnAttack()
    {
        base.UnAttack();
        animator.SetBool(attackHash, false);
      
    }

    protected virtual void AttackFire()
    {
        GameObject bullet = Instantiate(bulletSpawn, pointBulletSpawn.position, pointBulletSpawn.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.interpolation = RigidbodyInterpolation2D.Interpolate;
        rb.velocity = -pointBulletSpawn.up * speedBullet;
    }

}
