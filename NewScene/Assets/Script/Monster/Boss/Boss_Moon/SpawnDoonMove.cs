using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnDoonMove : MonoBehaviour
{
    public Rigidbody PatternBullet;

    private void Start()
    {
        StartCoroutine(Attack());
    }

    IEnumerator Attack() {

        float bulletTime = 0;

        float elapsedTime = 0;
        float speed = 10f;


        while (elapsedTime < 4f)
        {
            transform.Translate(-1 * speed * Time.deltaTime, 0, 0); //돌아가 있는데 축 때문에 -1이동

            bulletTime += Time.deltaTime;
            elapsedTime += Time.deltaTime;

            if (bulletTime > 1f)
            {
                Rigidbody bullet = Instantiate(PatternBullet, transform.position, transform.rotation);
                //bullet.velocity = transform.forward * 10;

                //Rigidbody bulle2t = Instantiate(PatternBullet, transform.position, transform.rotation);
                //bulle2t.velocity = transform.forward * -10;
                bulletTime = 0;
            }

            yield return null;
        }

        yield return new WaitForSeconds(0.5f);
        DestroyObj();
    }

    void DestroyObj()
    {
        Destroy(gameObject);
    }

}
