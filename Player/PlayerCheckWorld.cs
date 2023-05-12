using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCheckWorld : MonoBehaviour
{
    private static PlayerCheckWorld instance;
    public static PlayerCheckWorld Instance { get { return instance; } }

    [Header("Check World")]
    [Space(10)]
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask wallLayer;
    public Transform groundCheck;
    public Transform wallCheck;
    public bool isGrounded;
    public bool isWall;

    private ParticleController particleController;

    private void Awake()
    {
        if (instance != null) { Debug.LogError("Only 1 PlayerController"); }
        else { PlayerCheckWorld.instance = this; }
    }
    private void Start()
    {
        particleController =FindObjectOfType<ParticleController>(). GetComponent<ParticleController>();
        groundCheck = GameObject.FindGameObjectWithTag("PlayerCheckGround").GetComponent<Transform>();
        wallCheck = GameObject.FindGameObjectWithTag("PlayerCheckWall").GetComponent<Transform>();

    }

    private void Update()
    {
        CheckGround();
        CheckWall();
    }

    // ----------------- CheckWorld ----------------
    protected void CheckGround()
    {
        if (Physics2D.OverlapCircle(groundCheck.position, 0.1f, groundLayer)) { isGrounded = true; }
        else { isGrounded = false; }
    }

    protected void CheckWall()
    {
        if (Physics2D.OverlapCircle(wallCheck.position, 0.1f, wallLayer)) { isWall = true; }
        else { isWall = false; }
    }

    //------------Set Values -------------
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag =="ground")
        {
            particleController.onDouble = 0;
            PlayerController.Instance.jumpCount = 0;
            PlayerController.Instance.isDash = true;
            particleController.FallParticleSystem();
        }
        if (collision.gameObject.tag =="wall")
        {
           particleController.onDouble = 0;
            PlayerController.Instance.jumpCount = 0;
            PlayerController.Instance.isDash = true;
        }
       
    }
   

}
