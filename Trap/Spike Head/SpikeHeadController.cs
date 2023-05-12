using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeHeadController : MonoBehaviour {

    private GameObject spikeHead;
    [SerializeField]private float speedFall;
    private bool setNotActive;

    private void Start()
    {
        spikeHead = GameObject.FindGameObjectWithTag("SpikeHead");
        setNotActive = false;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            spikeHead.transform.SetParent(null);
            Rigidbody2D rb = spikeHead.GetComponent<Rigidbody2D>();
            rb.interpolation = RigidbodyInterpolation2D.Interpolate;
            rb.isKinematic = false;
            if(setNotActive == false) { rb.velocity = Vector2.right * speedFall; setNotActive = true; }
        }
    }
}
