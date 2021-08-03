using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformFallThrough : MonoBehaviour
{
    private PlatformEffector2D platform;
    private float waitTime = .5f;

    void Start()
    {
        platform = GetComponent<PlatformEffector2D>();
    }

    void Update()
    {

        if(Input.GetKeyUp(KeyCode.S))
        {
            waitTime = .5f;
        }

        if(Input.GetKey(KeyCode.S))
        {
            if(waitTime <= 0)
            {
                platform.rotationalOffset = 180f;
                waitTime = .5f;
            }
            else
            {
                waitTime -= Time.deltaTime;
            }
        }

        if(Input.GetKey(KeyCode.W) || Input.GetButtonDown("Jump"))
        {
            platform.rotationalOffset = 0f;
        }

    }
}
