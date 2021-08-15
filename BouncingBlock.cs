using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//NOTE CHECK FOR ERRORS - canUseVelocity variable

public class BouncingBlock : MonoBehaviour
{
    private Rigidbody2D rb;
    private PlayerMovement cc;
    public BounceBlockReceive BBR;

    void Start()
    {
        rb = GameObject.Find("Player").GetComponent<Rigidbody2D>();
        cc = GameObject.Find("Player").GetComponent<PlayerMovement>();
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.name == "Player")
        {
            StartCoroutine(pushTime());
        }
    }

    IEnumerator pushTime()
    {
        cc.canUseVelocity = false;
        rb.AddForce(Vector2.up * Mathf.Abs(BBR.yVelocity), ForceMode2D.Impulse);
        yield return new WaitForSeconds(.1f);

        cc.canUseVelocity = true;
    }
}
