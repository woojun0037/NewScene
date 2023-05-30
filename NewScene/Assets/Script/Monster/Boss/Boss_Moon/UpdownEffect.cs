using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdownEffect : MonoBehaviour
{
    void OnParticleCollision(GameObject other)

    {
        if (other.gameObject.tag == "Main_gangrim")
        {
            FindObjectOfType<HealthManager>().HurtPlayer(1);
        }
    }

}
