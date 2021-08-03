using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    private bool once = true;
    private bool mimic = false;
    public bool right = false;

    private int chanceNum = -1;
    private Animator animator;
    private BoxCollider2D triggerBox;
    private float range = 5f;
    private PlayerHealth playerDamageScript;
    public GameObject stinger;
    public GameObject firePoint;
    public GameObject coins;
    public GameObject coinLocation;

    [SerializeField]
    private LayerMask whatIsPlayer;

    void Start()
    {
        animator = GetComponent<Animator>();
        triggerBox = GetComponent<BoxCollider2D>();
        playerDamageScript = GameObject.Find("Player").GetComponent<PlayerHealth>();
    }

    void Update()
    {
        if (mimic)
        {
            animator.SetBool("inRange", detectPlayer());
        }
    }

    void Shoot()
    {
        if (right)
        {
            Instantiate(stinger, firePoint.transform.position, Quaternion.Euler(new Vector3(0, 0, 0)));
        }
        else
        {
            Instantiate(stinger, firePoint.transform.position, Quaternion.Euler(new Vector3(0, 180, 0)));
        }
    }

    public bool detectPlayer()
    {
        RaycastHit2D hitInfo1;
        Vector3 raycastStart = new Vector3(transform.position.x, transform.position.y - .5f, transform.position.z);

        
        if (right)
        {
            hitInfo1 = Physics2D.Raycast(raycastStart, Vector2.right, 10f, whatIsPlayer);//detects wall jump
        }
        else
        {
            hitInfo1 = Physics2D.Raycast(raycastStart, Vector2.left, 10f, whatIsPlayer);//detects wall jump
        }
        if (hitInfo1.collider == null)
        {
            return true;
        }
        return false;
    }

    void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.name == "Player")
        {
            if (Input.GetKey(KeyCode.E))
            {
                rollChance();
                triggerBox.enabled = false;
            }
        }
    }

    void rollChance()
    {
        chanceNum = (int)Random.Range(1, 4);

        if (chanceNum == 1)
        {
            animator.SetBool("regChestOpen", true);
        }
        else if (chanceNum == 2)
        {
            animator.SetBool("micmicOpen", true);
            gameObject.AddComponent<MobTakeDamage>();
            BoxCollider2D col = gameObject.AddComponent<BoxCollider2D>();
            col.offset = triggerBox.offset;
            col.size = triggerBox.size;
            triggerBox.enabled = false;
            gameObject.layer = 21;
            mimic = true;

        }
        else if (chanceNum == 3)
        {
            triggerBox.enabled = false;
            animator.SetBool("explosiveChestOpen", true);
        }
        else
        {
            Debug.Log("error");
        }
    }

    void damage()
    {
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(new Vector3(transform.position.x, transform.position.y - .25f, transform.position.z), 3f);
        for (int i = 0; i < hitColliders.Length; i++)
        {
            if (hitColliders[i].gameObject.layer == 3 || hitColliders[i].gameObject.layer == 6)
            {
                if (Vector3.Distance(hitColliders[i].transform.position, new Vector3(transform.position.x, transform.position.y - .5f, transform.position.z)) <= 1f)
                {
                    Debug.Log("1");
                    playerDamageScript.takeDamage(5); // ADD KNOCKBACK
                }
                else if (Vector3.Distance(hitColliders[i].transform.position, new Vector3(transform.position.x, transform.position.y - .5f, transform.position.z)) <= 2f)
                {
                    Debug.Log("2");
                    playerDamageScript.takeDamage(3); // ADD KNOCKBACK
                }
                else if (Vector3.Distance(hitColliders[i].transform.position, new Vector3(transform.position.x, transform.position.y - .5f, transform.position.z)) <= 3f)
                {
                    Debug.Log("3");
                    playerDamageScript.takeDamage(1); // ADD KNOCKBACK
                }
            }
        }
    }

    void destroyObject()
    {
        DestroyObject(gameObject);
    }

    void enableAttackBox()
    {
        gameObject.tag = "MobsDMG1";
    }

    void giveGold()
    {
        Instantiate(coins, coinLocation.transform.position, coinLocation.transform.rotation);
    }
}
