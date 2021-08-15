using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceBlockReceive : MonoBehaviour
{
    public float yVelocity;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GameObject.Find("Player").GetComponent<Rigidbody2D>();
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.name == "Player")
        {
            yVelocity = rb.velocity.y;
        }
    }
}
