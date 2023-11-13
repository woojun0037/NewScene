using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_moonRBullet : MonoBehaviour
{
    public GameObject Range;
    public GameObject Bullet1;
    public GameObject Bullet2;

    void Start()
    {
        Invoke("BulletOn", 2f);
    }

    void BulletOn()
    {
        Range.SetActive(false);
        Bullet1.SetActive(true);
        Bullet2.SetActive(true);
    }

}
