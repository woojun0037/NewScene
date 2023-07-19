using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialChange : MonoBehaviour
{
    public Renderer bodyRender;
    public Material M_originTiger, M_damageTiger;

    // Start is called before the first frame update
    void Start()
    {
        //M_originTiger = bodyRender.sharedMaterial;
        bodyRender.sharedMaterial = M_originTiger;
    }


    public void GetDamageMaterial()
    {
        StartCoroutine(ChangeMaterial());
    }

    IEnumerator ChangeMaterial()
    {
        bodyRender.sharedMaterial = M_damageTiger;

        yield return new WaitForSeconds(0.4f);
        bodyRender.sharedMaterial = M_originTiger;

    }
}
