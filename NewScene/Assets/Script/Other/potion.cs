using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potion : MonoBehaviour
{
    public float Healvalue;

    public GameManager Manager;
    public GameObject PotionPickUpEffect;

    public void HPpotion()
    {
        Manager.AddPotion(Healvalue);
        Instantiate(PotionPickUpEffect, transform.position, transform.rotation);
        Destroy(gameObject);
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Main_gangrim")
        {
            HPpotion();
        }
        
    }
}
