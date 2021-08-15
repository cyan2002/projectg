using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Termite : MonoBehaviour
{
    private bool jumpOut = false;

    [SerializeField]
    private LayerMask whatIsWall;

    private Vector3 raycastStart;
    private Vector3 raycastStart2;
    private Vector3 raycastStart3;

    private float movementSpeed = 5f;

    private bool right;
    private bool once = true;
    private bool canMove = false;

    private int health = 3;

    private SpriteRenderer sprite;
    private Rigidbody2D rb;
    public GameObject itself;

    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        if (jumpOut)
        {
            StartCoroutine(waitTime());
            if (canMove)
            {
                move();
            }

        }

    }

    IEnumerator waitTime()
    {
        yield return new WaitForSeconds(.25f);
        canMove = onGround();
    }

    private bool onGround()
    {
        raycastStart = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        RaycastHit2D hitInfo1 = Physics2D.Raycast(raycastStart, Vector2.down, .55f, whatIsWall);
        if (hitInfo1.collider != null)
        {
            return true;
        }

        return false;
    }

    private void move()
    {
        raycastStart = new Vector3(transform.position.x, transform.position.y + .25f, transform.position.z);
        raycastStart2 = new Vector3(transform.position.x + .5f, transform.position.y, transform.position.z);
        raycastStart3 = new Vector3(transform.position.x - .5f, transform.position.y, transform.position.z);
        if (right)
        {
            RaycastHit2D hitInfo1 = Physics2D.Raycast(raycastStart, Vector2.right, 1f, whatIsWall);
            RaycastHit2D hitInfo3 = Physics2D.Raycast(raycastStart2, Vector2.down, 1f, whatIsWall);

            if (hitInfo1.collider != null)
            {
                movementSpeed = movementSpeed * -1;
                right = false;
                Flip();
            }

            if (hitInfo3.collider == null)
            {
                movementSpeed = movementSpeed * -1;
                right = false;
                Flip();
            }

        }
        else
        {
            RaycastHit2D hitInfo2 = Physics2D.Raycast(raycastStart, Vector2.right, -1f, whatIsWall);
            RaycastHit2D hitInfo4 = Physics2D.Raycast(raycastStart3, Vector2.down, 1f, whatIsWall);

            if (hitInfo2.collider != null)
            {
                movementSpeed = movementSpeed * -1;
                right = true;
                Flip();
            }

            if (hitInfo4.collider == null)
            {
                movementSpeed = movementSpeed * -1;
                right = true;
                Flip();
            }

        }
        rb.velocity = new Vector2(movementSpeed, rb.velocity.y);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.name == "Player" && once)
        {
            once = false;

            sprite.sortingOrder = 0;

            if (col.gameObject.transform.position.x > transform.position.x)
            {
                right = true;
            }
            else
            {
                movementSpeed = movementSpeed * -1;
                right = false;
                Flip();
            }

            rb.AddForce(Vector2.up * 5f, ForceMode2D.Impulse);
            rb.AddForce(Vector2.right * movementSpeed, ForceMode2D.Impulse);

            jumpOut = true;

        }
    }

    private void Flip()
    {
        transform.Rotate(0.0f, 180.0f, 0.0f);
    }
}
