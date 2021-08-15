using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateBox : MonoBehaviour
{
    public bool toggle = false;
    private bool activate = false;
    private bool canSwitch = true;

    void OnTriggerStay2D(Collider2D col)
    {
        if (toggle)
        {
            if (canSwitch)
            {
                if (Input.GetKey(KeyCode.E) && !activate)
                {
                    activate = true;
                    canSwitch = false;
                }
                else if (Input.GetKey(KeyCode.E) && activate)
                {
                    activate = false;
                    canSwitch = false;
                }
            }
        }
        else
        {
            activate = true;
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if(!toggle)
        {
            activate = false;
        }
    }

    public bool checkActivation()
    {
        return activate;
    }

    private void finishAnimation()
    {
        canSwitch = true;
    }
}
