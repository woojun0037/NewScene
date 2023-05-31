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
            ui = FindObjectOfType<GangrimSkillUi>();
            GameObject effect = Instantiate(DarkPotionPickUpEffect, transform.position, transform.rotation);
            Destroy(effect, 1f);
            ui.DarkPillItemOn = true;
            ui.DARKPILL_item.SetActive(true);
            Destroy(gameObject);
        }
    }
}
