using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPitem : MonoBehaviour
{
    public GangrimSkillUi ui;
    public GameObject HPPotionPickUpEffect;

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Main_gangrim")
        {
            GameObject effect =Instantiate(HPPotionPickUpEffect, transform.position, transform.rotation);
            Destroy(gameObject);
            Destroy(effect, 1f);
            ui.HP_item.SetActive(true);
        }
    }
}
