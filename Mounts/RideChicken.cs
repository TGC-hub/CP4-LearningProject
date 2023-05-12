using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RideChicken : MonoBehaviour 
{
    protected Animator animator;

    [SerializeField] protected GameObject pointCheckPlayer;
    [SerializeField] private LayerMask playerMask;

    [SerializeField] private float speed = 20f;
    [SerializeField] private GameObject playerMove;
    [SerializeField] private GameObject riderImage;
    [SerializeField] private GameObject playerImage;
    private Rigidbody2D rbAnimal;
    private Rigidbody2D rbPlayer;
    [SerializeField] private bool isRiding = false;
    [SerializeField] private bool isFacingRight= true;

    private int runHash;
    private bool getDowAnimal = false;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    private void Start()
    {
        rbAnimal = GetComponent<Rigidbody2D>();
        rbAnimal.interpolation = RigidbodyInterpolation2D.Interpolate;
        rbPlayer = playerMove.GetComponentInParent<Rigidbody2D>();
        isRiding = false;

        runHash = Animator.StringToHash("runState");
    }

    private void FixedUpdate()
    {
        MoveRide();
    }


    private void Update()
    {
        CheckPlayer();
    }

    protected virtual void MoveRide()
    {
        if (isRiding)
        {
            float horizontalInput = Input.GetAxisRaw("Horizontal");
            rbAnimal.velocity = new Vector2(horizontalInput * speed, rbAnimal.velocity.y);
            if(horizontalInput == 0)
            {
                animator.SetBool(runHash, false);
            }
            else
            {
                animator.SetBool(runHash, true);
            }
            if (horizontalInput < 0 && isFacingRight) { Flip(); }
            else if (horizontalInput > 0 && !isFacingRight) { Flip(); }
        }
        else
        {
            rbAnimal.velocity = Vector2.zero;
            animator.SetBool(runHash, false);
        }
    }

    protected void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 localScale = transform.localScale;
        localScale.x *= -1f;
        transform.localScale = localScale;
    }


    protected virtual void CheckPlayer()
    {
        if (Physics2D.OverlapCircle(pointCheckPlayer.transform.position, 0.2f, playerMask))
        {

            if (Input.GetKeyDown(KeyCode.R) && !getDowAnimal)
            {
                playerMove.SetActive(false);
                rbPlayer.isKinematic = true;
                rbPlayer.velocity = Vector2.zero;
                rbPlayer.transform.SetParent(transform);
                isRiding = true;
                playerImage.SetActive(false);
                riderImage.SetActive(true);
                getDowAnimal = true;
            }
            else if (Input.GetKeyDown(KeyCode.R) && getDowAnimal)
            {
                playerMove.SetActive(true);
                if(transform.localScale.x > 0) { Flip();}
                playerImage.SetActive(true);
                riderImage.SetActive(false);
                rbPlayer.isKinematic = false;
                rbPlayer.transform.SetParent(null);
                isRiding = false;
                getDowAnimal= false;
            }
        }
    }


}
