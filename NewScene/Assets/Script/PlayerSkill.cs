using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class PlayerSkill : MonoBehaviour
{
    [SerializeField] private BlizzardSpawner FSkill;
    public static PlayerSkill Instance;

    public TrailRenderer trailEffect;
    public Transform CloudPos;
    public GameObject Cloudprab;
    public Transform target;// ��ä�ÿ� ���ԵǴ��� �Ǻ��� Ÿ��

    public float RainSkillTime;
    public float RainSkillCool;

    public float angleRange = 30f;
    public float radius = 3f;

    private Vector3 mousePos;
    private Color _blue = new Color(0f, 0f, 1f, 0.2f);
    private Color _red = new Color(1f, 0f, 0f, 0.2f);

    bool isSkillOn = false;
    bool isCollision = false;
    bool CloudisDelay = true;

    private void Start()
    {
        
    }

    void Update()
    {
        Skill();
        TrySkill();
    }

    public void Skill()
    {
        if (Input.GetKey(KeyCode.L))
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


        mousePos = Input.mousePosition;
        if (Input.GetKeyDown(KeyCode.R))//���� ��ų
        {
            Invoke("DestroyCloudShoot", 5f);

            if (CloudisDelay == true)
            {
                CloudisDelay = false;
                Debug.Log("Kecode.R");

                Ray ray = Camera.main.ScreenPointToRay(mousePos);
                Debug.DrawRay(ray.origin, ray.direction * 100f, Color.red);

                if (Physics.Raycast(ray, out RaycastHit rayHit))
                {
                    if (rayHit.collider.tag == "Platform")
                    {
                        transform.LookAt(rayHit.point);
                    }
                }
                StartCoroutine(CloudTime());
            }
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            isSkillOn = true;
            TrySkill();
        }
    }

    public void TrySkill()
    {
        if(isSkillOn == true)
        {
            if(RainSkillTime < 3)
            { 
                FSkill.RainDrop();
                RainSkillTime += Time.deltaTime;
            }
            else if(RainSkillTime > 3)
            {
                isSkillOn = false;
                RainSkillTime = 0;
            }
        }
    }

    IEnumerator CloudTime()
    {
        GameObject intantCloud = Instantiate(Cloudprab, CloudPos.position, CloudPos.rotation);
        Rigidbody CloudRigid = intantCloud.GetComponent<Rigidbody>();
        CloudRigid.velocity = transform.forward * 50;
        Destroy(intantCloud, 3);
        yield return new WaitForSeconds(0.1f);
        CloudisDelay = true;
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