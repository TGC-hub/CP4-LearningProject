using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleController : MonoBehaviour
{
    
    [SerializeField] protected ParticleSystem movementParticle;
    [SerializeField] protected ParticleSystem FallParticle;
    [SerializeField] protected ParticleSystem touchParticle;

    [Range(0f, 10f)]
    [SerializeField] int occurAfterVelocity;

    [Range(0, 0.2f)]
    [SerializeField] float dustFormattionPeriod;

    [SerializeField] Rigidbody2D rb2D;

    float counter;
    public int onDouble = 0;

    private void Start()
    {
        touchParticle.transform.parent = null;
    }

    private void Update()
    {
        MovementParticleSystem();
        OnTouchParticleSystem();
        OnDoubleParticleSystem();
    }

    public void MovementParticleSystem()
    {
        counter += Time.deltaTime;

        if (PlayerCheckWorld.Instance.isGrounded == true && Mathf.Abs(rb2D.velocity.x) > occurAfterVelocity)
        {
            if (counter > dustFormattionPeriod)
            {
                movementParticle.Play();
                counter = 0;
            }
        }
    }

    public void FallParticleSystem()
    {
        FallParticle.Play();
    }

    public void TouchParticleSystem(Vector2 pos)
    {   
        touchParticle.transform.position = pos;
        touchParticle.Play();
    }

    protected void OnTouchParticleSystem()
    {
        if (PlayerCheckWorld.Instance.isWall == true) { TouchParticleSystem(PlayerCheckWorld.Instance.wallCheck.position); }
    }
    protected void OnDoubleParticleSystem()
    {
       
        if (Input.GetButtonDown("Jump") && onDouble < 2)
        {
            onDouble++;
            FallParticleSystem();
        }
    }

}
