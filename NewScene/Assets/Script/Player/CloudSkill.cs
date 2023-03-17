using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudSkill : MonoBehaviour
{
    public TrailRenderer trailEffect;
    public Transform CloudPos;
    public GameObject Cloud;
    public Rigidbody Cloudprab;

    void Start()
    {

    }


    void Update()
    {

    }

    public void SkillUse()
    {
        StartCoroutine("useCloud");
    }

    IEnumerator useCloud()
    {
       

        Rigidbody CloudRigid = Instantiate(Cloudprab, transform.position, transform.rotation);
        CloudRigid.velocity = transform.forward * 20;

        //Rigidbody ThrowRockrigid = Instantiate(Boss_Skill_2, transform.position, transform.rotation);
        //ThrowRockrigid.velocity = transform.forward * 20;//  * Time.deltaTime;
        yield return null;
    }
}
