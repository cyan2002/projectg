using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiantMushroomAnimation : MonoBehaviour
{
    private Animator animator;
    private bool once = true;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (once)
        {
            if (col.gameObject.name == "Player")
            {
                animator.SetBool("Landed", true);
                once = false;
            }
        }
    }
}
