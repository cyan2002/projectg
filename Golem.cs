using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Golem : MonoBehaviour
{
    private bool right = true;
    private bool attack = false;

    private Rigidbody2D rb;
    private Animator animator;
    public LayerMask player;

    private Vector3 raycastStartPosition;
    private Vector2 velocityHolder;

    private RaycastHit2D playerDetectRight;
    private RaycastHit2D playerDetectLeft;

    public Transform middle;

    private GameObject playerPosition;
    public GameObject originalPositionR;
    public GameObject originalPositionL;

    public GameObject arm1;
    public GameObject arm2;

    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        playerPosition = GameObject.Find("Player");
    }

    void FixedUpdate()
    {
        if (attack)
        {

        }
        else
        {
            patrolMovement();
        }
    }

    void spotPlayer()
    {
        if (right)
        {
            if (playerPosition.transform.position.x > middle.position.x && Mathf.Abs(playerPosition.transform.position.y - middle.position.y) < 1f && Mathf.Abs(playerPosition.transform.position.x - middle.position.x) < 3f)
            {
                changeTag(true);
                attack = true;
                animator.SetBool("isWalking", false);
                animator.SetBool("Attack", true);
            }
        }
        else
        {
            if (playerPosition.transform.position.x < middle.position.x && Mathf.Abs(playerPosition.transform.position.y - middle.position.y) < 1f && Mathf.Abs(playerPosition.transform.position.x - middle.position.x) < 3f)
            {
                changeTag(true);
                attack = true;
                animator.SetBool("isWalking", false);
                animator.SetBool("Attack", true);
            }
        }
    }

    void patrolMovement()
    {
        if (transform.position.x > originalPositionR.transform.position.x && right)
        {
            Flip();
            right = false;
        }
        else if (transform.position.x < originalPositionL.transform.position.x && !right)
        {
            Flip();
            right = true;
        }

        if (right)
        {
            velocityHolder = new Vector2(1f, 0);
        }
        else
        {
            velocityHolder = new Vector2(-1f, 0);
        }

        rb.velocity = velocityHolder;
    }

    void stopAttack()
    {
        changeTag(false);
        animator.SetBool("Attack", false);
        animator.SetBool("isWalking", true);
        attack = false;
    }

    void changeTag(bool on)
    {
        if (on)
        {
            gameObject.layer = 6;
            arm1.layer = 6;
            arm2.layer = 6;
        }
        else
        {
            gameObject.layer = 10;
            arm1.layer = 10;
            arm2.layer = 10;
        }
    }

    private void Flip()
    {
        transform.Rotate(0.0f, 180.0f, 0.0f);
    }

}
