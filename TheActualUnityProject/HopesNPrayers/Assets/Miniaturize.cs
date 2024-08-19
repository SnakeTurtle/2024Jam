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
        float rot = Mathf.Atan2(rotation.x, rotation.y) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rot);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag.Equals("DeScalable"))
        {
            Vector2 initialScale = other.gameObject.transform.localScale;
            other.gameObject.transform.localScale = initialScale / sizeModifier;
            other.gameObject.tag = "Scalable";
            
        }
        Destroy(gameObject);
    }
}
