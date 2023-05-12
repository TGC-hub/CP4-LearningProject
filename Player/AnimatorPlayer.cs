using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorPlayer : MonoBehaviour
{
    [SerializeField] private AudioSource runSound;
    private Animator animator;

    private int isGround;
    private int isWall;
    private int runHash;
    private int jumpValueHash;
    private int isJumpHash;
    private int jumpWall;

    protected void Awake()
    { 
        animator = GetComponent<Animator>();
    }

    private void Start()
    {

        isGround = Animator.StringToHash("IsGround");
        runHash = Animator.StringToHash("Moving");
        jumpValueHash = Animator.StringToHash("JumpValue");
        isJumpHash = Animator.StringToHash("IsJump");
        isWall = Animator.StringToHash("IsWall");
        jumpWall = Animator.StringToHash("IsJumpWall");
    }
    private void Update()
    {
        AnimJump();
        AnimWallJump();
        AnimRun();

    }

    protected  void AnimRun()
    {
        if(PlayerController.Instance.Rigidbody.velocity.x != 0)
        {
            animator.SetBool(runHash, true);
        }
        else
        {
            animator.SetBool(runHash, false);
        }

        animator.SetBool(isGround, PlayerCheckWorld.Instance.isGrounded);
    }

    protected void AnimJump()
    {
        animator.SetFloat(jumpValueHash, PlayerController.Instance.jumpCount);
        animator.SetFloat(isJumpHash, PlayerController.Instance.Rigidbody.velocity.y);
    }

    protected void AnimWallJump()
    {
        animator.SetBool(isWall, PlayerCheckWorld.Instance.isWall);
        if (Input.GetKeyDown(KeyCode.Space))
        {
            animator.SetBool(jumpWall, true);
        }
        else
        {
            animator.SetBool(jumpWall, false);
        }
    }


    public void RunSound()
    {
       runSound.Play();
    }


}
