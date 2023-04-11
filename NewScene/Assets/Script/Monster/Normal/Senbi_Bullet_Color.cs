using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Senbi_Bullet_Color : MonoBehaviour
{
    Renderer bulletcolor;
    public GameObject obj3d; //총알 오브젝트

    bool onetime;

    // Start is called before the first frame update
    void Start()
    {
        bulletcolor = obj3d.GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!onetime)
        {
            onetime = true;
            StartCoroutine("ObjFadeOut");
        }
    }


    IEnumerator ObjFadeOut()
    {
        for (int i = 10; i >= 0; i--)
        {

            float f = i / 10.0f;
            Color c = bulletcolor.material.color;
            c.a = f;
            bulletcolor.material.color = c;

            yield return new WaitForSeconds(0.07f);
        }
    }

}
