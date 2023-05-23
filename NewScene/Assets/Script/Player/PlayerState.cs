using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : MonoBehaviour
{
    public Potion item;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "HPitem")
        {

        }

        if (other.tag == "Respawnitem")
        {

        }

        if (other.tag == "Darkitem")
        {

        }
    }
}
