using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MushroomBounce : MonoBehaviour
{
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.name == "Player")
        {
            animator.Play("MushroomBounce");
        }
    }
}
