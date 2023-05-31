using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillZip : MonoBehaviour
{
    public static SkillZip instance;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
}
