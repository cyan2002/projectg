using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoleComplex : MonoBehaviour
{
    public Mole mole1;
    public Mole mole2;
    private BoxCollider2D collider;
    private int num;

    void Start()
    {
        num = (int)Random.Range(1, 3);
        collider = GetComponent<BoxCollider2D>();
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.name == "Player")
        {
            if (num == 1)
            {
                mole1.startMoving = true;
            }
            else
            {
                mole2.startMoving = true;
            }
        }
    }
}
