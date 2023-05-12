using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Win : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer == 10)
        {
            Destroy(collision.gameObject);
        }

        if(collision.gameObject.layer == 9)
        {
            SceneManager.LoadScene(1);
        }
    }
}
