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
  
    public void HurtPlayer(float damage)
    {
        currentHealth -= damage;
        StartCoroutine(HitPlayerCor(currentHealth));
    }

    IEnumerator HitPlayerCor(float damge)
    { 
        FindObjectOfType<Main_Player>().PlayerHP(damge);
        yield return new WaitForSeconds(1.5f);
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
