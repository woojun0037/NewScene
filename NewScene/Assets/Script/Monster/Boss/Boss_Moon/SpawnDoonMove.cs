using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnDoonMove : MonoBehaviour
{
    public float speed; //임의 20
    public Rigidbody PatternBullet;

    bool bullettime;

    // Update is called once per frame
    void Update()
    {
        if (transform.position.z <= 20)
        {

            if (!bullettime)
            {
                bullettime = true;
                StartCoroutine(Pattern3BulletTime_()); //총알 위 아래 발사 코루틴
            }

            transform.Translate(-1 * speed * Time.deltaTime, 0, 0); //돌아가 있는데 축 때문에 -1이동
        }
        else
        {
            Destroy(gameObject);
        }
    }

    IEnumerator Pattern3BulletTime_()
    {
        yield return new WaitForSeconds(0.5f);

        Rigidbody bullet = Instantiate(PatternBullet, transform.position, transform.rotation);
        bullet.velocity = transform.forward * 10;
        //bullet.transform.Translate(Vector3.forward * Time.deltaTime * 10);

        Rigidbody bulle2t = Instantiate(PatternBullet, transform.position, transform.rotation);
        bulle2t.velocity = transform.forward * -10;
        //bulle2t.transform.Translate(Vector3.forward * Time.deltaTime * 10);
        bullettime = false;
    }
}
