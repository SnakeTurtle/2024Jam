using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mushroom_AI : MonoBehaviour
{
    public bool justShot = false;
    public GameObject projectile;
    public float timeBetweenShots;

    List<Transform> spawners = new List<Transform>();

    private void Start()
    {
        spawners.Add(transform.GetChild(0).transform);
        spawners.Add(transform.GetChild(1).transform);
        spawners.Add(transform.GetChild(2).transform);
    }
    private void Update()
    {
        if (!justShot)
        {
            justShot = true;
            foreach(Transform t in spawners) { Instantiate(projectile, t.position, t.rotation); }
            Invoke("ResetShot", timeBetweenShots);
        }
    }
    void ResetShot()
    {
        justShot = false;
    }

}
