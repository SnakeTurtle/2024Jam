using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Sword : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag.Equals("Player") && other.GetType().ToString() == "UnityEngine.BoxCollider2D")
        {
            Health playerHealth = other.GetComponent<Health>();
            playerHealth.TakeDamage(1);
        }

    }
}
