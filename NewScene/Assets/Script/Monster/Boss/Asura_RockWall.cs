using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asura_RockWall : MonoBehaviour
{
    public GameObject FireRing;
    public GameObject[] Pos;

    float random;
    float currentrandom;

    Vector3 spawnpos;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(FireLingspawn());
    }

    private IEnumerator FireLingspawn()
    {

        int numFireRings = 30;

        for (int i = 0; i < numFireRings; i++) //불꽃링 발사
        {

            Vector3 spawnpos = RandomFireRingPosition();
            spawnpos.y = transform.position.y + 3f;

            GameObject FireRing_i = Instantiate(FireRing, spawnpos, transform.rotation);
            Rigidbody rb = FireRing_i.GetComponent<Rigidbody>();

            float speed = 15f;//속도

            Vector3 moveDirection = transform.forward * speed;
            rb.velocity = moveDirection;

            yield return new WaitForSeconds(0.8f);
        }

        yield return new WaitForSeconds(2f);
    }

    private Vector3 RandomFireRingPosition() //3가지 위치중 랜덤
    {
        random = Random.Range(0, 4);

        if (random == currentrandom)
        {
            if (random < 3)
                random++;
            else
                random--;
        }

        currentrandom = random;

        if (currentrandom == 0)
        {
             spawnpos = Pos[0].transform.position;
        }
        else if (currentrandom == 1)
        {
             spawnpos = Pos[1].transform.position;
        }
        else if (currentrandom == 2)
        {
             spawnpos = Pos[2].transform.position;

        }
        else if (currentrandom == 3)
        {
            spawnpos = Pos[0].transform.position;
        }
        return spawnpos;
    }

}
