using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BunnyScript : MonoBehaviour
{
    [Header("Move Controller")]
    [Space(10)]
    [SerializeField] private float moveSpeed; 
    [SerializeField] private float jumpForce;
    [SerializeField] private float jumpTime;
    [SerializeField] private float jumpTimeCounter;
    private bool isJumping = true;

    [Header("Other")]
    [Space(10)]
    private Rigidbody2D rb; 
    protected Animator animator;

    [Header("Random")]
    [Space(10)]
    private float randomStyle;
    private float randomSpeed;
    private float randomJumpForce;
    private float randomJumpTime;
    [SerializeField] private float randomFlip;

    [Header("Animator Hash")]
    [Space(10)]
    protected int runHash;
    protected int jumpHash;
    protected int fallHash;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        rb.interpolation = RigidbodyInterpolation2D.Interpolate;

        randomStyle = Random.Range(1, 10);
        randomSpeed = Random.Range(5, 12);
        randomJumpForce = Random.Range(3, 5);
        randomJumpTime = Random.Range(0.2f, 2f);
        randomFlip = Random.Range(1, 10);

    }

    void Start()
    {
        moveSpeed = randomSpeed;
        jumpForce = randomJumpForce;
        jumpTime = randomJumpTime;
        
        runHash = Animator.StringToHash("EnemyRun");
        jumpHash = Animator.StringToHash("EnemyJump");
        fallHash = Animator.StringToHash("EnemyFall");
        
    }

   
    void FixedUpdate()
    {
        RandomFlip();
        RandomStyleBunny();
        Flip();
    }

    protected void RandomFlip()
    {
        if (randomFlip > 5f && Time.time == 5)
        {
            transform.Rotate(0, 180, 0);
        }
    }

    protected void RandomStyleBunny()
    {
        if(randomStyle < 5)
        {
            JumpBunny();
        }
        else
        {
            MoveBunny();
        }
    }

   protected void JumpBunny()
    {
        MoveBunny();

        if (isJumping)
        {
            jumpTimeCounter -= Time.deltaTime;

            if (jumpTimeCounter <= 0)
            {
                isJumping = false;
            }
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            if(jumpTimeCounter > 0)
            {
                animator.SetBool(jumpHash, true);
            }else if(jumpTimeCounter < 0)
            {
                animator.SetBool(fallHash, true);
            }
        }
    }

    protected void MoveBunny()
    {
        rb.velocity = new Vector2(moveSpeed, rb.velocity.y);
        animator.SetBool(runHash, true);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "ground")
        {
            animator.SetBool(fallHash, false);
            if (!isJumping)
            {
                isJumping = true;
                jumpTimeCounter = jumpTime;
            }
        }
    }

    protected void Flip()
    {
       
        if(transform.rotation.y == 180)
        {
            moveSpeed = randomSpeed ;
        }
        else if(transform.rotation.y == 0)
        {
            moveSpeed = -randomSpeed;
        }
    }

}
