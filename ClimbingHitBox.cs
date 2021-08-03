using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClimbingHitBox : MonoBehaviour
{
    public Collider2D col;
    public Collider2D col2;
    private PlayerMovement cc;

    void Start()
    {
        cc = GameObject.FindWithTag("Player").GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (cc.isClimbing)
        {
            col.enabled = false;
            col2.enabled = false;
        }
        else
        {
            col.enabled = true;
            col2.enabled = true;
        }
    }
}
