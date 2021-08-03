using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobTakeDamage : MonoBehaviour
{
    public int takeDamage = 1;
    public int health = 3;

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "SlingshotBullet")
        {
            health = health - takeDamage;
            if (health <= 0)
            {
                DestroyObject(gameObject);
            }
        }
        else if (col.gameObject.tag == "InstaKillSpike")
        {
            DestroyObject(gameObject);
        }
    }

    public void outsideDamage(int damage)
    {
        health = health - damage;
        if (health <= 0)
        {
            DestroyObject(gameObject);
        }
    }

}
