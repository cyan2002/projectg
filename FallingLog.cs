using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingLog : MonoBehaviour
{
    private Animator animator;
    private Rigidbody2D rb;

    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.layer == 11)
        {
            rb.bodyType = RigidbodyType2D.Dynamic;
            rb.gravityScale = 3;
            animator.SetBool("Falling", true);
        }
    }
}
