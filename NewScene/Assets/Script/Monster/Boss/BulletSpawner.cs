using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpawner : MonoBehaviour
{
    public GameObject BulletPrefab;
   
    private Transform target;
    
    private float TimeAfterSpawn;
    
    public float Spawn_Rate;
    public float Speed;
  
    // Start is called before the first frame update
    void Start()
    {
        //rigid = GetComponent<Rigidbody>();
        TimeAfterSpawn = 0f;
        
        //target = FindObjectOfType<Main_Player>().transform;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        BulletSpwanPosition();
        BulletSpwaner();
    }
    
    public void BulletSpwaner()
    {
        TimeAfterSpawn += Time.deltaTime;

        if (TimeAfterSpawn >= Spawn_Rate)
        {
            TimeAfterSpawn = 0f; 
            GameObject Bullet = Instantiate(BulletPrefab, transform.position, transform.rotation);
        }
    }
    public void BulletSpwanPosition()
    {
        float fMove = Time.deltaTime * Speed;
        gameObject.transform.Translate(Vector3.forward * -fMove);
    }
}
