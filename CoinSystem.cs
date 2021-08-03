using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinSystem : MonoBehaviour
{
    //attach to object above player
    private static int coins = 0;
    private Animator animator;
    private SpriteRenderer spriteR;
    private Text displayCoin;

    void Start()
    {
        animator = GetComponent<Animator>();
        spriteR = GetComponent<SpriteRenderer>();
        displayCoin = GameObject.Find("CoinShow").GetComponent<Text>();
        spriteR.enabled = false;
        displayCoin.text = "Money: " + coins;
    }

    public void addCoin(int amt)
    {
        coins += amt;
        displayCoin.text = "Money: " + coins;
    }

    public void subCoin(int amt)
    {
        coins -= amt;
        displayCoin.text = "Money: " + coins;
    }

    public void coinAnimation()
    {
        spriteR.enabled = true;
        animator.SetBool("Grabbed", true);
    }

    public void finishedAnimation()
    {
        spriteR.enabled = false;
        animator.SetBool("Grabbed", false);
    }
}
