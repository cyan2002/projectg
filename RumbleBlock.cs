using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RumbleBlock : MonoBehaviour
{

    public CameraShake camShake;
    public bool StalactiteFall = false;
    public int counter = 0;

    void OnTriggerEnter2D(Collider2D col) //change so it activates after player or mob touches
    {
        if (col.gameObject.name == "Player" || col.gameObject.name == "Boss")
        {
            StalactiteFall = true;
            StartCoroutine(camShake.Shake(.15f, .4f));
            counter++;
        }
    }
}
