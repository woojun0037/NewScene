using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudHit : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Monster")
        {
            other.GetComponent<Enemy>().curHearth -= 3f;
            Destroy(gameObject);
        }
    }
}
