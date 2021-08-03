using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MechanicalLift : MonoBehaviour
{
    public ActivateBox activate;

    public float downwardsMovement = 2f;
    public float upwardsMovement = 2f;

    public bool up = true;
    public bool on = false;
    public float blocksAboveorBelow;
    
    private Vector3 originalPosition;

    void Start()
    {
        originalPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (activate.checkActivation())
        {
            if (up)
            {
                if (transform.position.y <= blocksAboveorBelow + originalPosition.y)
                {
                    transform.position = new Vector3(transform.position.x, transform.position.y + (upwardsMovement * Time.deltaTime), transform.position.z);
                }
            }
            else
            {
                if (transform.position.y >= blocksAboveorBelow + originalPosition.y)
                {
                    transform.position = new Vector3(transform.position.x, transform.position.y - (downwardsMovement * Time.deltaTime), transform.position.z);
                }
            }

        }
        else
        {
            if (up)
            {
                if (transform.position.y >= originalPosition.y)
                {
                    transform.position = new Vector3(transform.position.x, transform.position.y - (downwardsMovement * Time.deltaTime), transform.position.z);
                }
            }
            else
            {
                if (transform.position.y <= originalPosition.y)
                {
                    transform.position = new Vector3(transform.position.x, transform.position.y + (downwardsMovement * Time.deltaTime), transform.position.z);
                }
            }
        }


    }
}
