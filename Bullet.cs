using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    public float speed = 20f;
    private Rigidbody2D rb;
    private Animator animator;
    private CircleCollider2D hitBox;
    public bool NPCbullet = false;

    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        hitBox = GetComponent<CircleCollider2D>();
        rb.velocity = transform.right * speed;
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if(NPCbullet)
        {
            Die();
        }
        else
        {
            hitBox.enabled = false;
            animator.SetBool("Hit", true);
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }
}
