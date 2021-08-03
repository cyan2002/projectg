using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChainGUI : MonoBehaviour
{
    private SpriteRenderer spriteR;
    public bool startVisible;

    void Start()
    {
        spriteR = GetComponent<SpriteRenderer>();
        if (!startVisible)
        {
            spriteR.enabled = false;
        }
    }

    void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.layer == 23)
        {
            spriteR.enabled = true;
        }
        else if (col.gameObject.layer == 16)
        {
            spriteR.enabled = false;
        }
    }
}
