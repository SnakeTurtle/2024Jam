using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_projectile : MonoBehaviour
{
    public float shootForce;
    public bool isStopped;
    Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb=GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(10,0)*Vector2.left * shootForce;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.tag.Equals("DeScalable"))
        {
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
        }
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag.Equals("Player") && gameObject.tag.Equals("Scalable"))
        {
            Health player = other.gameObject.GetComponent<Health>();
            player.TakeDamage(5);
            Destroy(gameObject);
        }
        
    }
}
