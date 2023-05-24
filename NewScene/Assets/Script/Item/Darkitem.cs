using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Darkitem : MonoBehaviour
{
    public GangrimSkillUi ui;
    public GameObject DarkPotionPickUpEffect;
   
    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Main_gangrim")
        {
            Instantiate(DarkPotionPickUpEffect, transform.position, transform.rotation);
            Destroy(gameObject);
            ui.DARKPILL_item.SetActive(true);
        }
    }
}
