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
}
