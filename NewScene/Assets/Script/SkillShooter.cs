using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillShooter : MonoBehaviour
{
    public TrailRenderer trailEffect;
    public Transform CloudPos;
    public GameObject Cloudprab;

    public void CloudSkillShot()
    {
        var direction = new Vector3(0, 0, 10);
        var CloudSkillShot = ObjectPool.GetObject();
        CloudSkillShot.transform.position = transform.position + direction.normalized;
        CloudSkillShot.Skill();
    }
}
