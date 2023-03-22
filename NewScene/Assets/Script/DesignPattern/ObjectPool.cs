using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool Instance;

    [SerializeField] 
    private GameObject PoolingObjectPrefab;
    
    private Queue<PlayerSkill> PoolingObjectQueue = new Queue<PlayerSkill>();

    private void Awake()
    {
        Instance = this;
        Initialize(10);
    }

    private void Initialize(int initCount)
    {
        for (int i = 0; i < initCount; i++)
        {
            PoolingObjectQueue.Enqueue(CreateNewObject());
        }
    }

    private PlayerSkill CreateNewObject()
    {
        var newObj = Instantiate(PoolingObjectPrefab, transform).GetComponent<PlayerSkill>();
        newObj.gameObject.SetActive(false);
        return newObj;
    }

    public static PlayerSkill GetObject()
    {
        if(Instance.PoolingObjectQueue.Count > 0)
        {
            var Obj = Instance.PoolingObjectQueue.Dequeue();
            Obj.transform.SetParent(null);
            Obj.gameObject.SetActive(true);
            return Obj;
        }
        else
        {
            var newObj = Instance.CreateNewObject();
            newObj.transform.SetParent(null);
            newObj.gameObject.SetActive(false);
            return newObj;
        }
    }

    public static void ReturnObject(PlayerSkill Skill)
    {
        Skill.gameObject.SetActive(false);
        Skill.transform.SetParent(Instance.transform);
        Instance.PoolingObjectQueue.Enqueue(Skill);
    }
}
