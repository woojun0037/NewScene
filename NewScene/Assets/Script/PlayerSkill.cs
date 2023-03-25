using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;

public class PlayerSkill : MonoBehaviour
{
    public Transform player;

    [Header("WindSkill")]
    public Transform target;// ��ä�ÿ� ���ԵǴ��� �Ǻ��� Ÿ��
    public float angleRange = 30f;
    public float radius = 3f;
    private Vector3 mousePos;
    private Color _blue = new Color(0f, 0f, 1f, 0.2f);
    private Color _red = new Color(1f, 0f, 0f, 0.2f);

    bool isSkillOn = false;
    bool isCollision = false;
    bool isSkillUse;

    [Header("CloudSkill")]
    public static PlayerSkill Instance;
    public TrailRenderer trailEffect;
    public Transform CloudPos;
    public GameObject Cloudprab;
    bool CloudisDelay = true;

    [Header("RainDropSkill")]
    private Vector3 posUp;
    private Vector3 postion;

    [SerializeField] private BlizzardSpawner RainSkill;
    public Canvas ability2Canvas;
    public Image targetCircle;
    public Image SkillRange;

    public float RainSkillTime;
    public float RainSkillCool;
    public float maxAbilityDistance;

    private void Start()
    {
        targetCircle.GetComponent<Image>().enabled = false;
        SkillRange.GetComponent<Image>().enabled = false;
    }

    void Update()
    {

        WindSkill();
        CloudSkill();
        RainDropSkill();
        Rainnig();

        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        
        //�� ��ų �̹��� ��ǲ
        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            if (hit.collider.gameObject != this.gameObject)
            {
                posUp = new Vector3(hit.point.x, 0f, hit.point.z);
                postion = hit.point;
            }
        }
        

        //�� ��ų �̹��� ��ǲ
        var hitPosDir = (hit.point - transform.position).normalized;
        float distance = Vector3.Distance(hit.point, transform.position);
        distance = Mathf.Min(distance, maxAbilityDistance);

        var newHitPos = transform.position + hitPosDir * distance;
        ability2Canvas.transform.position = (newHitPos);
    }

    public void WindSkill()
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

    }

    public void CloudSkill()
    {
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
    }

    public void RainDropSkill()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            SkillRange.GetComponent<Image>().enabled = true;
            targetCircle.GetComponent<Image>().enabled = true;
            
        }
        if (Input.GetMouseButton(0))
        {
            SkillRange.GetComponent<Image>().enabled = false;
            targetCircle.GetComponent<Image>().enabled = false;
            isSkillOn = true;
            Rainnig();
        }
    }

    void Rainnig()
    {
        if (isSkillOn == true)
        {
            if (RainSkillTime < RainSkillCool)
            {
                isSkillUse = true;
                RainSkill.RainDrop();
                RainSkillTime += Time.deltaTime;
            }
            else if (RainSkillTime > RainSkillCool)
            {
                isSkillUse = false;
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
        Destroy(intantCloud, 2);
        yield return new WaitForSeconds(3f);
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