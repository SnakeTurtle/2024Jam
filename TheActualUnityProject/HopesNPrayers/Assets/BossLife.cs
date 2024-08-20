using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossLife : MonoBehaviour
{
    public int counter = 3;
    public GameObject heart1;
    public GameObject heart2;
    public GameObject heart3;

    public GameObject winScreen;


    public void takeLive()
    {
        counter--;
        if (counter == 2)
        {
            heart1.SetActive(false);
        }
        else if (counter == 1)
        {
            heart2.SetActive(false);
        }
        else
        {
            heart3.SetActive(false);
            winScreen.SetActive(true);
            Destroy(gameObject);
        }
    }
}
