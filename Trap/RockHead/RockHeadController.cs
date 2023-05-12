using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockHeadController : MonoBehaviour
{
    [SerializeField] private float jumpforce;
    [SerializeField] private bool isGround;
    [SerializeField] private GameObject pointCheck;
    [SerializeField] private LayerMask ground;
    [SerializeField] private Transform player;

    private CameraShake cameraShake;
    private bool isShake = true;

    private Rigidbody2D rb;
    private Animator animator;

    private int stateHash;
    private float distance;
    public float Distance { get { return distance; } }
    private void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        rb.interpolation = RigidbodyInterpolation2D.Interpolate;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        stateHash = Animator.StringToHash("Distance");
        cameraShake = FindObjectOfType<CameraShake>();
        isShake = true;
    }

    private void Update()
    {
        CheckGround();
        AttackController();
        SetAnimShake();
    }

    protected virtual void AttackController()
    {
        distance = Vector2.Distance(player.position, transform.position);

        animator.SetFloat(stateHash, distance);
    }
    protected virtual void AttackJump()
    {

        float distanceFromPlayer = player.position.x - transform.position.x;
        if (isGround) { rb.velocity = new Vector2(distanceFromPlayer, jumpforce); }
    }

    protected virtual void CheckGround()
    {
        if(Physics2D.OverlapCircle(pointCheck.transform.position, 0.2f, ground))
        {
            isGround = true;
           
        }
        else
        {
            isGround = false;
        }
    }

    protected void SetAnimShake()
    {
        if(distance > 10)
        {
            cameraShake.OffRockHeadShake();
            isShake = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "ground" && isShake == false)
        {
            if (distance < 10)
            {
                cameraShake.OnRockHeadShake();
                isShake = true;
            }
            else
            {
                cameraShake.OffRockHeadShake();
                isShake = false;
            }
        }
    }   

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "ground" && isShake == true)
        {
            cameraShake.OffRockHeadShake();
            isShake = false;
        }
    }

}
