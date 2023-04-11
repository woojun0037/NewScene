using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BlizzardSpawner : MonoBehaviour
{

    public GameObject FlyingObject;
    public float destroyDelay;
    public float spawnInterval;
    public float spawnRaius;
    public float spawnForce;
    public float SpawnTime;
    public float spawnIntervalTimer;

    public Vector3 spawnOffset;
    public Vector3 RainPos;


    public void RainDrop()
    {
        var obj = Instantiate(FlyingObject, RainPos, Quaternion.identity);
        var forceDirection = transform.position - (transform.position + spawnOffset);
    }
}

