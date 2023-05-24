using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potion : MonoBehaviour
{
    public int value;

    public GameObject HPitem;
    public GameObject respwan;
    public GameObject dark;

    public GameObject PotionPickUpEffect;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        HP();
        ReSpwan();
        darkitem();
    }



    public void HP()
    {
        Instantiate(PotionPickUpEffect, transform.position, transform.rotation);
        Destroy(HPitem);
    }

    public void ReSpwan()
    {
        Instantiate(PotionPickUpEffect, transform.position, transform.rotation);
        Destroy(respwan);
    }

    public void darkitem()
    {
        Instantiate(PotionPickUpEffect, transform.position, transform.rotation);
        Destroy(dark);
    }

}
