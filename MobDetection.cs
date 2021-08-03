using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobDetection : MonoBehaviour
{
    public bool chasePlayer = false;
    private bool canSwitch = true;

    public float range;
    private GameObject player;

    void Start()
    {
        player = GameObject.Find("Player");
    }

    void FixedUpdate()
    {
        if (Vector3.Distance(player.transform.position, transform.position) >= range)
        {
            chasePlayer = false;
        }
        else
        {
            chasePlayer = true;
        }
    }
}
