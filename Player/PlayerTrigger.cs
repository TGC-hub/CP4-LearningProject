using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PlayerTrigger : MonoBehaviour
{

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer == 10 || collision.gameObject.tag == "TrapMossy")
        {
            SceneManager.LoadScene(0);
        }

        if (collision.gameObject.layer == 11)
        {
            Physics2D.IgnoreLayerCollision(collision.gameObject.layer, gameObject.layer, true);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 10 || collision.gameObject.tag == "TrapMossy")
        {
            SceneManager.LoadScene(0);
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {


        if (collision.gameObject.layer == 11)
        {
            Physics2D.IgnoreLayerCollision(collision.gameObject.layer, gameObject.layer, true);
        }
    }

}
