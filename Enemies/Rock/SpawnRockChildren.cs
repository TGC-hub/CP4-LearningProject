using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnRockChildren : MonoBehaviour
{
    [SerializeField] private GameObject smallRockPrefab;
    [SerializeField] private GameObject largeRockPrefab;
    [SerializeField] private float speed = 5f;

    private bool isGrounded = false;

    void Update()
    {
        if (!isGrounded)
        {
            transform.position -= new Vector3(0, speed * Time.deltaTime, 0);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("ground"))
        {
            isGrounded = true;
            CreateRocks();
            Destroy(gameObject);
        }
    }

    void CreateRocks()
    {
        int numSmallRocks = Random.Range(2, 5);
        int numLargeRocks = Random.Range(1, 3);
        float offset = (numSmallRocks - 1);
        for (int i = 0; i < numSmallRocks; i++)
        {
            Instantiate(smallRockPrefab, transform.position + new Vector3(i - offset, 0, 0), Quaternion.identity);
        }

        for (int i = 0; i < numLargeRocks; i++)
        {
            Instantiate(largeRockPrefab, transform.position + new Vector3(i - offset, 0, 0), Quaternion.identity);
        }

    }
}
