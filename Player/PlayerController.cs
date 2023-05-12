using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private static PlayerController instance;
    public static PlayerController Instance { get { return instance; } }

    [Header("Other")]
    [Space(10)]
    private Rigidbody2D rb;
    public Rigidbody2D Rigidbody { get { return rb; } }

    [Header("Movement")]
    [Space(10)]
    [SerializeField] private float moveSpeed = 10f;
    private Transform playerTransform;
    private bool isFacingRight = true;  
    private float horizontal;
    public float Horizontal { get { return horizontal; } }
    
    [Header("Jump")]
    [Space(10)]
    [SerializeField] private float jumpForce = 10f;
    public int jumpCount = 0;

    [Header("Wall Jump")]
    [Space(10)]
    [SerializeField] private bool isWallJumping = false;

    [Header("Wall Slide")]
    [Space(10)]
    [SerializeField] private float wallSlideSpeed = 2f;
    [SerializeField] private bool isWallSliding = false;


    [Header("Skill Dash")]
    [Space(10)]
    [SerializeField] private float dashSpeed = 50f;
    [SerializeField] private float dashDownSpeed = 40f;
    [SerializeField] private float dashUpSpeed = 30f;
    [SerializeField] private float startDashTime = 0.2f;
    public bool isDash = true;
    private float dashTime;
    private int direction;
    public int Direction { get { return direction; } }


    [Header("Audio")]
    [Space(10)]
    [SerializeField] private AudioSource jumpSound;
   

    //----------------- Main -----------------------
    protected  void Awake()
    {
        if (instance != null) { Debug.LogError("Only 1 PlayerController"); }
        else { PlayerController.instance = this; }
        rb = GetComponentInParent<Rigidbody2D>();
    }
    private void Start()
    {
        playerTransform = transform.parent;
        rb.interpolation = RigidbodyInterpolation2D.Interpolate;
        dashTime = startDashTime;
        isDash = true;
    }
    private void Update()
    {
        JumpPlayer();
        CheckKey();
        WallJump();
    }
    private void FixedUpdate()
    {
        
            CheckSlidingWall();
            MovePlayer();
            SildingWall();
            DashSkill();
    }
   
    // ------------------- Movement ---------------
    protected virtual void MovePlayer()
    {
        horizontal = Input.GetAxisRaw("Horizontal");

        rb.velocity = new Vector2(horizontal * moveSpeed, rb.velocity.y);

        if (horizontal < 0 && isFacingRight) { Flip(); }
        else if (horizontal > 0 && !isFacingRight) { Flip(); }
    }

    protected void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 localScale = playerTransform.localScale;
        localScale.x *= -1f;
        playerTransform.localScale = localScale;
    }

    //------------- Jump ---------------
    protected virtual void JumpPlayer()
    {
        if (Input.GetKeyDown(KeyCode.Space) && jumpCount < 2 && PlayerCheckWorld.Instance.isWall == false)
        {
            if (jumpCount == 0)
            {
                rb.velocity = Vector2.up * jumpForce;
            }
            else
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            }
            jumpCount++;
            jumpSound.Play();
        }
    }

    //------------------ Wall Jump -----------------------
    protected  void WallJump()
    {
        if (PlayerCheckWorld.Instance.isWall == true) { isWallJumping = true; }
        else { isWallJumping = false; }
        if (Input.GetButtonDown("Jump") && isWallJumping)
        {
            jumpSound.Play();
            jumpCount = 0;
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            rb.AddForce(new Vector2(-playerTransform.localScale.x * jumpForce * 5, 0), ForceMode2D.Impulse);
        }
    }
 

    // ------------------- Wall Silde ---------------- 
    protected void CheckSlidingWall()
    {
        if (isWallJumping == true)
        {
            if (rb.velocity.y < 0)
            {
                isWallSliding = true;
            }
            else
            {
                isWallSliding = false;
            }
        }
        else
        {
            isWallSliding = false;
        }
    }

    protected void SildingWall()
    {
        if (rb.velocity.y < 0 && isWallSliding)
        {
            rb.velocity = new Vector2(rb.velocity.x, -wallSlideSpeed);
        }
    }

    // ---------------- Skill Dash ----------------

    protected void CheckKey()
    {
        if (direction == 0)
        {
            if (Input.GetKeyDown(KeyCode.Q) && jumpCount == 2 && isDash == true && (horizontal!= 0 || Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.UpArrow)))
            {
                direction = 1;
            }
        }
    }

    protected void DashSkill()
    {
        if (direction != 0)
        {
            if (dashTime <= 0)
            {
                direction = 0;
                dashTime = startDashTime;
                rb.velocity = Vector2.zero;
            }
            else
            {
                dashTime -= Time.deltaTime;
                if (direction == 1)
                {
                    if (Input.GetKey(KeyCode.DownArrow)) { rb.velocity = Vector2.down * dashDownSpeed; }
                    else if (Input.GetKey(KeyCode.UpArrow)) { rb.velocity = Vector2.up * dashUpSpeed; }
                    else { rb.AddForce(new Vector2(horizontal * dashSpeed, 0), ForceMode2D.Impulse); }
                    isDash = false;
                }
            }
        }
    }

}

