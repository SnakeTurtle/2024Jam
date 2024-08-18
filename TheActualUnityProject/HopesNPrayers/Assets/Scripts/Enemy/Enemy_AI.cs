using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_AI : MonoBehaviour
{

    public float viewDistance;
    public GameObject eyeBalls;
    LayerMask player;

    void Start()
    {
        eyeBalls = gameObject.transform.GetChild(0).gameObject;
        player = LayerMask.GetMask("Player");
    }

    // Update is called once per frame
    void Update()
    {
        FindPlayer();
        GetBigger();
    }

    private void FindPlayer()
    {
        bool hit = Physics2D.Raycast(eyeBalls.transform.position, Vector2.right, viewDistance,player);
        if (hit)
        {
            Debug.Log("PlayerFound");
        }
        else
        {
            Debug.Log("Player not found");
        }
    }
    private void GetBigger()
    {
        bool a = Input.GetKeyDown(KeyCode.G);
        if (a)
        {
            gameObject.transform.localScale = new Vector3(3, 4, 3);
        }
    }



    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawRay(eyeBalls.transform.position, Vector2.right * viewDistance);
    }
}
