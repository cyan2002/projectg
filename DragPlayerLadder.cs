using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragPlayerLadder : MonoBehaviour
{

    public PlayerMovement CC;

    private void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.name == "Player")
        {
            if (CC.isClimbing)
            {

                col.transform.SetParent(transform);
            }
            else
            {
                col.transform.SetParent(null);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.name == "Player")
        {
            col.transform.SetParent(null);
        }
    }
}
