using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;

public class PlayerSkill : MonoBehaviour
{

    private Vector3 mousePos;
    [Header("WindSkill")]
    windStorm windskill;

    public bool isSkillOn = false;
    public bool isCollision = false;
    public bool isSkillUse = true;

    [Header("CloudSkill")]
    public static PlayerSkill Instance;
    public TrailRenderer trailEffect;
    public Transform CloudPos;
    public GameObject Cloudprab;
    public GameObject WindSkillPrefab;
    bool CloudisDelay = true;

    [Header("RainDropSkill")]
    private Vector3 posUp;
    public Vector3 postion;

    [SerializeField] private BlizzardSpawner RainSkill;
    public Canvas ability2Canvas;
    public Image targetCircle;
    public Image SkillRange;

    public float RainSkillTime;
    public float RainSkillCool;
    public float maxAbilityDistance;
    public bool RainSkillCheck;
    public bool isTest;

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

        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);


        //비 스킬 이미지 인풋
        if(isSkillUse)
        {
           
            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                if (hit.collider.gameObject != this.gameObject)
                {
                    posUp = new Vector3(hit.point.x, 0, hit.point.z);
                    postion = hit.point;
                    
                }
            }
            //비 스킬 이미지 인풋
            var hitPosDir = (new Vector3(hit.point.x, 2, hit.point.z) - transform.position).normalized;
            float distance = Vector3.Distance(hit.point, transform.position);
            distance = Mathf.Min(distance, maxAbilityDistance);

            var newHitPos = transform.position + hitPosDir * distance;
            RainSkill.RainPos = new Vector3(transform.position.x, transform.position.y + 10, transform.position.z) + hitPosDir * distance;
            ability2Canvas.transform.position = (newHitPos);
        }
    }

    public void WindSkill()
    {
        if (Input.GetKey(KeyCode.E))
        {
            
        }
    }

    public GameObject WindTest()
    {
        GameObject temp = Instantiate(WindSkillPrefab, transform.position, Quaternion.identity);
        temp.transform.rotation = this.transform.rotation;
        return temp;
    }

    public void CloudSkill()
    {
        mousePos = Input.mousePosition;
        if (Input.GetKeyDown(KeyCode.R))//구름 스킬
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
        if (Input.GetKeyDown(KeyCode.F) && isSkillUse)
        {
            SkillRange.GetComponent<Image>().enabled = true;
            targetCircle.GetComponent<Image>().enabled = true;
            RainSkillCheck = true;
        }
        if (Input.GetMouseButton(0) && RainSkillCheck == true)
        {
            SkillRange.GetComponent<Image>().enabled = false;
            targetCircle.GetComponent<Image>().enabled = false;
            isSkillOn = true;
            RainSkillCheck = false;
            Rainnig();
        }
    }

    void Rainnig()
    {
        if (isSkillOn == true)
        {
            if (!isTest)
            {
                isSkillUse = false;
                RainSkill.RainDrop();
                isTest = true;
                StartCoroutine(TestCor());
            }
            else if (isTest)
            {
                isSkillUse = true;
                isSkillOn = false;
                RainSkillTime = 0;
            }
        }
    }

    IEnumerator TestCor()
    {
        while(RainSkillTime <= RainSkillCool)
        {
            RainSkillTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        Rainnig();
        isTest = !isTest;
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
}