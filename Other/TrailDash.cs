using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailDash : MonoBehaviour
{
    [SerializeField] private GameObject trailDash;
    private void Start()
    {
        trailDash = GameObject.FindGameObjectWithTag("Trail");
    }
    void Update()
    {
        if(PlayerController.Instance.Direction == 1)
        {
            trailDash.SetActive(true);
        }
        else
        {
            trailDash.SetActive(false);
        }
    }
}
