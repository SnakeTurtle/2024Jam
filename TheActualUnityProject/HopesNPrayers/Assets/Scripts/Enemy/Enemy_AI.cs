using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class Enemy_AI : MonoBehaviour
{

    public float viewDistance;
    public bool playerFound = false;
    public bool hasJumped = false;
    public Transform player;

    public float speed = 1f;
    public float jumpTimer;

    public float jumpY;
    public float jumpX;

    public GameObject sword;
    public GameObject alert;
    public Collider2D swordCol;

    public bool hasAttacked = false;
    public float atkTimer;
    public bool inAtkRange = false;
    public float atkRange;
    public float atkAnimTime;
    void Start()
    {
        swordCol.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        //On Collision Trigger Enter get the player's pos and then start tracking them
        if (playerFound)
        {
            TrackPlayer();
        }
        //Circle cast, if player in cirlce begin attack
        inAtkRange = Physics2D.CircleCast(transform.position, atkRange, new Vector2(0, 0),0f,LayerMask.GetMask("Player"));
        
        if (inAtkRange && !hasAttacked)
        {
            Attack();
        }
        if (inAtkRange)
        {
            alert.SetActive(true);
        }
        else
        {
            alert.SetActive(false);
        }
    }

    void Attack()
    {
        //Attack rotates the sword, the actual damage to the player is in a script attached to the sword obj
        hasAttacked = true;
        swordCol.enabled = true;
        sword.transform.Rotate(0, 0, -60,Space.Self);
        Invoke("ResetSword", atkAnimTime);
        Invoke("ResetAtk", atkTimer);
    }

    void ResetSword()
    {
        //Resets the rotation of sword, the swordCol is the collider of the sword, turn on when attacking turn off when not
        swordCol.enabled = false;
        sword.transform.Rotate(0, 0, 60,Space.Self);
    }
    void TrackPlayer()
    {
        //Compare the position of enemy to position of player
        float distance = Vector2.Distance(transform.position, player.position);
        Vector2 direction = player.position - transform.position;
        //Move towards the player in a given time frame, this is speed*time
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
        //If the player is on the right or left of enemy adjust rotation appropriately
        if(transform.position.x < player.position.x)
        {
            Invoke("changeRotationLeft",2f);
        }
        else if (transform.position.x > player.position.x)
        {
            Invoke("changeRotationRight", 2f);
        }
        if(player.position.y > transform.position.y && !hasJumped)
        {
            Jump(direction);
        }
    }
    void changeRotationLeft()
    {
        transform.rotation = new Quaternion(0, 0, 0, 0);
    }
    void changeRotationRight()
    {
        transform.rotation = new Quaternion(0, 180, 0, 0);
    }
    void Jump(Vector2 dir)
    {
        //If player is above enemy then jump if not on cooldwon
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.AddForce(new Vector2(jumpX,jumpY)*dir, ForceMode2D.Impulse);
        hasJumped = true;
        Invoke("ResetJump", jumpTimer);
    }

    void ResetJump()
    {
        hasJumped = false;
    }

    void ResetAtk()
    {
        hasAttacked = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag.Equals("Player") && other.GetType().ToString() == "UnityEngine.BoxCollider2D")
        {
            player = other.gameObject.transform;
            playerFound = true;
            
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, atkRange);
        Gizmos.color = Color.red;
    }
}
