using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPlant : MonoBehaviour
{
    public Sprite sprite;
    private SpriteRenderer Rsprite;
    private Collider2D collider;
    private PlayerHealth playerHealth;

    void Start()
    {
        Rsprite = GetComponent<SpriteRenderer>();
        collider = GetComponent<Collider2D>();
        playerHealth = GameObject.FindWithTag("Player").GetComponent<PlayerHealth>();
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.name == "Player")
        {
            Rsprite.sprite = sprite;
            collider.enabled = false;
            playerHealth.healHealth(1);
        }
    }
}
