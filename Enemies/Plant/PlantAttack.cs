using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantAttack : MonoBehaviour
{
    [SerializeField] private Transform player;

    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform bulletSpawnPoint;
    [SerializeField] protected float speedBullet;
    protected Animator animator;

    private float distance;
    protected int fireHash;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        animator = GetComponent<Animator>();
        fireHash = Animator.StringToHash("EnemyAttack");
    }


    protected virtual void FixedUpdate()
    {
        CheckPlayer();
    }

    protected virtual void CheckPlayer()
    {
        distance = Vector2.Distance(player.position, transform.position);
        if(distance < 15f)
        {
            Flip();
            animator.SetBool(fireHash, true);
        }
        else { animator.SetBool(fireHash, false); }
      
    }

    protected virtual void Flip()
    {
        Vector3 targetDir = player.position - transform.position;
        float angle = Mathf.Atan2(0, targetDir.x) * Mathf.Rad2Deg;

        Quaternion targetRotation = Quaternion.Euler(new Vector3(0, angle - 180, 0));

        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 10);
    }

    protected virtual void Attack()
    {
        
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.interpolation = RigidbodyInterpolation2D.Interpolate;
        rb.velocity = -bulletSpawnPoint.right * speedBullet;
    }

   
   
}
