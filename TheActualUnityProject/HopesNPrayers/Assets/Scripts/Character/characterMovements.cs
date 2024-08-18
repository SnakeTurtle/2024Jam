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
    public Rigidbody2D grapplableObj;
    private DistanceJoint2D grapple;
    public LineRenderer _lineRenderer;



    // Start is called before the first frame update
    void Start()
    {
       rb = GetComponent<Rigidbody2D>();
       grapple = GetComponent<DistanceJoint2D>();
       grapple.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        //SCALING 

        //to change sizes
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


        //GRAPPLE
        if (Input.GetKeyDown(KeyCode.Q))
        {
            playerGrapple();
        }
        if (grapple.enabled)
        {
            _lineRenderer.SetPosition(0, transform.position);
        }
    }



    //Other Methods


    //SCALE METHODS

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




    // MOVING & JUMP
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


    //GRAPPLE
    private void playerGrapple()
    {
        if(grapple.enabled)
        {
            grapple.enabled = false;
            _lineRenderer.enabled = false;
            Debug.Log("Grapple off");
        }
        else
        {
            _lineRenderer.SetPosition(0,transform.position);
            _lineRenderer.SetPosition(1, grapplableObj.transform.position);
            grapple.connectedAnchor = new Vector2(grapplableObj.transform.position.x, grapplableObj.transform.position.y);
            grapple.enabled = true;
            _lineRenderer.enabled = true;
            Debug.Log("Grapple on");
        }
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
