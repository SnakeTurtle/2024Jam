using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class tutorialText : MonoBehaviour
{
    public Text text1, text2, text2a, text2b, text3, text4, text5, text6, text6a, text7, text8,text9;
    public EdgeOfScreen screenLeft;
    public EdgeOfScreenRight screenRight;
    public float screenNum;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        screenNum = screenRight.screenNum + screenLeft.screenNum;
        if (screenNum == 0)
        {
            text1.enabled = true;
            text2.enabled = false;
            text2a.enabled = false;
            text2b.enabled = false;
        }
        else if (screenNum == 1)
        {
            text1.enabled = false;
            text2.enabled = true;
            text2a.enabled = true;
            text2b.enabled = true;
            text3.enabled = false;
        }
        else if (screenNum == 2)
        {
            text2.enabled = false;
            text3.enabled = true;
            text2a.enabled = false;
            text2b.enabled = false;
        }
        else if (screenNum == 3)
        {
            text3.enabled = false;
            text4.enabled = true;
            text5.enabled = false;
        }
        else if (screenNum == 4)
        {
            text4.enabled = false;
            text5.enabled = true;
            text6.enabled = false;
            text6a.enabled = false;
        }
        else if (screenNum == 5)
        {
            text5.enabled = false;
            text6.enabled = true;
            text6a.enabled = true;
            text7.enabled = false;
        }
        else if (screenNum == 6)
        {
            text6.enabled = false;
            text6a.enabled = false;
            text7.enabled = true;
        }
        else if (screenNum == 7)
        {
            text7.enabled = false;
            text8.enabled = true;
        }else if (screenNum==8)
        {
            text8.enabled=false;
            text9.enabled=true;
        }
    }
}
