using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragPlayerLadder : MonoBehaviour
{
    private PlayerMovement player;

    void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<PlayerMovement>();
    }

    private void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.name == "Player")
        {
            if (player.isClimbing)
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
