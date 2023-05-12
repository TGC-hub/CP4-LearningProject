using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : MonoBehaviour
{
    [SerializeField] protected List<Transform> posSpwanRock;

    [SerializeField] protected Transform player;
    [SerializeField] protected Transform rock;

    private float spawnTime = 3f;
    private int stopInvoke = 1;

    private void Reset()
    {
        LoadComponent();
    }

    protected virtual void LoadComponent()
    {
        LoadRock();
        LoadPointPosRock();
        LoadPlayer();
    }

    protected void LoadPlayer()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    protected virtual void LoadRock()
    {
        rock = GameObject.FindGameObjectWithTag("Rock").GetComponent<Transform>();
    }



    protected virtual void LoadPointPosRock()
    {
        Transform samplePoint = GameObject.Find("PointRock").GetComponent<Transform>();
        foreach (Transform point in samplePoint)
        {
            this.posSpwanRock.Add(point);
        }
    }

    void Start()
    {
        stopInvoke = 1;
    }

    private void Update()
    {
        CheckDistance();
    }

    protected void CheckDistance()
    {
        float distance = Vector2.Distance(gameObject.transform.position,player.position);
        if(distance < 100 && stopInvoke == 1)
        {
            stopInvoke++;
            InvokeRepeating("Spawn", spawnTime, spawnTime);
        }
  
    }


    void Spawn()
    {
            int spawnPointIndex = Random.Range(0, posSpwanRock.Count);
            Instantiate(rock, posSpwanRock[spawnPointIndex].position, posSpwanRock[spawnPointIndex].rotation);
    }




}
