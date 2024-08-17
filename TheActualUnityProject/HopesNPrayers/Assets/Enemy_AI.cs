using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_AI : MonoBehaviour
{

    public float viewDistance;
    GameObject eyeBalls;

    // Start is called before the first frame update
    void Start()
    {
        eyeBalls = gameObject.transform.GetChild(0).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        FindPlayer();
        GetBigger();
    }

    private void FindPlayer()
    {
        RaycastHit2D hit = Physics2D.Raycast(eyeBalls.transform.position, Vector2.right, viewDistance);
        Debug.Log(hit.collider.gameObject.name);
        if (hit.collider.gameObject.tag == "Player")
        {
            Debug.Log("PlayerFound");
        }
    }
    private void GetBigger()
    {
        bool a = Input.GetKeyDown(KeyCode.F);
        if (a)
        {
            gameObject.transform.localScale = new Vector3(3, 4, 3);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawRay(transform.position, Vector2.right * viewDistance);
    }
}
