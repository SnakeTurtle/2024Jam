using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
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
    public bool charNormal =  true;
    public Rigidbody2D grapplableObj;
    private DistanceJoint2D grapple;
    public LineRenderer _lineRenderer;
    public Animator animator;
    public float dashRate = 2f;
    public float dashingTime = .4f;
    public bool canDash;
    public bool isDashing = false;
    public float dashingPower = 20f;
    public float dashingCooldown = 1f;
    private bool keepMom = false;
    Boolean swungRight = true, swungLeft = true;
    public Rigidbody2D grapObj;
    private bool canGrap = false;
    BoxCollider2D playerCollider;
    public float hitBoxTime;
    public int jump;
    public int wreckForce;
    public bool safe;

    // Start is called before the first frame update
    void Start()
    {
       canDash = true;
       rb = GetComponent<Rigidbody2D>();
       grapple = GetComponent<DistanceJoint2D>();
       grapple.enabled = false;
       animator.SetBool("isDashing", false);
       playerCollider = GetComponent<BoxCollider2D>();
    }


    //INPUTS FOR MOVEMENT
    // Update is called once per frame
    void Update()
    {
        if (isDashing == false)
        {
            //SCALING 

            //to change sizes
            if (Input.GetKeyDown(KeyCode.E) && canDash == true)
            {
                playerChange();
            }


            //MOVEMENT
            if (grapple.enabled)
            {
                grappleMovement();
            }
            else{
                if (keepMom == false && charNormal == true)
                {
                    playerMovement();
                }
            }

            if (Input.GetKeyDown(KeyCode.W) && canJump)
            {
                playerJump();
            }


            //GRAPPLE
            if (Input.GetKeyDown(KeyCode.Q) && canGrap)
            {
                playerGrapple();
            }
            if (grapple.enabled)
            {
                _lineRenderer.SetPosition(0, transform.position);
            }
        }
        //DASH
        if (Input.GetKeyDown(KeyCode.Space) && grapple.enabled == false && canDash)
        {
            playerNormal();
            StartCoroutine(playerDash());
        }
    }



    //Other Methods


    //SCALE METHODS

    private void playerChange()
    {
        if (charNormal)
        {
            if (grapple.enabled)
            {
                grapple.enabled = false;
                _lineRenderer.enabled = false;
                rb.AddForce(new Vector2(rb.velocity.x+wreckForce, rb.velocity.y));
                keepMom = true;
            }else if(canJump == false && keepMom  == false)
            {
                rb.velocity = new Vector2(0,rb.velocity.y);
            }
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

    public void playerNormal()
    {
        gameObject.transform.localScale = new Vector3(0.4f, 0.4f, 1);
        charNormal = true;
        playerCollider.size = new Vector2(1, 1);
        playerCollider.offset = new Vector2(0, 0);
    }




    // MOVING & JUMP
    public void playerMovement()
    {
        move = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(speed * move, rb.velocity.y);
    }

    public void playerJump()
    {
        if (charNormal)
        {
            rb.AddForce(new Vector2(rb.velocity.x, jump));
        }
    }


    //GRAPPLE
    public void playerGrapple()
    {
        keepMom = false;
        if(grapple.enabled)
        {
            grapple.enabled = false;
            _lineRenderer.enabled = false;
            keepMom = true;
            Debug.Log("Grapple off");
        }
        else
        {
            _lineRenderer.SetPosition(0, transform.position);
            _lineRenderer.SetPosition(1, grapObj.transform.position);
            grapple.connectedAnchor = new Vector2(grapObj.transform.position.x, grapObj.transform.position.y);
            grapple.enabled = true;
            _lineRenderer.enabled = true;
            Debug.Log("Grapple on");
        }
    }


    //FOR DASH
    IEnumerator playerDash()
    {

        animator.SetBool("isDashing", true);
        //For Actual Dash
        keepMom = false;
        canDash = false;
        isDashing = true;
        rb.gravityScale = 0f;
        playerCollider.size = new Vector2(2.33885f, 0.7601432f);
        playerCollider.offset = new Vector2(-0.26922f, -0.1525713f);

        //direction of dash

        if (move >= 0 && Input.GetKey(KeyCode.A) == false|| Input.GetKey(KeyCode.D))
        {
            rb.velocity = new Vector2(dashingPower, 0);
        }
        else if (move < 0 && Input.GetKey(KeyCode.D)  == false|| Input.GetKey(KeyCode.A))
        {
            rb.velocity = new Vector2(-1 * dashingPower, 0);
            gameObject.transform.localScale = new Vector3(-0.4f, 0.4f,0);
        }

        //FINISH DASH
        yield return new WaitForSeconds(dashingTime);
        
        isDashing = false;
        rb.gravityScale = 1f;
        animator.SetBool("isDashing", false);

        Invoke("playerNormal", hitBoxTime);
        
        //READY DASH
        yield return new WaitForSeconds(dashingCooldown);
        canDash = true;
        playerNormal();
    }


    private void grappleMovement()
    {
        if (Input.GetKeyDown(KeyCode.D) && swungLeft)
        {
            rb.velocity = new Vector2(6, 0);
            swungLeft = false;
            swungRight = true;
        }
        if (Input.GetKeyDown(KeyCode.A) && swungRight)
        {
           rb.velocity = new Vector2(-6, 0);
            swungRight = false;
            swungLeft = true;
        }
    }
    //Check for Ground
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            canJump = true;
            keepMom = false;
        }
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
            {
                canJump = true;
                
            }a
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            canJump = false;
        }
    }

    //Check for Grapple Objects
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Grappable")
        {
            grapObj = other.attachedRigidbody;
            canGrap = true;
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "Grappable")
        {
            grapObj = other.attachedRigidbody;
            canGrap = true;
        }else if(other.gameObject.tag == "Ground")
        {
            safe = false;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Grappable") {
            canGrap = false;
        }
        else if (other.gameObject.tag == "Ground")
        {
            safe = true;
        }
    }

    Boolean safeToGrow()
    {
        if (safe)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

}