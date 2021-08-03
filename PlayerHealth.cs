using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    private static int health = 3;
    private static int maxHealth = 3;
    private static int healthElement;
    private Vector3[] heartPositions = new Vector3[10];
    private GameObject[] hearts = new GameObject[10];
    //Position 0 is Top left, Position 9 is Bottom Right

    public GameObject heart;
    public GameObject camPos;
    private Rigidbody2D rb;
    private SpriteRenderer spriteR;
    private Animator animator;
    public LevelMover sceneMover;
    private PlayerMovement movement;

    private bool invincible = false;
    private bool hurt = false;

    private float invincibilityTime = 3f;
    private float invincibilityDeltaTime = .3f;

    [SerializeField] private string newLevel = "DeathScreen";

    void Start()
    {
        assignPositions();
        rb = GetComponent<Rigidbody2D>();
        spriteR = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        movement = GetComponent<PlayerMovement>();
    }

    private void assignPositions()
    {
        healthElement = health - 1;
        for(int i = 0; i < 5; i++)
        {
            heartPositions[i] = new Vector3(-9 + i, 4, 0);
        }
        for(int i = 5; i < heartPositions.Length; i++)
        {
            heartPositions[i] = new Vector3(-9 + i, 3, 0);
        }
        for(int i = 0; i < health; i++)
        {
            hearts[i] = Instantiate(heart, heartPositions[i], Quaternion.identity, camPos.transform);
        }
    }

    public void resetHealth()
    {
        health = 3;
    }

    public void takeDamage(int amt)
    {
        health -= amt;
        if(health <= 0)
        {
            movement.stopMovement();
            displayHearts(health, true);
            deathAnimation();
        }
        else
        {
            displayHearts(amt, true);
        }
    }

    public void healHealth(int amt)
    {
        if(health < maxHealth)
        {
            if(health+amt > maxHealth)
            {
                displayHearts(maxHealth - health, false);
                health = maxHealth;
            }
            else
            {
                displayHearts(amt, false);
                health += amt;
            }
        }
    }

    private void increaseMaxHealth(int amt)
    {
        if(maxHealth+amt >= 10)
        {
            maxHealth = 10;
            healHealth(maxHealth - health);
        }
        else
        {
            maxHealth += amt;
            healHealth(amt);
        }
    }

    private void displayHearts(int amt, bool damage)
    {
        if (damage) //taking damage (deleting hearts)
        {
            for(int i = 0; i < amt; i++)
            {
                Destroy(hearts[healthElement]);
                healthElement--;
            }
        }
        else //healing
        {
            for(int i = 0; i < amt; i++)
            {
                hearts[healthElement+1+i] = Instantiate(heart, heartPositions[healthElement + 1 + i], Quaternion.identity, camPos.transform);
            }
        }
    }

    private void knockBack(float knockForceUp, float knockForceSide, bool right)
    {
        rb.AddForce(knockForceUp * Vector2.up, ForceMode2D.Impulse);
        if (right)
        {
            rb.AddForce(Vector2.right * knockForceSide, ForceMode2D.Impulse);

        }
        else
        {
            rb.AddForce(Vector2.left * knockForceSide, ForceMode2D.Impulse);
        }
    }

    private void deathAnimation()
    {
        animator.SetBool("IsDead", true);
    }

    private void Die()
    {
        resetHealth();
        StartCoroutine(sceneMover.respawn());
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.layer == 6)
        {
            if(!invincible)
            {
                takeDamage(1);
                knockBack(10f, 10f, col.gameObject.transform.position.x > this.transform.position.x);
                StartCoroutine(Invulnerability());
            }
        }
        else if(col.gameObject.layer == 8)
        {
            if (!invincible)
            {
                deathAnimation();
            }
        }
        else if(col.gameObject.layer == 9)
        {
            if (!invincible)
            {
                takeDamage(1);
                StartCoroutine(Invulnerability());
            }
        }
    }

    private IEnumerator Invulnerability()
    {
        invincible = true;
        gameObject.layer = 7;

        for (float i = 0; i < invincibilityTime; i += invincibilityDeltaTime)//Time.deltaTime
        {
            if (!hurt)
            {
                spriteR.color = new Color(0, 0, 0, 0);
                hurt = !hurt;
            }
            else
            {
                spriteR.color = new Color(255, 255, 255, 255);
                hurt = !hurt;
            }
            yield return new WaitForSeconds(invincibilityDeltaTime);
        }

        spriteR.color = new Color(255, 255, 255, 255);
        gameObject.layer = 3;
        invincible = false;
    }
}
