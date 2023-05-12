using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlatform : MonoBehaviour
{
    [SerializeField] protected Transform samplePoint;
    [SerializeField] protected List<Transform> points; 
    [SerializeField] private float speed;  

    private int currentPoint = 0;
    private bool isResetCalled = false;
    private void Start()
    {
        if (!isResetCalled)
        {
            Reset();
        }
    }
    void Update()
    {
        Moving();
    }

    protected void Reset()
    {
        this.LoadComponents();
    }

    protected virtual void LoadComponents()
    {
        this.LoadPoint();
    }


    protected virtual void LoadPoint()
    {
        FindSamplePoint();
        foreach (Transform point in samplePoint)
        {
            this.points.Add(point);
        }
        isResetCalled = true;
    }

    protected virtual void FindSamplePoint()
    {
        samplePoint = GameObject.Find("PointMoving").GetComponent<Transform>();
    }
    protected virtual void Moving()
    {
        if (transform.position == points[currentPoint].position)
        {
            currentPoint++;
            if (currentPoint >= points.Count)
            {
                currentPoint = 0;
            }
        }

        transform.position = Vector3.MoveTowards(transform.position, points[currentPoint].position, speed * Time.deltaTime);
    }

    protected virtual void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.transform.SetParent(this.transform);
        }
    }

    protected virtual void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.transform.SetParent(null);
        }
    }

}   
