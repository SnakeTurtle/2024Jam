using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Biggify : MonoBehaviour
{
    public float shootForce;
    public float sizeModifier;
    Vector3 mousePos;
    Camera mainCam;
    // Start is called before the first frame update
    void Start()
    {
        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);
        Vector3 direction = mousePos - transform.position;
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        Vector3 rotation = transform.position - mousePos;
        rb.velocity = new Vector2(direction.x, direction.y).normalized * shootForce;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag.Equals("Scalable"))
        {
            Vector2 initialScale = other.gameObject.transform.localScale;
            other.gameObject.transform.localScale = initialScale * sizeModifier;
            other.gameObject.tag = "DeScalable";

            if (other.gameObject.layer.ToString().Equals("7"))
            {
                other.gameObject.GetComponent<SpriteRenderer>().color = Color.red;
            }
            if (other.gameObject.layer.ToString().Equals("6")){
                other.gameObject.transform.position = new Vector3(other.gameObject.transform.position.x, -2.881526f, 0);
                other.gameObject.GetComponent<SpriteRenderer>().color = Color.red;
            }
            
        }
        else if (other.gameObject.tag.Equals("longatableFloor"))
        {
            other.gameObject.transform.localScale = new Vector3(10.13f, 0.48f);
            other.gameObject.GetComponent<SpriteRenderer>().color = Color.red;
            other.gameObject.tag = "Ground";
        }

        Debug.Log("Bullet hit: " + other.gameObject.tag.ToString());
        if (!other.gameObject.CompareTag("ignoreBullet") && !other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("destroyBullet"))
        { 
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("mainCamera"))
        {
            Destroy(gameObject);
        }
    }
}
