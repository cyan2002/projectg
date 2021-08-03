using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//NOTE CHECK FOR ERRORS - canUseVelocity variable

public class BouncingBlock : MonoBehaviour
{
    private Rigidbody2D rb;
    private float speed;

    void OnTriggerEnter2D(Collider2D col)
    {
        rb = col.gameObject.GetComponent<Rigidbody2D>();
        speed = rb.velocity.y;
    }

    void OnCollisionEnter2D()
    {
        rb.AddForce(Vector2.up * Mathf.Abs(speed), ForceMode2D.Impulse);
    }
}
