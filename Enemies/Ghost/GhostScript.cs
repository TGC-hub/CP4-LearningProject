using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostScript : MonoBehaviour
{
    [SerializeField] private Transform pointParent;
    [SerializeField] private List<Transform> points;
    private int pointIndex;

    [SerializeField] float moveSpeed = 5f;
    [SerializeField] bool time_nn;
    bool facingRight = false;
    int number;
    private bool isResetCalled = false;
    void Start()
    {
        if (!isResetCalled)
        {
            Reset();
        }
        pointIndex = 0;
        transform.position = points[pointIndex].transform.position;
    }


    private void Reset()
    {
        this.LoadComponents();
    }

    protected virtual void LoadComponents()
    {
        this.LoadPointIndex();
    }

    protected virtual void LoadPointIndex()
    {
        pointParent = GameObject.Find("PointIndexGhostMove").GetComponent<Transform>();
        foreach (Transform pointsIndex in pointParent)
        {
            this.points.Add(pointsIndex);
        }
        isResetCalled = true;
    }

    void Update()
    {

        FlipEnemy();

        MoveGhost();

    }

    protected void MoveGhost()
    {
        if (pointIndex <= points.Count - 1)
        {
            transform.position = Vector3.MoveTowards(transform.position,
            points[pointIndex].transform.position, moveSpeed * Time.deltaTime);

            if (transform.position == points[pointIndex].transform.position)
            {
                pointIndex += 1;
                if (time_nn == true)
                {
                    float nn = Random.Range(0.5f, 2f);
                    moveSpeed = nn;
                }
            }
        }
        else
        {
            pointIndex = 0;
        }
    }

    void FlipEnemy()
    {
        if (pointIndex == points.Count)
        {
            number = 0;
        }
        else
        {
            number = pointIndex;
        }
        if (transform.position.x < points[number].position.x && facingRight)
        {
            Flip();
        }
        else if (transform.position.x > points[number].position.x && !facingRight)
        {
            Flip();
        }
    }
    protected void Flip()
    {
        Vector3 thescale = transform.localScale;
        thescale.x *= -1;
        transform.localScale = thescale;
        facingRight = !facingRight;
    }
}
