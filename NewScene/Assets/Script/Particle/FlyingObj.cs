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
    public GameObject ImpactFX;
    Enemy enemy;

    void Start()
    {
        ImpactFX = this.gameObject;
    }

    private void Update()
    {
        Destroy(ImpactFX, 5f);
    }

    //private void OnTriggerEnter(Collider hit)
    //{
    //   if(hit.gameObject.tag == "Monster")
    //    {
    //        enemy.curHearth -= damage;
    //    }
    //}
}
