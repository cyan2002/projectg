using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateBox : MonoBehaviour
{
    public bool toggle = false;
    private bool activate = false;

    void OnTriggerStay2D(Collider2D col)
    {
        if(col.gameObject.name == "Player")
        {
            if(toggle)
            {
                if (Input.GetKey(KeyCode.E) && !activate)
                {
                    activate = true;
                }
                else if (Input.GetKey(KeyCode.E) && activate)
                {
                    activate = false;
                }
            }
            else
            {
                activate = true;
            }
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if(!toggle)
        {
            if(col.gameObject.name == "Player")
            {
                activate = false;
            }
        }
    }

    public bool checkActivation()
    {
        return activate;
    }
}
