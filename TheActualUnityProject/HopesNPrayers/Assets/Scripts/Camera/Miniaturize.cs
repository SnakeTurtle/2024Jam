using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Miniaturize : MonoBehaviour
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


    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag.Equals("DeScalable"))
        {
            Vector2 initialScale = other.gameObject.transform.localScale;
            other.gameObject.transform.localScale = initialScale / sizeModifier;
            other.gameObject.tag = "Scalable";
            other.gameObject.GetComponent<SpriteRenderer>().color = Color.blue;

            if (other.gameObject.layer.ToString().Equals("6"))
            {
                other.gameObject.GetComponent<SpriteRenderer>().color = Color.blue;
                other.gameObject.transform.position = new Vector3(other.gameObject.transform.position.x, -3.277826f, 0f);
            }

        }else if (other.gameObject.tag.Equals("bigWall"))
            {
                other.gameObject.transform.position = new Vector3(other.gameObject.transform.position.x, other.gameObject.transform.position.y -1.0308137f, 0);
                other.gameObject.transform.localScale= new Vector3(other.gameObject.transform.localScale.x-0.335024f, other.gameObject.transform.localScale.y-2.417522f, 0);
                other.gameObject.transform.GetChild(0).transform.position = new Vector3(other.gameObject.transform.GetChild(0).transform.position.x, other.gameObject.transform.GetChild(0).transform.position.y-.272f);
                other.gameObject.GetComponent<SpriteRenderer>().color = Color.blue;
            }
        Debug.Log("Bullet hit: " + other.gameObject.tag.ToString());
        if (!other.gameObject.CompareTag("ignoreBullet") && !other.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
}
