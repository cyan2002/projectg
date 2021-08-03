using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraStuff : MonoBehaviour
{
    private Transform player;

    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Transform>();
        transform.position = player.position;
    }

    void FixedUpdate()
    {
        if(player != null)
        {
            if (Input.GetKey(KeyCode.UpArrow))
            {
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(player.position.x, player.position.y + 2, -10f), 1.0f);
            }
            else if (Input.GetKey(KeyCode.DownArrow))
            {
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(player.position.x, player.position.y - 2, -10f), 1.0f);
            }
            else
            {
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(player.position.x, player.position.y, -10f), 1.0f);
            }
        }
    }
}
