using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReSpwanitem : MonoBehaviour
{
    public GangrimSkillUi ui;
    public GameObject ReSpwanPotionPickUpEffect;

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Main_gangrim")
        {
            Instantiate(ReSpwanPotionPickUpEffect, transform.position, transform.rotation);
            Destroy(gameObject);
            ui.RESPWAN_item.SetActive(true);
        }
    }
}
