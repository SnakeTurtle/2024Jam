using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public List<Transform> positions;
    public static CameraManager instance;
    public int currentPos=0;
    public Transform mainCam;
    public bool inFight = false;
    public float count = 0;
    Vector2 bound;
    public Transform locked;

    Transform leftScreen;
    Transform rightScreen;

    float lockpos;
    void Start()
    {
        instance = this;
        //mainCam.position = positions[currentPos].position;
        leftScreen = mainCam.transform.GetChild(0);
        rightScreen = mainCam.transform.GetChild(1);
        bound = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, mainCam.position.z));
        leftScreen.position = new Vector3(-bound.x, 0, 0);
        rightScreen.position = new Vector3(bound.x, 0, 0);
    }

    public void moveCamLeft()
    {
        //Vector2 bound = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, mainCam.position.z));
        mainCam.position = new Vector3(mainCam.position.x -(bound.x*1.8f), 0, -10);
        count -= 1;
        /*if (currentPos > 0 && !inFight)
        {
            //mainCam.position = Vector3.MoveTowards(mainCam.position, positions[currentPos - 1].position, Vector3.Distance(mainCam.position, positions[currentPos - 1].position));
            //currentPos--;
        }*/

    }
    public void moveCamRight()
    {
        //Vector2 bound = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, mainCam.position.z));
        mainCam.position = new Vector3(mainCam.position.x + (bound.x*1.8f), 0, -10);
        count += 1;
        /*if (currentPos < positions.Count-1 && !inFight)
        {
            //mainCam.position = Vector3.MoveTowards(mainCam.position, positions[currentPos + 1].position,Vector3.Distance(mainCam.position, positions[currentPos + 1].position));
            //currentPos++;
            if (positions[currentPos].gameObject.tag.Equals("FightRoom"))
            {
                inFight = true;
                mainCam.GetChild(0).GetComponent<BoxCollider2D>().isTrigger = false;
                mainCam.GetChild(1).GetComponent<BoxCollider2D>().isTrigger = false;
            }
            
        }*/

    }
    
    public void endFight()
    {
        positions[currentPos].gameObject.tag = "FightFinished";
        inFight = false;
        mainCam.GetChild(0).GetComponent<BoxCollider2D>().isTrigger = true;
        mainCam.GetChild(1).GetComponent<BoxCollider2D>().isTrigger = true;
    }

    void Update()
    {
        if (inFight && Input.GetKeyDown(KeyCode.Space))
        {
            endFight();
        }
        if(mainCam.position.x > lockpos)
        {
            mainCam.position = locked.position;
            BoxCollider2D L = leftScreen.gameObject.GetComponent<BoxCollider2D>();
            BoxCollider2D R = rightScreen.gameObject.GetComponent<BoxCollider2D>();
            L.isTrigger = false;
            R.isTrigger = false;
        }
    }
}
