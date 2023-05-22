using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    public Image HPbar;
    public float maxHealth;
    public float currentHealth;
    bool isDamage = false;

    void Start()
    {
        
    }

    
    void Update()
    {
       
    }
  
    public void HurtPlayer(float damage)
    {
        if (!isDamage)
        {
            isDamage = true;
            currentHealth -= damage;
            HPbar.fillAmount = currentHealth / maxHealth;
            StartCoroutine(HitPlayerCor(currentHealth));

        }

    }

    IEnumerator HitPlayerCor(float damge)
    { 
        FindObjectOfType<Main_Player>().PlayerHP(damge);
        yield return new WaitForSeconds(1.5f);
        isDamage = false;
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
