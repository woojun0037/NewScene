using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potion : MonoBehaviour
{
    public GangrimSkillUi ui;
   
    public GameObject HPitem;
    public GameObject respwan;
    public GameObject dark;
    public GameObject PotionPickUpEffect;

    // Start is called before the first frame update
    void Start()
    {

    }

    public void HP()
    {
        Instantiate(PotionPickUpEffect, HPitem.transform.position, HPitem.transform.rotation);
    }

    public void ReSpwan()
    {
        Instantiate(PotionPickUpEffect, respwan.transform.position, respwan.transform.rotation);
    }

    public void Darkitem()
    {
        Instantiate(PotionPickUpEffect, dark.transform.position, dark.transform.rotation);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Main_gangrim")
        {
            if(gameObject == HPitem)
            {
                HP();
                ui.HP_item.SetActive(true);
                ui.HPitemOn = true;
            }
            if(gameObject == respwan)
            {
                ReSpwan();
                ui.RESPWAN_item.SetActive(true);
                ui.RESPWAN_itemOn = true;
            }
            if(gameObject == dark)
            {
                Darkitem();
                ui.DARKPILL_item.SetActive(true);
                ui.DarkPillItemOn = true;
            }
        }
    }
}
