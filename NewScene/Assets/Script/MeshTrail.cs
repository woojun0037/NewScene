using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshTrail : MonoBehaviour
{
    public float activeTime = 2f;

    [Header("Mesh Related")]
    public float meshRefreshRate = 0.1f;

    private bool isTrailActive;
    private SkinnedMeshRenderer[] skinnedMeshRenderers;

    void Start()
    {
        
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.X) && !isTrailActive)
        {
            TrailUse();
        }
    }

    public void TrailUse()
    {
        isTrailActive = true;
        StartCoroutine(ActiveTrailCor(activeTime));
    }

    IEnumerator ActiveTrailCor(float timeActive)
    {
        while (timeActive > 0)
        {
            timeActive -= meshRefreshRate;

            if(skinnedMeshRenderers == null)
            {
                skinnedMeshRenderers = GetComponentsInChildren<SkinnedMeshRenderer>();

                for(int i=0; i < skinnedMeshRenderers.Length; i++)
                {
                    GameObject gObj = new GameObject();

                    gObj.AddComponent<MeshRenderer>();
                    gObj.AddComponent<MeshFilter>();
                }
            }
            yield return new WaitForSeconds(meshRefreshRate);

        }
        isTrailActive = false;   
    }
}
