using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindSkillHitCheck : MonoBehaviour
{
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Monster")
        {
            other.GetComponent<Enemy>().curHearth -= 3f;
        }
    }
}
