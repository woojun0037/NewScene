using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    public int maxHealth;
    public int currentHealth;
    
    void Start()
    {
        
    }

    
    void Update()
    {
       
    }
  
    public void HurtPlayer(int damage)
    {
        currentHealth -= damage;
        FindObjectOfType<Main_Player>().PlayerHP(currentHealth);
    }


    public void HealPlayer(int healAmount)
    {
        currentHealth += healAmount;

        if(currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
    }
}
