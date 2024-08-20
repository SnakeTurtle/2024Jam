using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EdgeOfScreenRight : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag.Equals("Player") && !CameraManager.instance.inFight && other.GetType().ToString() == "UnityEngine.BoxCollider2D")
        {
            CameraManager.instance.moveCamRight();
        }
    }
}
