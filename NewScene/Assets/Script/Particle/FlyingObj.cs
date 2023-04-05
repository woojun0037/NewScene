using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class FlyingObj : MonoBehaviour
{
    public string[] tagsToCheck;
    public float impactRaidus;
    public float destroyDelay;
    public float damage;
    private GameObject ImpactFX;

    void Start()
    {
        ImpactFX = this.gameObject;
    }

    private void Update()
    {
        Destroy(ImpactFX, 2f);
    }

    private void OnParticleCollision(GameObject other)
    {
        if (other.tag == "Monster")
        {
            Enemy Eny = other.GetComponent<Enemy>();
            Eny.curHearth -= damage;
        }
    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    if (tagsToCheck.Contains(other.tag))
    //    {
    //        Collider[] objectsInRnage = Physics.OverlapSphere(transform.position, impactRaidus);

    //        foreach (Collider col in objectsInRnage)
    //        {
    //            Rigidbody Monster = col.GetComponent<Rigidbody>();

    //            if (Monster != null)
    //            {

    //                Destroy(Monster.gameObject);
    //            }
    //        }

    //        ImpactFX.SetActive(true);

    //        ImpactFX.transform.SetParent(null);

    //        Destroy(ImpactFX, destroyDelay);

    //        Destroy(gameObject); 
    //    }
    //}
}
