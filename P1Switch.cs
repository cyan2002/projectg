using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P1Switch : MonoBehaviour
{

    public int counter = 0;

    private Animator animator;
    private bool coolDown = true;
    private bool allow = false;
    private bool down = false;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (allow)
        {
            if (Input.GetKeyDown(KeyCode.E) && !down)
            {
                if (coolDown)
                {
                    animator.SetBool("Flick", true);
                    coolDown = false;
                    counter++;
                    down = true;
                }
            }
            else if (Input.GetKeyDown(KeyCode.E) && down)
            {
                if (coolDown)
                {
                    animator.SetBool("Flick", false);
                    coolDown = false;
                    counter++;
                    down = false;
                }

            }
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.name == "Player")
        {
            allow = true;
        }
    }

    void OnTriggerExit2D()
    {
        allow = false;
    }

    void finishAnimation()
    {
        coolDown = true;
    }

}
