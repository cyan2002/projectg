using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Track : MonoBehaviour
{
    public ActivateBox activate;

    public int direction = 0; // 1 - N, 2 - E, 3 - S, 4 - W
    private float blockAngle;
    private float distance = 1f;
    public float movementSpeed = 2.5f;
    private float turnBlockCoordinate;

    [SerializeField]
    private LayerMask whatIsTrack;

    public bool moving = false;
    private bool once = true;
    private bool startTurn = false;
    private bool xOry;

    private Vector2 drawingRaycast;
    private Vector3 raycaststartPosition;

    void Update()
    {
        if (activate.checkActivation())
        {
            moving = true;
        }


        if (moving)
        {
            if (direction == 1)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y + movementSpeed * Time.deltaTime, transform.position.z);
            }
            else if (direction == 2)
            {
                transform.position = new Vector3(transform.position.x + movementSpeed * Time.deltaTime, transform.position.y, transform.position.z);
            }
            else if (direction == 3)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y - movementSpeed * Time.deltaTime, transform.position.z);
            }
            else if (direction == 4)
            {
                transform.position = new Vector3(transform.position.x - movementSpeed * Time.deltaTime, transform.position.y, transform.position.z);
            }
            else
            {
                // Debug.Log("Error!");
            }
        }
    }

    public int findstartDirection(float angle)
    {
        int findDirection = 0;

        if (angle == 0)
        {
            findDirection = 1;
        }
        else if (angle == 90)
        {
            findDirection = 4;
        }
        else if (angle == 270)
        {
            findDirection = 2;
        }
        else
        {
            findDirection = 3;
        }


        return findDirection;

    }

    public int findDirection(float angle)
    {
        int findDirection = 0;

        //Debug.Log("Angle" + angle);

        if (direction == 1)//coming from north
        {
            if (angle == 0)
            {
                findDirection = 4;
            }
            else if (angle == 90)
            {
                findDirection = 2;
            }
            else
            {
                //Debug.Log("error or turn2");
            }
        }
        else if (direction == 2)
        {
            if (angle == 270)
            {
                findDirection = 1;
            }
            else if (angle == 0)
            {
                findDirection = 3;
            }
            else
            {
                // Debug.Log("error or turn2");
            }
        }
        else if (direction == 3)
        {
            if (angle == 270)
            {
                findDirection = 4;
            }
            else if (angle == 180)
            {
                findDirection = 2;

            }
            else
            {
                //Debug.Log("error or turn2");
            }
        }
        else if (direction == 4)
        {
            if (angle == 180)
            {
                findDirection = 1;
            }
            else if (angle == 90)
            {
                findDirection = 3;
            }
            else
            {
                //Debug.Log("error or turn2");
            }
        }
        else
        {
            //Debug.Log("Error");
        }

        return findDirection;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Special")
        {
            if (direction == 2 || direction == 4)
            {
                turnBlockCoordinate = col.gameObject.transform.position.x;
            }
            else
            {
                turnBlockCoordinate = col.gameObject.transform.position.y;
            }

            StartCoroutine(waitTime());
            blockAngle = col.gameObject.transform.rotation.eulerAngles.z;
        }
        else if (col.gameObject.tag == "Special2")
        {
            blockAngle = col.gameObject.transform.rotation.eulerAngles.z;
            direction = findstartDirection(blockAngle);
        }
    }

    void startTurnFunction()
    {
        direction = findDirection(blockAngle);
    }

    private bool readyToTurn()
    {
        if (direction == 1)
        {
            if (transform.position.y + .25f >= turnBlockCoordinate)
            {
                return true;
            }
        }
        else if (direction == 2)
        {
            if (transform.position.x >= turnBlockCoordinate)
            {
                return true;
            }
        }
        else if (direction == 3)
        {
            if (transform.position.y + .25f <= turnBlockCoordinate)
            {
                return true;
            }
        }
        else if (direction == 4)
        {
            if (transform.position.x <= turnBlockCoordinate)
            {
                return true;
            }
        }
        else
        {
            //Debug.Log("Error");
        }

        return false;
    }

    IEnumerator waitTime()
    {
        yield return new WaitUntil(readyToTurn);
        startTurnFunction();
    }
}
