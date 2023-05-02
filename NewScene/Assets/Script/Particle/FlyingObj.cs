using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class FlyingObj : MonoBehaviour
{
    public string[] tagsToCheck;
    public float impactRaidus;
    public float destroyDelay;
    public float damage;
    public float DamgeTime;

    public bool isRain;
    public GameObject ImpactFX;

    void Start()
    {
        
        ImpactFX = this.gameObject;
    }

    private void Update()
    {
        Destroy(ImpactFX, 5f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Monster")
        {
            isRain = true;
            StartCoroutine(HitCor(other.GetComponent<Enemy>()));
        }
    }
    IEnumerator HitCor(Enemy enemy)
    {
        while(isRain)
        {
            yield return new WaitForSeconds(0.8f);
            enemy.curHearth -= 1f;
        }    
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Monster")
        {
            isRain = false;
        }
    }
}
