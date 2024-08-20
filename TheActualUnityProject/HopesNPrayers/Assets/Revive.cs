using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Revive : MonoBehaviour
{
    public void ReloadScene()
    {
        SceneManager.LoadScene("Level 1 Prototype");
    }
}
