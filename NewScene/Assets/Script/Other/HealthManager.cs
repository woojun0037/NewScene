using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    public float maxHealth;
    public float currentHealth;
    
    void Start()
    {
        
    }

    
    void Update()
    {
       
    }
  
    public void HurtP
        
        (float damage)
    {
        currentHealth -= damage;
        FindObjectOfType<Main_Player>().PlayerHP(currentHealth);
    }


    public void HealPlayer(float healAmount)
    {
        currentHealth += healAmount;

        if(currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
    }
}
