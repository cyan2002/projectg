using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapDoor : MonoBehaviour
{
    private Animator animation;
    public BoxCollider2D box;

    void Start()
    {
        box.enabled = false;
        animation = GetComponent<Animator>();
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.name == "Player")
        {
            box.enabled = true;
            animation.Play("TrapDoorAnimation");
        }
    }
}
