using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gnomes : MonoBehaviour
{
    private Vector3 raycastStart;
    private Vector3 raycastStart2;
    private Vector3 raycastStart3;
    private Vector3 raycastStart4;

    [SerializeField]
    private LayerMask whatIsWall;
    [SerializeField]
    private LayerMask whatIsPlayer;

    private bool right = true;
    private bool attackMode = false;
    private bool startAttack = false;
    private bool canTransition = true;
    public bool isGrounded = false;

    private float movementSpeed = 2.5f;
    private float jumpForce = 4.5f;

    private int health = 3;

    public MobDetection mD;
    private GameObject Player;
    public GameObject itself;

    private Rigidbody2D rb;
    private Animator animator;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        Player = GameObject.Find("Player");
    }

    void FixedUpdate()
    {
        if (detectPlayer())
        {
            if (canTransition)
            {
                canTransition = false;//returns back to true once it turns to walking
                StartCoroutine(waitTimeGo());
            }
        }

        if (attackMode)
        {
            gameObject.layer = 6;
            if (startAttack)
            {
                if (!mD.chasePlayer)
                {
                    animator.SetBool("startRun", false);
                    animator.SetBool("returnWalk", true);

                }
                else
                {
                    attackMove();
                }
            }
        }
        else
        {
            move();
        }
    }

    private void attackMove()
    {
        if (Player.transform.position.x > transform.position.x && !right)
        {
            movementSpeed = movementSpeed * -1;
            right = !right;
            Flip();
        }
        if (Player.transform.position.x < transform.position.x && right)
        {
            movementSpeed = movementSpeed * -1;
            right = !right;
            Flip();
        }

        raycastStart = new Vector3(transform.position.x, transform.position.y + 1f, transform.position.z);
        raycastStart2 = new Vector3(transform.position.x + .5f, transform.position.y, transform.position.z);
        raycastStart3 = new Vector3(transform.position.x - .5f, transform.position.y, transform.position.z);

        if (right)
        {
            RaycastHit2D hitInfo1 = Physics2D.Raycast(raycastStart, Vector2.right, .75f, whatIsWall);//detects wall jump
            RaycastHit2D hitInfo3 = Physics2D.Raycast(raycastStart2, Vector2.down, 3f, whatIsWall);//detects cliff
            RaycastHit2D hitInfo5 = Physics2D.Raycast(transform.position, Vector2.right, .75f, whatIsWall);

            if (hitInfo1.collider != null)
            {
                movementSpeed = movementSpeed * -1;
                right = false;
                Flip();
            }

            if (hitInfo1.collider == null && hitInfo5.collider != null)
            {
                Jump();
            }

            if (hitInfo3.collider == null)
            {
                Jump();
            }

        }
        else
        {
            RaycastHit2D hitInfo2 = Physics2D.Raycast(raycastStart, Vector2.right, -.75f, whatIsWall);
            RaycastHit2D hitInfo4 = Physics2D.Raycast(raycastStart3, Vector2.down, 3f, whatIsWall);
            RaycastHit2D hitInfo6 = Physics2D.Raycast(transform.position, Vector2.right, -.75f, whatIsWall);

            if (hitInfo2.collider != null)
            {
                movementSpeed = movementSpeed * -1;
                right = true;
                Flip();
            }

            if (hitInfo2.collider == null && hitInfo6.collider != null)
            {
                Jump();
            }

            if (hitInfo4.collider == null)
            {
                Jump();
            }

        }
        rb.velocity = new Vector2(movementSpeed, rb.velocity.y);
    }

    private void startWalk()
    {
        gameObject.layer = 15;
        itself.tag = "Untagged";
        animator.SetBool("startPrep", false);
        animator.SetBool("startTurn", false);
        animator.SetBool("startRun", false);
        animator.SetBool("returnWalk", false);
        animator.SetBool("startWalk", true);
        attackMode = false;
        startAttack = false;
        canTransition = true;
        Flip();

    }

    private void startTurn()
    {
        animator.SetBool("startPrep", false);
        animator.SetBool("startTurn", true);
    }

    private void startRunning()
    {
        animator.SetBool("startTurn", false);
        animator.SetBool("startRun", true);
        startAttack = true;
        right = !right;
        movementSpeed = movementSpeed * -1;
    }

    private IEnumerator waitTimeGo()
    {
        yield return new WaitForSeconds(1f);
        attackMode = true;
        animator.SetBool("startWalk", false);
        animator.SetBool("startPrep", true);
    }

    private bool detectPlayer()
    {
        raycastStart = new Vector3(transform.position.x, transform.position.y + .25f, transform.position.z);
        if (right)
        {
            RaycastHit2D detectRight = Physics2D.Raycast(raycastStart, Vector2.right, 1f, whatIsPlayer);
            if (detectRight.collider != null)
            {
                return true;
            }
        }
        else
        {
            RaycastHit2D detectLeft = Physics2D.Raycast(raycastStart, Vector2.right, -1f, whatIsPlayer);

            if (detectLeft.collider != null)
            {
                return true;
            }
        }
        return false;
    }

    private void move()
    {
        raycastStart = new Vector3(transform.position.x, transform.position.y + 1f, transform.position.z);
        raycastStart2 = new Vector3(transform.position.x + .5f, transform.position.y, transform.position.z);
        raycastStart3 = new Vector3(transform.position.x - .5f, transform.position.y, transform.position.z);

        if (right)
        {
            RaycastHit2D hitInfo1 = Physics2D.Raycast(raycastStart, Vector2.right, 1f, whatIsWall);//detects wall jump
            RaycastHit2D hitInfo3 = Physics2D.Raycast(raycastStart2, Vector2.down, 3f, whatIsWall);//detects cliff
            RaycastHit2D hitInfo5 = Physics2D.Raycast(transform.position, Vector2.right, 1f, whatIsWall);

            if (hitInfo1.collider != null)
            {
                movementSpeed = movementSpeed * -1;
                right = false;
                Flip();
            }

            if (hitInfo1.collider == null && hitInfo5.collider != null)
            {
                Jump();
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
            RaycastHit2D hitInfo2 = Physics2D.Raycast(raycastStart, Vector2.right, -.75f, whatIsWall);
            RaycastHit2D hitInfo4 = Physics2D.Raycast(raycastStart3, Vector2.down, 3f, whatIsWall);
            RaycastHit2D hitInfo6 = Physics2D.Raycast(transform.position, Vector2.right, -.75f, whatIsWall);

            if (hitInfo2.collider != null)
            {
                movementSpeed = movementSpeed * -1;
                right = true;
                Flip();
            }

            if (hitInfo2.collider == null && hitInfo6.collider != null)
            {
                Jump();
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

    private void Jump()
    {
        if (isGrounded)
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }

    private void Flip()
    {
        transform.Rotate(0.0f, 180.0f, 0.0f);
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Ground")
        {
            isGrounded = true;
        }
    }

    void OnCollisionExit2D(Collision2D col)
    {
        if (col.gameObject.tag == "Ground")
        {
            isGrounded = false;
        }
    }
}
