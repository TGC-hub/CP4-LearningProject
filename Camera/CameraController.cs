using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("Setting Player")]
    [Space(10)]
    [SerializeField] private Transform player;

    [Header("Setting Camera")]
    [Space(10)]
    [SerializeField] private float smoothSpeed = 0.05f;
    [SerializeField] private Vector3 offset = new Vector3(0f, 0f, -10f);

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }
    private void LateUpdate()
    {
        Vector3 desiredPosition = player.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;
    }
}
