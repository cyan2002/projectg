using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PossessedBones : MonoBehaviour
{
    private bool attack = false;
    private bool patrol = true;
    private bool up = false;
    private bool still = false;
    public Vector2 originalPositionR;
    public Vector2 originalPositionL;
    public GameObject itself;
    private Animator animator;
    private Rigidbody2D rb;

    public GameObject leftPoint;
    public GameObject rightPoint;

    private bool right = false;

    public Vector2 pivotPoint = Vector2.zero;
    private Vector2 startPoint = Vector2.zero;
    private Vector2 velocityHolder;
    public float range = 7.5f;
    public float angle = 225.0f;

    [SerializeField]
    private LayerMask whatIsPlayer;

    private int health = 3;

    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();

        originalPositionR = new Vector2(rightPoint.transform.position.x, transform.position.y);
        originalPositionL = new Vector2(leftPoint.transform.position.x, transform.position.y);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (attack)
        {
            attackMovement();
        }
        else if (patrol)
        {
            PatrolMovement();
        }
        else if (still)
        {

        }
        else
        {
            returnToStart();
        }
    }

    void detectPlayer()
    {
        if (right)
        {
            angle = 315f;
        }
        else
        {
            angle = 225f;
        }
        startPoint = (Vector2)transform.position + pivotPoint; // Update starting ray point.

        // Direct use.
        // Get normalized (of length = 1) distance vector.
        // Vector2 direction = new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad)).normalized; 

        // Using function.
        Vector2 direction = GetDirectionVector2D(angle);

        RaycastHit2D hitInfo1 = Physics2D.Raycast(startPoint, direction, range, whatIsPlayer); // Shot ray.

        // Draw ray. For Debug we have to multiply our direction vector. 
        // Even if there is said Debug.DrawRay(start, dir), not Debug.DrawRay(start, end). Keep that in mind.
        //Debug.DrawRay(startPoint, direction * range, Color.red);

        if (hitInfo1.collider != null)
        {
            rb.velocity = new Vector2(0, 0);
            animator.SetBool("attack", true);
            patrol = false;
            still = true;
        }
    }

    void startAttack()
    {
        still = false;
        attack = true;
    }

    public Vector2 GetDirectionVector2D(float angle)
    {
        return new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad)).normalized;
    }

    void PatrolMovement()
    {
        if (transform.position.x > originalPositionR.x && right)
        {
            right = false;
            Flip();
        }
        else if (transform.position.x < originalPositionL.x && !right)
        {
            Flip();
            right = true;
        }

        if (transform.position.y > originalPositionL.y && up)
        {
            up = !up;
        }
        else if (transform.position.y < originalPositionL.y - 2 && !up)
        {
            up = !up;
        }

        if (right)
        {
            if (up)
            {
                velocityHolder = new Vector2(1.5f, 1.5f);
            }
            else
            {
                velocityHolder = new Vector2(1.5f, -1.5f);
            }
        }
        else
        {
            if (up)
            {
                velocityHolder = new Vector2(-1.5f, 1.5f);
            }
            else
            {
                velocityHolder = new Vector2(-1.5f, -1.5f);
            }
        }

        rb.velocity = velocityHolder;

        detectPlayer();
    }

    void attackMovement()
    {
        if (right)
        {
            velocityHolder = new Vector2(5f, -5f);
            rb.velocity = velocityHolder;
        }
        else
        {
            velocityHolder = new Vector2(-5f, -5f);
            rb.velocity = velocityHolder;
        }
    }

    void returnToStart()
    {
        if (right)
        {
            transform.position = Vector2.MoveTowards(new Vector2(transform.position.x, transform.position.y), originalPositionR, 3f * Time.deltaTime);
        }
        else
        {
            transform.position = Vector2.MoveTowards(new Vector2(transform.position.x, transform.position.y), originalPositionL, 3f * Time.deltaTime);
        }

        if (transform.position.x <= originalPositionR.x && right)
        {
            patrol = true;
            gameObject.layer = 21;
        }
        else if (transform.position.x >= originalPositionL.x && !right)
        {
            patrol = true;
            gameObject.layer = 21;
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        attack = false;
        still = false;
        animator.SetBool("attack", false);
        rb.velocity = new Vector2(0, 0);
        gameObject.layer = 29;
    }

    private void Flip()
    {
        rb.velocity = new Vector2(0, 0);
        transform.Rotate(0.0f, 180.0f, 0.0f);
    }

}
