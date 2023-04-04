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
            amount -= 1;

            //var spawnPosition = transform.position + new Vector3(Random.insideUnitCircle.x * spawnRaius, 0,
            //                                                     Random.insideUnitCircle.y * spawnRaius) + spawnOffset;

            var obj = Instantiate(FlyingObject, RainPos, Quaternion.identity);

            var forceDirection = transform.position - (transform.position + spawnOffset);

            //obj.GetComponent<Rigidbody>().AddForce(forceDirection * spawnForce, ForceMode.VelocityChange);

            //Destroy(obj, destroyDelay);

            //if (amount <= 0)
            //{
            //    isDead = true;
            //    Destroy(gameObject, destroyDelay);
            //}
        }
    }
}

