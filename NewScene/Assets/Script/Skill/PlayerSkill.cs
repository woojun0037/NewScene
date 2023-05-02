using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;

public class PlayerSkill : MonoBehaviour
{

    private Vector3 mousePos;
    private Vector3 Dir;
    private Vector3 DashTarget;

    //WindStorm windskill;
    Main_Player player;

    public bool isSkillOn = false;
    public bool isCollision = false;
    public bool isSkillUse = true;
    public bool isTest;

    public bool WindSkillCheck;
    public bool RainSkillCheck;

    private bool CloudisDelay = true;
    private bool isDash;

    public static PlayerSkill Instance;

    public Canvas WindDirection;

    public TrailRenderer trailEffect;
    public Transform CloudPos;
    public GameObject Cloudprab;
    public GameObject WindSkillPrefab;

    private Vector3 posUp;
    public Vector3 postion;

    [SerializeField] private BlizzardSpawner RainSkill;
    public Canvas ability2Canvas;
    public Image targetCircle;
    public Image SkillRange;

    public float RainSkillTime;
    public float RainSkillCool;
    public float maxAbilityDistance;


    private void Start()
    {
        player = GetComponent<Main_Player>();
        targetCircle.GetComponent<Image>().enabled = false;
        SkillRange.GetComponent<Image>().enabled = false;
        WindDirection.enabled = false;
        Instance = this;
    }

    void Update()
    {

        CloudSkill();
        RainDropSkill();
        WindSkillRange();

        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                if (hit.collider.tag == "Platform" && Vector3.Distance(hit.point, transform.position) < 15f)
                {
                    player.Anim.SetTrigger("Dash");
                    DashTarget = hit.point;
                    Dir = DashTarget - transform.position;
                    isDash = true;
                    player.isAttack = true;
                    Debug.Log(DashTarget);
                }
            }
        }
        if (isDash)
        {
            Dash();
        }

        if (isSkillUse)
        {
            int layerMask = 1 << LayerMask.NameToLayer("Platform");

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
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
            RainSkill.RainPos = new Vector3(transform.position.x, transform.position.y, transform.position.z) + hitPosDir * distance;
            ability2Canvas.transform.position = (newHitPos);
        }
    }

    public void Dash()
    {
        if (Vector3.Distance(DashTarget, transform.position) > 0.2f)
        {
            transform.forward = Dir;
            transform.position = transform.position + Dir * Time.deltaTime * 3f;
        }
        else
        {
            isDash = false;
            player.isAttack = false;
        }
    }

    public GameObject WindTest()
    {
        GameObject temp = Instantiate(WindSkillPrefab, transform.position, Quaternion.identity);
        temp.transform.rotation = this.transform.rotation;
        return temp;
    }

    public void WindSkillRange()
    {
        if (Input.GetKeyDown(KeyCode.Q) && isSkillUse && !WindSkillCheck)
        {
            WindDirection.enabled = true;
            WindSkillCheck = true;
            isSkillUse = false;
        }
        else if (Input.GetKeyDown(KeyCode.Q) && !isSkillUse)
        {
            WindDirection.enabled = false;
            WindSkillCheck = false;
            isSkillUse = true;
        }
        if(Input.GetMouseButtonDown(0) && !isSkillUse)
        {
            isSkillUse = true;
            WindDirection.enabled = false;
            player.Skill_Q();
        }
    }

    public void CloudSkill()
    {
        mousePos = Input.mousePosition;
        if (Input.GetKeyDown(KeyCode.E))//구름 스킬
        {
            Invoke("DestroyCloudShoot", 5f);

            if (CloudisDelay == true)
            {
                CloudisDelay = false;
                Ray ray = Camera.main.ScreenPointToRay(mousePos);
                Debug.DrawRay(ray.origin, ray.direction * 100f, Color.red);

                if (Physics.Raycast(ray, out RaycastHit rayHit))
                {
                    if (rayHit.collider.tag == "Platform")
                    {
                        transform.LookAt(rayHit.point);
                    }
                }
                StartCoroutine(CloudTimeCor());
            }
        }
    }

    public void RainDropSkill()
    {

        if (Input.GetKeyDown(KeyCode.R) && isSkillUse && !RainSkillCheck)
        {
            SkillRange.GetComponent<Image>().enabled = true;
            targetCircle.GetComponent<Image>().enabled = true;
            RainSkillCheck = true;
        }
        else if (Input.GetKeyDown(KeyCode.R))
        {
            SkillRange.GetComponent<Image>().enabled = false;
            targetCircle.GetComponent<Image>().enabled = false;
            RainSkillCheck = false;
        }
        if (Input.GetMouseButtonDown(0) && RainSkillCheck == true)
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
                StartCoroutine(RainCor());
            }
            else if (isTest)
            {
                isSkillUse = true;
                isSkillOn = false;
                RainSkillTime = 0;
            }
        }
    }

    IEnumerator RainCor()
    {
        while (RainSkillTime <= RainSkillCool)
        {
            RainSkillTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        Rainnig();
        isTest = !isTest;
    }

    IEnumerator CloudTimeCor()
    {
        GameObject intantCloud = Instantiate(Cloudprab, CloudPos.position, CloudPos.rotation);
        Rigidbody CloudRigid = intantCloud.GetComponent<Rigidbody>();
        CloudRigid.velocity = transform.forward * 10;
        Destroy(intantCloud, 2);
        yield return new WaitForSeconds(3f);
        CloudisDelay = true;
    }
}