using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot_Mushroom : MonoBehaviour
{
    public float shootForce;
    // Start is called before the first frame update
    void Start()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.AddForce(new Vector2(shootForce,shootForce)*transform.up, ForceMode2D.Impulse);
    }
}
