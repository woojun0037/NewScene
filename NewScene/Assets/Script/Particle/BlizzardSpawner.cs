using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class BlizzardSpawner : MonoBehaviour
{
    public string[] tagsToCheck;
    public float impactRaidus;
    public float destroyDelay;

    private GameObject ImpactFX;

    void Start()
    {
        ImpactFX = transform.Find("ImpactFX").gameObject;

    }

    private void OnTriggerEnter(Collider other)
    {
        if(tagsToCheck.Contains(other.tag))
        {
            Collider[] objectsInRnage = Physics.OverlapSphere(transform.position, impactRaidus);

            foreach(Collider col in objectsInRnage)
            {
                Rigidbody enemy = col.GetComponent<Rigidbody>();
            }

        }
    }
}

