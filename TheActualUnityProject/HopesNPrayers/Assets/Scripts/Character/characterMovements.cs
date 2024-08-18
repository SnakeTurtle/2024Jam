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
    public Animator animator;
    public float dashRate = 2f;
    float dashingTime = .1f;
    public bool canDash;
    public bool isDashing;
    public float dashingPower = 24f;
    public float dashingCooldown = 1f;



    // Start is called before the first frame update
    void Start()
    {
       rb = GetComponent<Rigidbody2D>();
       grapple = GetComponent<DistanceJoint2D>();
       grapple.enabled = false;
       animator.SetBool("isDashing", false);
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
        //DASH
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(playerDash());
            playerNormal();
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

    public void playerNormal()
    {
        gameObject.transform.localScale = new Vector3((float).4, (float)0.4, 1);
        charNormal = true;
    }




    // MOVING & JUMP
    public void playerMovement()
    {
        move = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(speed * move, rb.velocity.y);
    }

    public void playerJump()
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
    }


    //GRAPPLE
    public void playerGrapple()
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


    //FOR DASH
    IEnumerator playerDash()
    {

        animator.SetBool("isDashing", true);
        //For Actual Dash
        canDash = false;
        isDashing = true;
        float ogGravity = rb.gravityScale;
        rb.gravityScale = 0f;
        if (move < 0)
        {
            rb.velocity = new Vector2(-1 * dashingPower, 0);
            gameObject.transform.rotation = Quaternion.Euler(0, 0, 180);
        }
        else
        {
            rb.velocity = new Vector2(dashingPower, 0);
            gameObject.transform.rotation = Quaternion.Euler(0,0,0);
        }
        Debug.Log("start dash");
        yield return new WaitForSeconds(dashingTime);
        isDashing = false;
        rb.gravityScale = ogGravity;
        animator.SetBool("isDashing", false);
        Debug.Log("finished dash");
        yield return new WaitForSeconds(dashingCooldown);
        canDash = true;
        Debug.Log("ready to dash");
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