using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class YouFell : MonoBehaviour
{
    public GameObject deathText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag.Equals("Player")&&other.GetType().ToString() == "UnityEngine.Collision2D")
        {
            deathText.SetActive(true);
            Destroy(other.gameObject);
        }
    }
}
