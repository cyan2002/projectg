using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goblin : MonoBehaviour
{

    [SerializeField]
    private LayerMask whatIsWall;

    private Vector3 raycastStart;
    private Vector3 raycastStart2;
    private Vector3 raycastStart3;

    private Animator animator;

    private float holdSpeed;
    private float Dice;
    private float movementSpeed = 2.75f;

    private bool startWait = false;
    private bool Once = true;
    private bool endOfAnimation = false;
    private bool canMove = true;
    private bool right = true;

    private int counter = 0;
    private int random;
    private int animationPlay;

    void Start()
    {
        animator = GetComponent<Animator>();
        rollDice();
    }

    private void rollDice()
    {
        Dice = Random.Range(0.0f, 110.0f);

        if (Dice <= 10.0f)
        {
            animationPlay = 1;
        }
        else if (Dice <= 35.0f)
        {
            animationPlay = 3;
        }
        else if (Dice <= 60.0f)
        {
            animationPlay = 5;
        }
        else if (Dice <= 85.0f)
        {
            animationPlay = 7;
        }
        else
        {
            animationPlay = 8;
        }

    }

    void FixedUpdate()
    {

        if (canMove)
        {

            if (Once)
            {

                StartCoroutine(waitTimeGo(animationPlay));
                Once = false;
            }

            move();

            if (startWait)
            {
                rollDice();
                StartCoroutine(waitTimeStop());
                startWait = false;
            }


        }
    }

    private void Flip()
    {
        transform.Rotate(0.0f, 180.0f, 0.0f);
    }

    private IEnumerator waitTimeStop()
    {
        canMove = false;
        animator.SetBool("GoblinWalk", false);
        yield return new WaitForSeconds(5f);
        animator.SetBool("GoblinWalk", true);
        canMove = true;
        Once = true;
    }

    private IEnumerator waitTimeGo(float waitTime)
    {
        yield return new WaitForSeconds(waitTime * 1.25f);

        startWait = true;
    }

    private void move()
    {
        raycastStart = new Vector3(transform.position.x, transform.position.y + .25f, transform.position.z);
        raycastStart2 = new Vector3(transform.position.x + .5f, transform.position.y, transform.position.z);
        raycastStart3 = new Vector3(transform.position.x - .5f, transform.position.y, transform.position.z);
        if (right)
        {
            RaycastHit2D hitInfo1 = Physics2D.Raycast(raycastStart, Vector2.right, .5f, whatIsWall);
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
            RaycastHit2D hitInfo2 = Physics2D.Raycast(raycastStart, Vector2.right, -.5f, whatIsWall);
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
        transform.position = transform.position + new Vector3(movementSpeed * Time.deltaTime, 0f, 0f);
    }
}
