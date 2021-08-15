using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    private bool canPickUp = false;
    public bool hasWeapon = false;
    public string weaponName;
    private SpriteRenderer spriteR;
    private Collider2D collider;
    private PlayerMovement player;

    void Start()
    {
        spriteR = GetComponent<SpriteRenderer>();
        collider = GetComponent<Collider2D>();
        player = GameObject.FindWithTag("Player").GetComponent<PlayerMovement>();
    }

    void Update()
    {
        if (canPickUp)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                hasWeapon = true;
                spriteR.enabled = false;
                collider.enabled = false;
                player.equipWeapon();
            }
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.name == "Player")
        {
            canPickUp = true;
        }
    }

    void OnTriggerExit2D()
    {
        canPickUp = false;
    }
}
