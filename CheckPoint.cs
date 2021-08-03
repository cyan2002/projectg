using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    private PlayerMovement checkPoint;
    private Vector3 positionFlag;
    private Collider2D collider2D;
    private Animator animator;

    void Start()
    {
        positionFlag = transform.position;
        collider2D = GetComponent<Collider2D>();
        animator = GetComponent<Animator>();
        checkPoint = GameObject.Find("Player").GetComponent<PlayerMovement>();
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.name == "Player")
        {
            checkPoint.setCheckPoint(positionFlag);
            collider2D.enabled = false;
            animator.SetBool("Passed", true);
        }
    }
}
