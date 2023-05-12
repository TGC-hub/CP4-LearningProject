using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour {
 
    private Animator animator;
    private int shakeHash;
    private int rockHeadShakeHash;
    protected void Awake()
    {
        animator = GetComponent<Animator>();
    }
    private void Start()
    {
        shakeHash = Animator.StringToHash("shakeState");
        rockHeadShakeHash = Animator.StringToHash("ShakeRockHead");
    }

    private void Update()
    {
        ShakeState();
    }

    protected void ShakeState()
    {
        if(PlayerController.Instance.Direction == 1)
        {
            animator.SetBool(shakeHash, true);
        }
        else
        {
            animator.SetBool(shakeHash, false);
        }
    }

    public virtual void OnRockHeadShake()
    {
        animator.SetBool(rockHeadShakeHash, true);

    }
    public virtual void OffRockHeadShake()
    {
        animator.SetBool(rockHeadShakeHash, false);
    }



}
