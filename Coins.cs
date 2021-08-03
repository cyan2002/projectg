using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coins : MonoBehaviour
{
    public Sprite[] coinList;
    private CoinSystem PCS;
    private SpriteRenderer spriteR;
    public int coinValue;
    private Animator animator;

    void Start()
    {
        PCS = GameObject.Find("CoinCollector").GetComponent<CoinSystem>();
        animator = GameObject.Find("Player").GetComponent<Animator>();
        int random = (int)Random.Range(0, 8);
        spriteR = GetComponent<SpriteRenderer>();
        spriteR.sprite = coinList[random];

        switch (random)
        {
            case 0:
                coinValue = 300;
                break;
            case 1:
                coinValue = 320;
                break;
            case 2:
                coinValue = 260;
                break;
            case 3:
                coinValue = 230;
                break;
            case 4:
                coinValue = 140;
                break;
            case 5:
                coinValue = 140;
                break;
            case 6:
                coinValue = 270;
                break;
            case 7:
                coinValue = 320;
                break;
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.name == "Player")
        {
            PCS.addCoin(coinValue);
            PCS.coinAnimation();
            DestroyObject(gameObject);
        }
    }
}
