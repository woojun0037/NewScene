using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BlizzardSpawner : MonoBehaviour
{
    
    public GameObject FlyingObject;
    public int amount;
    public float destroyDelay;
    public float spawnInterval;
    public float spawnRaius;
    public float spawnForce;
    public float SpawnTime;

    public Vector3 spawnOffset;
    public Vector3 RainPos;

    private bool isDead = false;
    public float spawnIntervalTimer;

    public void RainDrop()
    {
        if (!isDead)
        {
            var obj = Instantiate(FlyingObject, RainPos, Quaternion.identity);

            var forceDirection = transform.position - (transform.position + spawnOffset);
        }
    }
}

