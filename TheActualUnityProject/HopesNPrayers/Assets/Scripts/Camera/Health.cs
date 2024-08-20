using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{

    public Image healthBar;
    public float healthAmount = 100f;
    public GameObject deathScreen;
    // Start is called before the first frame update
    public void TakeDamage(int damage)
    {
        healthAmount -= damage;
        healthBar.fillAmount = healthAmount / 100f;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            TakeDamage(1);
        }
        if(healthAmount <= 0)
        {
            deathScreen.SetActive(true);
            Destroy(gameObject);
        }
    }
}
