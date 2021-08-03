using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float movementSpeed;
    [SerializeField]
    private float groundCheckRadius;
    [SerializeField]
    private float jumpForce;
    [SerializeField]
    private float slopeCheckDistance;
    [SerializeField]
    private float maxSlopeAngle;
    [SerializeField]
    private Transform groundCheck;
    [SerializeField]
    private LayerMask whatIsGround;
    [SerializeField]
    private LayerMask whatIsRungsR;
    [SerializeField]
    private LayerMask whatIsRungsL;
    [SerializeField]
    private LayerMask whatIsLadder;
    [SerializeField]
    private PhysicsMaterial2D noFriction;
    [SerializeField]
    private PhysicsMaterial2D fullFriction;

    private float xInput;
    private float slopeDownAngle;
    private float slopeSideAngle;
    private float lastSlopeAngle;
    private float distance;
    private float up;

    private int facingDirection = 1;

    public bool isClimbing;
    private bool isGrounded;
    private bool isOnSlope;
    private bool isJumping;
    private bool canWalkOnSlope;
    private bool canJump;
    private bool circle;
    private bool ladder;
    private bool rung;
    private bool lookingRight;
    private bool lookingLeft;
    private bool canTurn;
    private bool canReleaseShot = false;
    private bool canUseVelocity = true;
    private bool canMove = true;
    private bool isShooting = false;
    private static bool equipped = false;
    private bool lowShot = false;

    private Vector2 newVelocity;
    private Vector2 newForce;
    private Vector2 capsuleColliderSize;
    private Vector2 slopeNormalPerp;
    private static Vector2 startPos = new Vector2(0, 0);

    private Rigidbody2D rb;
    private CapsuleCollider2D cc;
    private Animator animator;

    public Transform firePoint;
    public GameObject slingShotBullet;

    private void Start()
    {
        canTurn = true;
        rb = GetComponent<Rigidbody2D>();
        cc = GetComponent<CapsuleCollider2D>();
        animator = GetComponent<Animator>();
        capsuleColliderSize = cc.size;
        transform.position = new Vector3(startPos.x, startPos.y, 0);
    }

    private void Update()
    {
        CheckInput();
    }

    private void FixedUpdate()
    {
        if(canMove)
        {
            CheckGround();
            SlopeCheck();
            ApplyMovement();
        }
    }

    private void CheckInput()
    {
        if (!isShooting && canMove)
        {
            if (canTurn)
            {
                xInput = Input.GetAxisRaw("Horizontal");
                animator.SetFloat("Speed", Mathf.Abs(xInput));
            }

            if (Input.GetButtonDown("Jump"))
            {
                Jump();
            }

            if (xInput == 1 && facingDirection == -1)
            {
                Flip();
            }
            else if (xInput == -1 && facingDirection == 1)
            {
                Flip();
            }

            checkClimb();
        }

        if (equipped) // from weaponScript
        {
            if (Input.GetMouseButtonDown(0) && !isClimbing)
            {
                startShot();
            }

            if (Input.GetMouseButtonUp(0))
            {
                startRelease();
            }

            if(Input.GetKey(KeyCode.R) && !lowShot)
            {
                lowShot = true;
            }
            else if(Input.GetKey(KeyCode.R) && lowShot)
            {
                lowShot = false;
            }
        }

    }

    public void stopMovement()
    {
        canMove = false;
    }

    private void Shoot()
    {
        if (Input.GetKey(KeyCode.S) && lowShot)
        {
            Vector3 newPosition = new Vector3(firePoint.position.x, firePoint.position.y - .5f, firePoint.position.z);
            Instantiate(slingShotBullet, newPosition, firePoint.rotation);
        }
        else
        {
            Instantiate(slingShotBullet, firePoint.position, firePoint.rotation);
        }
    }

    private void startShot()
    {
        isShooting = true;
        xInput = 0;
        animator.SetFloat("Speed", Mathf.Abs(xInput));
        animator.SetBool("IsShooting", true);
        canReleaseShot = false;
    }

    private void endShootHold()
    {
        animator.SetBool("IsShooting", false);
    }

    private void startRelease()
    {
        animator.SetBool("IsReleased", true);
    }

    private void endShootRelease()
    {
        animator.SetBool("IsReleased", false);
        isShooting = false;
        Shoot();
    }

    private void checkClimb()
    {
        RaycastHit2D hitInfo1 = Physics2D.Raycast(transform.position, Vector2.up, distance, whatIsLadder);
        RaycastHit2D hitInfo2 = Physics2D.Raycast(transform.position, Vector2.up, distance, whatIsRungsR);
        RaycastHit2D hitInfo3 = Physics2D.Raycast(transform.position, Vector2.up, distance, whatIsRungsL);

        if (hitInfo1.collider != null)
        {
            if (Input.GetKeyDown(KeyCode.E) && !isClimbing)
            {
                isClimbing = true;
                ladder = true;
            }
            else if (Input.GetKeyDown(KeyCode.E) && isClimbing)
            {
                animator.SetBool("LadderClimb", false);
                isClimbing = false;
                ladder = false;
                canTurn = true;
            }
        }
        else if (hitInfo2.collider != null)
        {
            if (Input.GetKeyDown(KeyCode.E) && !isClimbing)
            {
                isClimbing = true;
                rung = true;
                if (Mathf.Round(transform.rotation.eulerAngles.y) == 180)
                {
                    Flip();
                }
            }
            else if (Input.GetKeyDown(KeyCode.E) && isClimbing)
            {
                animator.SetBool("RungClimb", false);
                isClimbing = false;
                rung = false;
                canTurn = true;
            }
        }
        else if (hitInfo3.collider != null)
        {
            if (Input.GetKeyDown(KeyCode.E) && !isClimbing)
            {
                isClimbing = true;
                rung = true;
                if (Mathf.Round(transform.rotation.eulerAngles.y) == 0)
                {
                    Flip();
                }
            }
            else if (Input.GetKeyDown(KeyCode.E) && isClimbing)
            {
                animator.SetBool("RungClimb", false);
                isClimbing = false;
                rung = false;
                canTurn = true;
            }
        }
        else
        {
            animator.SetBool("LadderClimb", false);
            animator.SetBool("RungClimb", false);
            isClimbing = false;
            ladder = false;
            rung = false;
            canTurn = true;
        }

        if (isClimbing)
        {
            if (Input.GetKey(KeyCode.W))
            {
                up = 2;
            }
            else if (Input.GetKey(KeyCode.S))
            {
                up = 1;
            }
            else
            {
                up = 0;
            }

            if (Input.GetButtonDown("Jump"))
            {
                animator.SetBool("LadderClimb", false);
                animator.SetBool("RungClimb", false);
                //animator.SetBool("IsJumping", true);
                isClimbing = false;
                rung = false;
                ladder = false;
                canJump = true;
                canTurn = true;
                Jump();
            }
        }
    }

    private void CheckGround()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);
        /*
        if (rb.velocity.y == 0.0f)
        {
            if (isJumping)//add facing up frame
            {
                isJumping = false;
            }
        }
        */


        if (isGrounded && slopeDownAngle <= maxSlopeAngle && !isJumping)// && !isJumping
        {
            canJump = true;
        }
        //else //this controls jump off and then jump
        //{
        //canJump = false;
        //}

        /*if (isJumping)
        {
            if (!isGrounded)
            {
                OnLandEvent.Invoke();
            }
        }*/
    }

    private void SlopeCheck()
    {
        Vector2 checkPos = transform.position - (Vector3)(new Vector2(0.0f, capsuleColliderSize.y / 2));

        SlopeCheckHorizontal(checkPos);
        SlopeCheckVertical(checkPos);
    }

    private void SlopeCheckHorizontal(Vector2 checkPos)
    {
        RaycastHit2D slopeHitFront = Physics2D.Raycast(checkPos, transform.right, slopeCheckDistance, whatIsGround);
        RaycastHit2D slopeHitBack = Physics2D.Raycast(checkPos, -transform.right, slopeCheckDistance, whatIsGround);

        if (slopeHitFront)
        {
            isOnSlope = true;
            slopeSideAngle = Vector2.Angle(slopeHitFront.normal, Vector2.up);

        }
        else if (slopeHitBack)
        {
            isOnSlope = true;
            slopeSideAngle = Vector2.Angle(slopeHitBack.normal, Vector2.up);
        }
        else
        {
            slopeSideAngle = 0.0f;
            isOnSlope = false;
        }

    }

    private void SlopeCheckVertical(Vector2 checkPos)
    {
        RaycastHit2D hit = Physics2D.Raycast(checkPos, Vector2.down, slopeCheckDistance, whatIsGround);

        if (hit)
        {
            slopeNormalPerp = Vector2.Perpendicular(hit.normal).normalized;

            slopeDownAngle = Vector2.Angle(hit.normal, Vector2.up);

            if (slopeDownAngle != lastSlopeAngle)
            {
                isOnSlope = true;
            }

            lastSlopeAngle = slopeDownAngle;

            Debug.DrawRay(hit.point, slopeNormalPerp, Color.blue);
            Debug.DrawRay(hit.point, hit.normal, Color.green);

        }

        if (slopeDownAngle > maxSlopeAngle || slopeSideAngle > maxSlopeAngle)
        {
            canWalkOnSlope = false;
        }
        else
        {
            canWalkOnSlope = true;
        }

        if (isOnSlope && canWalkOnSlope && xInput == 0.0f)
        {
            rb.sharedMaterial = fullFriction;
        }
        else
        {
            rb.sharedMaterial = noFriction;
        }
    }

    private void Jump()
    {
        if (canJump)
        {
            isJumping = true;
            newVelocity.Set(0.0f, 0.0f);
            rb.velocity = newVelocity;
            newForce.Set(0.0f, jumpForce);
            rb.AddForce(newForce, ForceMode2D.Impulse);
            canJump = false;
        }
    }

    private void ApplyMovement()
    {
        if (isClimbing)
        {
            canTurn = false;
            transform.position = new Vector3(Mathf.FloorToInt(transform.position.x) + .5f, transform.position.y, transform.position.z);
            rb.gravityScale = 0;

            if (ladder)
            {
                animator.SetBool("LadderClimb", true);
                //player animation for ladder climbing
            }
            if (rung)
            {
                animator.SetBool("RungClimb", true);
                //player animation for rung climbing and position character in one set direction
            }

            if (up == 2)
            {
                animator.SetFloat("ClimbSpeed", 5f);
                newVelocity.Set(0.0f, 5f);
                rb.velocity = newVelocity;
                rb.AddForce(Vector2.up * 5f);
            }
            else if (up == 1)
            {
                animator.SetFloat("ClimbSpeed", 5f);
                newVelocity.Set(0.0f, -5f);
                rb.velocity = newVelocity;
            }
            else if (up == 0)
            {
                animator.SetFloat("ClimbSpeed", 0);
                newVelocity.Set(0f, 0f);
                rb.velocity = newVelocity;
            }
        }
        else
        {
            rb.gravityScale = 3;

            //script thinks it is on slope against a wall

            if (isGrounded && isOnSlope && canWalkOnSlope && !isJumping && canUseVelocity) //If on slope
            {
                newVelocity.Set(movementSpeed * slopeNormalPerp.x * -xInput, movementSpeed * slopeNormalPerp.y * -xInput);
                rb.velocity = newVelocity;
            }
            else if (isGrounded  && !isJumping && canUseVelocity) //if not on slope && !isOnSlope
            {
                newVelocity.Set(movementSpeed * xInput, 0.0f);
                rb.velocity = newVelocity;
            }
            else if (!isGrounded && canUseVelocity) //If in air
            {
                newVelocity.Set(movementSpeed * xInput, rb.velocity.y);
                rb.velocity = newVelocity;
            }
        }
    }

    private void OnCollisionEnter2D()
    {
        if(isJumping)
        {
            isJumping = false;
        }
    }

    public void setCheckPoint(Vector2 pos)
    {
        startPos = pos;
    }

    private void Flip()
    {
        facingDirection *= -1;
        transform.Rotate(0.0f, 180.0f, 0.0f);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
    }
}
