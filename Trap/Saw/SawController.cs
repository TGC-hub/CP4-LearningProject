using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SawController : MovePlatform
{

    protected override void FindSamplePoint()
    {
        samplePoint = GameObject.Find("PointSample").GetComponent<Transform>();
    }

    protected override void OnCollisionExit2D(Collision2D collision)
    {
        
    }

    protected override void OnCollisionStay2D(Collision2D collision)
    {
        
    }


}
