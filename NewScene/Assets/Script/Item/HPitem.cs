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
            ui = FindObjectOfType<GangrimSkillUi>();
            GameObject effect =Instantiate(HPPotionPickUpEffect, transform.position, transform.rotation);
            Destroy(effect, 1f);
            ui.HPitemOn = true;
            ui.HP_item.SetActive(true);
            Destroy(gameObject);
        }
    }
}
