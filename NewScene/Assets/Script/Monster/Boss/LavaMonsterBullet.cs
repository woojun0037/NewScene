using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaMonsterBullet : MonoBehaviour
{
    public GameObject bomb;
    public GameObject bomb2;

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Main_gangrim")
        {
            Invoke("EnableCollider", 0.6f);

            bomb2.SetActive(false);
            bomb.SetActive(true);
        }
    }

    private void EnableCollider()
    {
        BoxCollider[] boxColliders = GetComponents<BoxCollider>();
        foreach (var boxCollider in boxColliders)
        {
            if (boxCollider != null)
            {
                boxCollider.enabled = false;
            }
        }

    }
}
