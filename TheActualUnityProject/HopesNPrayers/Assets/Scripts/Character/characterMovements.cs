using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UIElements;

public class characterMovements : MonoBehaviour
{
    //VARIABLES
    private Rigidbody2D rb;
    public float speed;
    private float move;
    private bool canJump;
    private bool charNormal =  true;



    // Start is called before the first frame update
    void Start()
    {
       rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //SCALING 

        //to scale bigger
        if (Input.GetKeyDown(KeyCode.E))
        {
            playerChange();
        }
  

        //MOVEMENT
        playerMovement();

        if (Input.GetKeyDown(KeyCode.W) && canJump)
        {
            playerJump();
        }
    }



    //Other Methods

    private void playerChange()
    {
        if (charNormal)
        {
            playerBigger();
        }
        else
        {
            playerNormal();
        }
    }
    private void playerBigger()
    {
        gameObject.transform.localScale = Vector3.one;
        charNormal = false;
    }

    private void playerNormal()
    {
        gameObject.transform.localScale = new Vector3((float).4, (float)0.4, 1);
        charNormal = true;
    }

    private void playerMovement()
    {
        move = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(speed * move, rb.velocity.y);
    }

    private void playerJump()
    {
        int jump;
        if (charNormal)
        {
            jump = 350;
        }
        else
        {
            jump = 200;
        }
        rb.AddForce(new Vector2(rb.velocity.x, jump));
        Debug.Log("jump");
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            canJump = true;
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            canJump = false;
        }
    }

}
