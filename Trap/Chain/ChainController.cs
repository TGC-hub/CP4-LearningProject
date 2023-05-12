using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChainController : MonoBehaviour
{
    private int speedRotate = 2;
    protected virtual void Update()
    {
        StartRotate();
    }

    protected virtual void StartRotate()
    {
      if(transform.rotation.z > -100 && transform.rotation.z < 20)  
        {
            transform.Rotate(0, 0, speedRotate);
        }
        
    }
}
