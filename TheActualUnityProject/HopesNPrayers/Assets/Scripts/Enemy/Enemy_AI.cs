using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class Enemy_AI : MonoBehaviour
{

    public float viewDistance;
    public bool playerFound = false;
    public Transform player;

    public float speed = 1f;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (playerFound)
        {
            TrackPlayer();
        }
    }

    void TrackPlayer()
    {
        float distance = Vector2.Distance(transform.position, player.position);
        Vector2 direction = player.position - transform.position;

        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag.Equals("Player"))
        {
            player = other.gameObject.transform;
            playerFound = true;
            print("gamin");
        }
    }


}
