using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleWall : MonoBehaviour
{
    GameObject use_key;
    bool canActivate=false;
    // Start is called before the first frame update
    void Start()
    {
        use_key = transform.Find("Key").gameObject;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && canActivate)
        {
            Debug.LogAssertion("PRESSED");
            transform.localScale = new Vector2(1, 3);
        }
    }
    private void OnMouseEnter()
    {
        ShowActivateKey(true);
        canActivate = true;
    }
    private void OnMouseExit()
    {
        ShowActivateKey(false);
        canActivate = false;
    }
    void ShowActivateKey(bool status)
    {
        if (status)
        {
            use_key.SetActive(true);
        }
        else use_key.SetActive(false);

    }
}
