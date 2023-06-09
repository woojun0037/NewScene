using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BlizzardSpawner : MonoBehaviour
{
    public GameObject FlyingObject;

    public float destroyDelay;
    public float spawnInterval;
    public float spawnRaius;
    public float SpawnTime;
    public float spawnForce;
    public float spawnIntervalTimer;

    public Vector3 spawnOffset;
    public Vector3 RainPos;


    public void RainDrop()
    {
        var obj = Instantiate(FlyingObject, RainPos, Quaternion.Euler(new Vector3(-90, 0, 0)));
        Destroy(obj,destroyDelay);
    }
}

