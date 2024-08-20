using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class characterAttack : MonoBehaviour
{
    public characterMovements charMoves;
    public int count;
    // Start is called before the first frame update
    void Start()
    {
        count = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (charMoves.isDashing == true)
        {
            if (other.gameObject.CompareTag("Weakpoint"))
            {
                Destroy(other.gameObject.transform.parent.gameObject);
                count++;
            }
            if (other.gameObject.CompareTag("Boss"))
            {
                other.GetComponent<BossLife>().takeLive();
            }
        }
    }
}
