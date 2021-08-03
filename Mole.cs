using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mole : MonoBehaviour
{
    private float speed = 5f;

    private Animator animator;
    private Rigidbody2D rb;
    private CapsuleCollider2D collider;

    public bool right;
    private bool once = true;
    public bool startMoving = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        collider = GetComponent<CapsuleCollider2D>();
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.name == "MoleStop")
        {
            startMoving = false;
            rb.velocity = new Vector2(0f, 0f);
            collider.enabled = false;
            animator.SetBool("StartWalk", false);
            animator.SetBool("StartDig", true);
        }
    }

    void Update()
    {
        if (startMoving)
        {
            animator.SetBool("StartWalk", true);
            if (right)
            {
                rb.velocity = new Vector2(5f, 0f);
            }
            else
            {
                rb.velocity = new Vector2(-5f, 0f);
            }
        }
    }

    void disappear()
    {
        Destroy(gameObject);
    }
}
