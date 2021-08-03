using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingStalactites : MonoBehaviour
{
    private bool startSpin = false;
    public RumbleBlock rumble;
    private Rigidbody2D rb;
    private SpriteRenderer sprite;

    private float random = 0;
    private float random2 = 0;
    private float waitTime = .05f;
    private float opacity = .7f;

    private bool hold = true;
    private bool finishShake = false;
    private bool right;
    private bool slant = true;
    private bool once = true;
    private bool once2 = true;

    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();

        random = Random.Range(-1f, 1f);
        random2 = Random.Range(1f, 2f);

        if (random > 0)
        {
            random = 1f;
            right = true;
        }
        else
        {
            random = -1f;
            right = false;
        }
    }

    void Update()
    {

        if (rumble.counter == 2)
        {
            if (once2)
            {
                stopSlantTime();
                once2 = false;
            }
        }

        if (rumble.StalactiteFall && !finishShake)
        {
            GetComponent<Collider2D>().enabled = false;
            StartCoroutine(shakeTime());
        }

        if (rumble.StalactiteFall && once)
        {
            if (slant)
            {
                StartCoroutine(slantTime(waitTime));
                if (hold)
                {
                    waitTime = .1f;
                    hold = false;
                }
                slant = false;
            }
            if (right)
            {
                transform.Rotate(0, 0, 150f * Time.deltaTime);
            }
            else
            {
                transform.Rotate(0, 0, -150f * Time.deltaTime);
            }
        }

        if (rumble.StalactiteFall && finishShake)
        {
            if (once)
            {
                GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
                GetComponent<Collider2D>().enabled = true;
                rb.gravityScale = 2f;
                once = false;
            }

            if (startSpin)
            {
                transform.Rotate(0, 0, random * 300 * Time.deltaTime);
                sprite.color = new Color(1f, 1f, 1f, opacity);
                opacity -= .005f;

                if (opacity < 0)
                {
                    Destroy(gameObject);
                }

            }
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Ground")
        {
            GetComponent<Collider2D>().enabled = false;
            startSpin = true;
            rb.gravityScale = 3f;
        }
    }

    void stopSlantTime()
    {
        finishShake = true;
    }

    IEnumerator slantTime(float wait)
    {
        yield return new WaitForSeconds(wait);
        right = !right;
        slant = true;
    }

    IEnumerator shakeTime()
    {
        yield return new WaitForSeconds(random2);
        finishShake = true;
    }
}
