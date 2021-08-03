using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : MonoBehaviour
{
    public LayerMask ground;
    public LayerMask whatIsPlayer;
    private GameObject player;
    public GameObject itself;

    private Vector3 raycastStartPosition;

    RaycastHit2D playerDetectionRight;
    RaycastHit2D playerDetectionLeft;
    RaycastHit2D cliffDetection;
    RaycastHit2D wallDetection;

    private float timer = 0;
    private float timeout = 2;
    private float movementSpeed = 2.5f;
    private float jumpForce = 4.5f;
    private float distance = 1.5f;

    private int jumpCount = 0;
    private int health = 3;

    private Animator animator;
    private Rigidbody2D rb;

    private bool right = true;
    private bool chasePlayer = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        player = GameObject.Find("Player");
    }

    void FixedUpdate()
    {
        detectPlayer();

        if (timer > timeout)
        {
            if (chasePlayer)
            {
                chasePlayerMovement(transform.position.x < player.transform.position.x);
            }
            else
            {
                move();
            }
        }

        timer += Time.deltaTime;

        animator.SetFloat("movementSpeed", rb.velocity.x);

    }

    private void chasePlayerMovement(bool direction) // true = right, maybe constant chase until out of range create varaible like spiker for that
    {
        if (direction)
        {
            if (!right)
            {
                right = !right;
                movementSpeed = movementSpeed * -1;
                Flip();
            }
        }
        else
        {
            if (right)
            {
                right = !right;
                movementSpeed = movementSpeed * -1;
                Flip();
            }
        }

        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        rb.AddForce(Vector2.right * movementSpeed, ForceMode2D.Impulse);

        timer = 0;

    }

    private void move()
    {
        if (right)
        {
            raycastStartPosition = new Vector3(transform.position.x + 2.5f, transform.position.y, transform.position.z);
            cliffDetection = Physics2D.Raycast(raycastStartPosition, Vector3.down, distance, ground); //change transform position for cliff detection ****************
            wallDetection = Physics2D.Raycast(transform.position, Vector2.right, 1.5f, ground);

        }
        else
        {
            raycastStartPosition = new Vector3(transform.position.x - 2.5f, transform.position.y, transform.position.z);
            cliffDetection = Physics2D.Raycast(raycastStartPosition, Vector3.down, distance, ground);
            wallDetection = Physics2D.Raycast(transform.position, Vector2.right, -1.5f, ground);
        }

        if (cliffDetection)
        {
            if (!wallDetection)
            {

                if (jumpCount == 5)
                {
                    jumpCount = 0;
                    Flip();
                    movementSpeed = movementSpeed * -1;
                    right = !right;
                }

                rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
                rb.AddForce(Vector2.right * movementSpeed, ForceMode2D.Impulse);

                jumpCount++;
            }
            else
            {
                right = !right;
                movementSpeed = movementSpeed * -1;
                jumpCount = 0;
                Flip();
                jumpCount++;
                rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
                rb.AddForce(Vector2.right * movementSpeed, ForceMode2D.Impulse);
            }
        }
        else
        {
            right = !right;
            movementSpeed = movementSpeed * -1;
            jumpCount = 0;
            Flip();
            jumpCount++;
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            rb.AddForce(Vector2.right * movementSpeed, ForceMode2D.Impulse);
        }

        timer = 0;
    }

    private void Flip()
    {
        transform.Rotate(0.0f, 180.0f, 0.0f);
    }

    private void detectPlayer()
    {
        playerDetectionRight = Physics2D.Raycast(transform.position, Vector2.right, 5, whatIsPlayer);
        playerDetectionLeft = Physics2D.Raycast(transform.position, Vector2.left, 5, whatIsPlayer);

        if (playerDetectionLeft.collider != null || playerDetectionRight.collider != null)
        {
            chasePlayer = true;
        }

        stopAttack();
    }

    private void stopAttack()
    {
        if (Mathf.Abs(player.transform.position.x) - Mathf.Abs(transform.position.x) > 5 || Mathf.Abs(player.transform.position.x) - Mathf.Abs(transform.position.x) < -5)
        {
            chasePlayer = false;
        }
    }
}
