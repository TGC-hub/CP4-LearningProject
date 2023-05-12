using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IgnoreLayerCollision : MonoBehaviour
{
    protected void Start()
    {
        gameObject.layer = LayerMask.NameToLayer("Enemy");
        Physics2D.IgnoreLayerCollision(gameObject.layer, gameObject.layer, true);
    }
}
