using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformFallThrough : MonoBehaviour
{
    private float waitTime = .5f;

    [SerializeField]
    private LayerMask ignoreLayer;
    [SerializeField]
    private LayerMask whatIsGround;

    void Update()
    { 
        if(Input.GetKey(KeyCode.S))
        {
            gameObject.layer = 20;
        }

        if(Input.GetKey(KeyCode.W) || Input.GetButtonDown("Jump"))
        {
            gameObject.layer = 10;
        }

    }
}
