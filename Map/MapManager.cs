using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    [SerializeField]private Transform player; 
    [SerializeField]private float hideDistance = 100f; 

    [SerializeField] private List<Transform> maps;
    private bool isResetCalled = false;

    protected void Awake()
    {
        if (!isResetCalled)
        {
            Reset();
        }
    }
   
    protected void Reset()
    {
        this.LoadComponents();
    }

    protected virtual void LoadComponents()
    {
        this.LoadPlayer();
        this.LoadMap();
        isResetCalled = true;
    }

    protected void LoadPlayer()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    protected void LoadMap()
    {
        foreach (Transform map in transform)
        {
            maps.Add(map.gameObject.transform);
        }
    }


    private void Update()
    {
        foreach (Transform map in maps)
        {
            if (map != transform && Vector3.Distance(player.position, map.position) > hideDistance)
            {
                map.gameObject.SetActive(false); 
            }
            else
            {
                map.gameObject.SetActive(true); 
            }
        }
    }
}
