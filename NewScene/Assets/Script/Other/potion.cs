using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class potion : MonoBehaviour
{
    public int value;

    public GameObject PotionPickUpEffect;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Main_gangrim")
        {
            FindObjectOfType<GameManager>().AddPotion(value);
            Instantiate(PotionPickUpEffect, transform.position, transform.rotation); 
            Destroy(gameObject);
        }
    }
}
