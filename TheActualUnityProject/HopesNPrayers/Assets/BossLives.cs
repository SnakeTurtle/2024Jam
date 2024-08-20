using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossLives : MonoBehaviour
{
    public int counter = 3;
    public GameObject heart1;
    public GameObject heart2;
    public GameObject heart3;

    void takeLive()
    {
        counter--;
        if(counter == 2)
        {
            heart1.SetActive(false);
        }
        else if(counter == 1)
        {
            heart2.SetActive(false);
        }
        else
        {
            heart3.SetActive(false);
        }
    }
}
