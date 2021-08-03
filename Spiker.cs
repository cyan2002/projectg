using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spiker : MonoBehaviour
{
    [SerializeField]
    private LayerMask whatIsWall;
    [SerializeField]
    private LayerMask whatIsPlayer;

    private Vector3 raycastStart;
    private Vector3 raycastStart2;
    private Vector3 raycastStart3;
    private Vector2 velocity;

    private Animator animator;
    private Rigidbody2D rb;
    public GameObject itself;

    private float movementSpeed = 2.75f;

    private bool right = true;
    private bool attackMode = false;
    private bool attackLeft = false;
    private bool attackRight = false;
    private bool canMove = true;
    private bool locked = false;

    private int animationPlay;
    private int health = 3;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (canMove)
        {
            if (!locked)
            {
                if (detectPlayer())
                {
                    attackMode = true;
                }
                if (!attackMode)
                {
                    move();
                }
                else
                {
                    beginAttack();
                }
            }
            else
            {
                attackMove();
            }
        }
    }

    private void beginAttack()
    {
        locked = true;
        Flip();
        movementSpeed = 5f;
    }

    private void attackMove()
    {
        raycastStart = new Vector3(transform.position.x, transform.position.y + .25f, transform.position.z);

        if (attackRight)
        {
            RaycastHit2D detectRight = Physics2D.Raycast(raycastStart, Vector2.right, .75f, whatIsWall);
            velocity = new Vector2(5f, rb.velocity.y);
            rb.velocity = velocity;

            if (detectRight.collider != null)
            {
                animator.SetBool("stuck", false);
                canMove = false;
                StartCoroutine(waitTimeGo());
            }
        }
        else if (attackLeft)
        {
            RaycastHit2D detectLeft = Physics2D.Raycast(raycastStart, Vector2.right, -.75f, whatIsWall);
            velocity = new Vector2(-5f, rb.velocity.y);
            rb.velocity = velocity;

            if (detectLeft.collider != null)
            {
                animator.SetBool("stuck", false);
                canMove = false;
                StartCoroutine(waitTimeGo());
            }
        }
        else
        {
            Debug.Log("Error");
        }
    }

    private bool detectPlayer()
    {
        if (right)
        {
            RaycastHit2D[] hitPoints = Physics2D.RaycastAll(transform.position, Vector2.right, Mathf.Infinity);
            if (hitPoints[1].collider.gameObject.layer == 3)
            {
                if (Mathf.Abs(hitPoints[1].collider.gameObject.transform.position.x - transform.position.x) < 5)
                {
                    attackRight = true;
                    return true;
                }
            }
        }
        else
        {
            RaycastHit2D[] hitPoints = Physics2D.RaycastAll(transform.position, Vector2.left, Mathf.Infinity);
            if (hitPoints[1].collider.gameObject.layer == 3)
            {
                if (Mathf.Abs(hitPoints[1].collider.gameObject.transform.position.x - transform.position.x) < 5)
                {
                    attackLeft = true;
                    return true;
                }
            }
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
                //Debug.Log(hitInfo1.collider.name);
            }

            if (hitInfo3.collider == null)
            {
                movementSpeed = movementSpeed * -1;
                right = false;
                Flip();
                //Debug.Log("2");
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
                //Debug.Log("3");
            }

            if (hitInfo4.collider == null)
            {
                movementSpeed = movementSpeed * -1;
                right = true;
                Flip();
                //Debug.Log("4");
            }

        }
        velocity = new Vector2(movementSpeed, rb.velocity.y);
        rb.velocity = velocity;
    }

    private void Flip()
    {
        transform.Rotate(0.0f, 180.0f, 0.0f);
    }

    private IEnumerator waitTimeGo()
    {
        yield return new WaitForSeconds(5f);

        if (attackLeft)
        {
            movementSpeed = 2.75f;
        }
        else
        {
            movementSpeed = -2.75f;
        }

        right = !right;
        attackMode = false;
        attackLeft = false;
        attackRight = false;
        canMove = true;
        locked = false;

        animator.SetBool("stuck", true);
    }
}
