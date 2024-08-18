using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public List<Transform> positions;
    public static CameraManager instance;
    public int currentPos=0;
    public Transform mainCam;
    
    void Start()
    {
        instance = this;
        mainCam.position = positions[currentPos].position;
    }

    public void moveCamLeft()
    {
        if (currentPos > 0)
        {
            mainCam.position = Vector3.MoveTowards(mainCam.position, positions[currentPos - 1].position, Vector3.Distance(mainCam.position, positions[currentPos - 1].position));
            currentPos--;
        }
        
    }
    public void moveCamRight()
    {
        if (currentPos < positions.Count-1)
        {
            mainCam.position = Vector3.MoveTowards(mainCam.position, positions[currentPos + 1].position,Vector3.Distance(mainCam.position, positions[currentPos + 1].position));
            currentPos++;
        }
        
    }
    
    void Update()
    {
        
    }
}
