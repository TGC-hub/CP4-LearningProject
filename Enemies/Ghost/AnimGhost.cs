using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimGhost : MonoBehaviour
{
    [SerializeField] GameObject ghost_obj;
    [SerializeField] float value = 5f;

    // Update is called once per frame
    void Update()
    {
        AnimMoveGhost();

    }

    protected void AnimMoveGhost()
    {
        if (this.value > 0)
            this.value -= Time.deltaTime;
        else
        {

            if (this.ghost_obj.activeSelf)
            {
                ghost_obj.GetComponent<Animator>().SetBool("disappear", true);
                if (this.ghost_obj.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Desappear"))
                {
                    if (this.ghost_obj.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime > 0.5f)
                    {
                        this.ghost_obj.SetActive(false);
                        this.value = Random.Range(3f, 10f);
                    }
                }
            }
            else
            {
                ghost_obj.GetComponent<Animator>().SetBool("disappear", false);
                this.ghost_obj.SetActive(true);
                this.value = Random.Range(3f, 10f);
            }

        }
    }
}
