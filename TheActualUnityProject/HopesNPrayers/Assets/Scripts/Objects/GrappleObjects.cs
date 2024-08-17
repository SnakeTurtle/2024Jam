using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrappleObjects : MonoBehaviour
{
    public DistanceJoint2D grapple;
    // Start is called before the first frame update
    void Start()
    {
        grapple = GetComponent<DistanceJoint2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //grapple.connectedBody
    }
}
