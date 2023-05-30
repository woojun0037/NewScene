using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindSkillHitCheck : MonoBehaviour
{
    public bool HitCheck = false;

    public void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Monster")
        {
            HitCheck = true;
        }
    }
}
