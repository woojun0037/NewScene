using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagDoll_delete : MonoBehaviour
{
    public Renderer ragdollcolor;
    public GameObject obj3d;

    float time;
    bool onetime;

    // Start is called before the first frame update
    void Start()
    {
        time = 0f;
        onetime = false;
        ragdollcolor = obj3d.GetComponent<Renderer>();
        StartCoroutine("ObjFadeOut");
    }

    // Update is called once per frame
    void Update()
    {
        DeleteRagDoll();
    }

    void DeleteRagDoll()
    {
        time += Time.deltaTime;

        if (time >= 5f)
        {
            StopCoroutine("ObjFadeOut");
            gameObject.SetActive(false);
            //Destroy(gameObject);
        }
    }

    IEnumerator ObjFadeOut()
    {
        for (int i = 10; i >= 0; i--)
        {
            Debug.Log("ObjFadeOut");
            float f = i / 10.0f;
            Color c = ragdollcolor.material.color;
            c.a = f;
            ragdollcolor.material.color = c;

            yield return new WaitForSeconds(0.3f);
        }
    }
}
