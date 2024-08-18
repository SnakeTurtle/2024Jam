using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class characterAttack : MonoBehaviour

{

    //VARIABLES
    public GameObject attackForm;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space)) {
            gameObject.SetActive(false);
            attackForm.SetActive(true);
        }
        if (attackForm.activeSelf)
        {
            attackForm.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, 0);
        }


    }
}
