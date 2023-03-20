using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class PlayerSkill : MonoBehaviour
{
    public TrailRenderer trailEffect;
    public Transform CloudPos;
    public Rigidbody Cloudprab;
    public GameObject Cloud;

    public Transform target;    // ��ä�ÿ� ���ԵǴ��� �Ǻ��� Ÿ��
    public float angleRange = 30f;
    public float radius = 3f;
    
    Color _blue = new Color(0f, 0f, 1f, 0.2f);
    Color _red = new Color(1f, 0f, 0f, 0.2f);

    bool isCollision = false;
    bool CloudisDelay;

    void Update()
    {
        Skill();
    }

    void Start()
    {
        GameObject CloudSkill = Instantiate(Cloud);
    }
    
    void Skill()
    {
       if(Input.GetKey(KeyCode.L))
        {
            Vector3 interV = target.position - transform.position;

            // target�� �� ������ �Ÿ��� radius ���� �۴ٸ�
            if (interV.magnitude <= radius)
            {
                // 'Ÿ��-�� ����'�� '�� ���� ����'�� ����
                float dot = Vector3.Dot(interV.normalized, transform.forward);

                // �� ���� ��� ���� �����̹Ƿ� ���� ����� cos�� ���� ���ؼ� theta�� ����
                float theta = Mathf.Acos(dot);

                // angleRange�� ���ϱ� ���� degree�� ��ȯ
                float degree = Mathf.Rad2Deg * theta;

                // �þ߰� �Ǻ�
                if (degree <= angleRange / 2f)
                    isCollision = true;
                else
                    isCollision = false;
            }
            else
                isCollision = false;
        }

       if(Input.GetKey(KeyCode.R))
        {
           if(CloudisDelay == false)
           {
               CloudisDelay = true;
               StartCoroutine("CloudTime");
               Vector3 spawn = transform.position;
               spawn.y = 1f;
               Rigidbody CloudRigid = Instantiate(Cloudprab, transform.position, transform.rotation);
               CloudRigid.velocity = transform.forward * 20;
           }
        }
    }

    IEnumerable CloudTime()
    {
        yield return new WaitForSeconds(1f);
        CloudisDelay = false;
    }

    // ����Ƽ �����Ϳ� ��ä���� �׷��� �޼ҵ�
    private void OnDrawGizmos()
    {
        Handles.color = isCollision ? _red : _blue;
        // DrawSolidArc(������, ��ֺ���(��������), �׷��� ���� ����, ����, ������)
        Handles.DrawSolidArc(transform.position, Vector3.up, transform.forward, angleRange / 2, radius);
        Handles.DrawSolidArc(transform.position, Vector3.up, transform.forward, -angleRange / 2, radius);
    }
}