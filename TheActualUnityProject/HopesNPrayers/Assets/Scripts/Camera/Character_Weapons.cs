using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character_Weapons : MonoBehaviour
{
    public float timeBetweenShots;
    public bool justShot = false;

    //Set in editor for prefab
    public GameObject biggify;
    public GameObject miniaturize;
    public Transform gun;

    private Camera mainCam;
    private Vector3 mousePos;

    private void Start()
    {
        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }
    private void Update()
    {
        //this just makes the gun follow the mouse
        mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);
        Vector3 rotation = mousePos - transform.position;
        float rotZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rotZ);

        if(!justShot && Input.GetKeyDown(KeyCode.Mouse0))
        {
            justShot = true;
            Instantiate(biggify, gun.position, Quaternion.identity);
            Invoke("ResetShot", timeBetweenShots);
        }
        if (!justShot && Input.GetKeyDown(KeyCode.Mouse1))
        {
            justShot = true;
            Instantiate(miniaturize, gun.position, Quaternion.identity);
            Invoke("ResetShot", timeBetweenShots);
        }
    }
    private void ResetShot()
    {
        justShot = false;
    }
}
