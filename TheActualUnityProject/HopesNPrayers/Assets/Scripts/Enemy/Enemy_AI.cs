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
        if (playerFound)
        {
            TrackPlayer();
        }
        inAtkRange = Physics2D.CircleCast(transform.position, atkRange, new Vector2(0, 0),0f,LayerMask.GetMask("Player"));
        if(inAtkRange && !hasAttacked)
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
        hasAttacked = true;
        swordCol.enabled = true;
        sword.transform.Rotate(0, 0, -60,Space.Self);
        Invoke("ResetSword", atkAnimTime);
        Invoke("ResetAtk", atkTimer);
    }

    void ResetSword()
    {
        swordCol.enabled = false;
        sword.transform.Rotate(0, 0, 60,Space.Self);
    }
    void TrackPlayer()
    {
        float distance = Vector2.Distance(transform.position, player.position);
        Vector2 direction = player.position - transform.position;

        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
        if(transform.position.x < player.position.x)
        {
            transform.rotation = new Quaternion(0, 0, 0, 0);
        }
        else if (transform.position.x > player.position.x)
        {
            transform.rotation = new Quaternion(0, 180, 0, 0);
        }
        if(player.position.y > transform.position.y && !hasJumped)
        {
            Jump(direction);
        }
    }

    void Jump(Vector2 dir)
    {
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
        if (other.gameObject.tag.Equals("Player"))
        {
            player = other.gameObject.transform;
            playerFound = true;
            
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, atkRange);
        Gizmos.color = Color.yellow;
    }
}
