using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragPlayerPlatform : MonoBehaviour
{

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            col.collider.transform.SetParent(transform);

        }
    }

    private void OnCollisionExit2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            col.collider.transform.SetParent(null);
        }
    }
}
