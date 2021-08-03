using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationTrigger : MonoBehaviour
{
    public ActivateBox trigger;
    public string varName;
    private Animator animator;
    private bool canSwitch = true;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        if(canSwitch)
        {
            if (trigger.checkActivation())
            {
                animator.SetBool(varName, true);
            }
            else
            {
                animator.SetBool(varName, false);
            }
            canSwitch = false;
        }
    }

    void allowSiwtch()
    {
        canSwitch = true;
    }

}
