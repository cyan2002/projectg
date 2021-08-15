using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingLog : MonoBehaviour
{
    private Animator animator;
    private Rigidbody2D rb;

    public GameObject destroyObject1;
    public GameObject destroyObject2;
    public GameObject destroyObject3;

    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.layer == 10)
        {
            rb.bodyType = RigidbodyType2D.Dynamic;
            rb.gravityScale = 3;
            animator.SetBool("Falling", true);
            DestroyChains();
        }
    }

    void DestroyChains()
    {
        Destroy(destroyObject1);
        Destroy(destroyObject2);
        Destroy(destroyObject3);
    }
}
