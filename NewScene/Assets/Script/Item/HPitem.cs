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
            Instantiate(HPPotionPickUpEffect, transform.position, transform.rotation);
            Destroy(gameObject);
            ui.HP_item.SetActive(true);
        }
    }
}
