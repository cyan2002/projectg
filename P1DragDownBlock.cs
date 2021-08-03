using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P1PuzzelDragdownBlocks : MonoBehaviour
{
    public bool up = false;
    public bool on = false;
    public Vector3 endPoint;

    void FixedUpdate()
    {
        if (on)
        {
            if (up)
            {
                if (transform.position.y >= endPoint.y)
                {
                    transform.position = new Vector3(transform.position.x, transform.position.y + (1f * Time.deltaTime), transform.position.z);
                    Debug.Log("Here!");
                }
            }
            else
            {
                if (transform.position.y >= endPoint.y)
                {
                    transform.position = new Vector3(transform.position.x, transform.position.y - (1f * Time.deltaTime), transform.position.z);
                }
            }
        }
    }
}