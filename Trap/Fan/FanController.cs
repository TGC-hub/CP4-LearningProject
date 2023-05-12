using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FanController : MonoBehaviour
{
    [SerializeField]protected float force;
    [SerializeField] protected ParticleSystem particle;

    private void Update()
    {
        RotationFan();
    }

    protected void RotationFan()
    {
        particle.transform.Rotate(0, 0, 1);
    }

    protected virtual void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Rigidbody2D rb = other.gameObject.GetComponent<Rigidbody2D>();
            rb.AddForce(new Vector2(rb.velocity.x, force * Time.deltaTime), ForceMode2D.Impulse);
        }
    }

}
