using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BOSS : MonoBehaviour
{
    public List<Transform> shots = new List<Transform>();
    public GameObject bullet;
    public int currentShot;
    public bool alreadyShot=false;
    public float shotTimer;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!alreadyShot)
        {
            Shoot();
        }

    }
    void Shoot()
    {
        alreadyShot = true;

        if (currentShot < shots.Count)
        {
            Instantiate(bullet, shots[currentShot].transform.position, Quaternion.identity);
            currentShot++;
            if(currentShot == shots.Count)
            {
                currentShot = 0;
            }
        }
        Invoke("ResetShot", shotTimer);
    }
    void ResetShot()
    {
        alreadyShot = false;
    }
}
