using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawn : MonoBehaviour
{
    public GameObject itemPrefab;
    [SerializeField] float itemYpos = 0.5f;

    private void OnDisable()
    {
        Vector3 dir = transform.position;
        dir.y = transform.position.y + itemYpos;

        GameObject spawnedItem = Instantiate(itemPrefab, dir, itemPrefab.transform.rotation);
    }

}
