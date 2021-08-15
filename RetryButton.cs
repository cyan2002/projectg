using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RetryButton : MonoBehaviour
{
    public SpriteRenderer retryButton;
    private PlayerMovement movement;
    private PlayerHealth playerHealth;
    private bool GameIsPaused = false;

    void Start()
    {
        retryButton.enabled = false;
        movement = GameObject.FindWithTag("Player").GetComponent<PlayerMovement>();
        playerHealth = GameObject.FindWithTag("Player").GetComponent<PlayerHealth>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                movement.startMovement();
                Time.timeScale = 1f;
                GameIsPaused = false;
                retryButton.enabled = false;
            }
            else
            {
                movement.stopMovement();
                Time.timeScale = 0f;
                GameIsPaused = true;
                retryButton.enabled = true;
            }
        }

        if(GameIsPaused)
        {
            if(Input.GetKeyDown(KeyCode.R))
            {
                movement.startMovement();
                Time.timeScale = 1f;
                GameIsPaused = false;
                retryButton.enabled = false;
                playerHealth.Die();
            }
        }

    }


}
