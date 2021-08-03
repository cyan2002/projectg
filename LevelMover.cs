using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelMover : MonoBehaviour
{
    [SerializeField] private string nextLevel;
    [SerializeField] private string respawnLevel;

    public Image black;
    public Animator animator;
    private PlayerMovement movement;
    private SpriteRenderer spriteR;

    void Start()
    {
        movement = GameObject.Find("Player").GetComponent<PlayerMovement>();
        spriteR = GameObject.Find("Player").GetComponent<SpriteRenderer>();
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.name == "Player")
        {
            StartCoroutine(newLevel());
        }
    }

    public void checkPointFade()
    {
        StartCoroutine(respawn());
    }

    public IEnumerator respawn()
    {
        spriteR.enabled = false;
        animator.SetBool("Fade", true);
        yield return new WaitUntil(() => black.color.a == 1);
        SceneManager.LoadScene(respawnLevel);
    }

    IEnumerator newLevel()
    {
        animator.SetBool("Fade", true);
        movement.setCheckPoint(new Vector2(0, 0));
        yield return new WaitUntil(() => black.color.a == 1);
        SceneManager.LoadScene(nextLevel);
    }
}
