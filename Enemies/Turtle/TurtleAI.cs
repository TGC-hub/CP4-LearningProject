using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurtleAI : MonoBehaviour
{
    [SerializeField] protected Animator animator;
    public Collider2D col;
    public Collider2D colPlayer;
    private void Awake()
    {
        animator = GetComponent<Animator>();
        colPlayer = GameObject.FindGameObjectWithTag("Player").GetComponent<Collider2D>();
        col = GetComponentInChildren<CircleCollider2D>();
    }
    private void Start()
    {
        Invoke("SpakeOut", 3f);
    }
    protected void SpakeOut()
    {
        animator.SetInteger("ValueAnim", 1);
        Invoke("Idle1", 0.5f);
    }
    protected void Idle1()
    {
        animator.SetInteger("ValueAnim", 2);
        Invoke("SpakeIn", 3f);
    }
    protected void SpakeIn()
    {
        animator.SetInteger("ValueAnim", 3);
        Invoke("Idle2", 0.5f);
    }
    protected void Idle2()
    {
        animator.SetInteger("ValueAnim", 4);
        Invoke("SpakeOut", 3f);
    }

    protected void AddDame()
    {

    }
}
