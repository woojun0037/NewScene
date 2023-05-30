using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;

public class PlayerSkill : MonoBehaviour
{

    public static PlayerSkill Instance;
    public Vector3 mousePos;
    private Vector3 Dir;
    private Vector3 DashTarget;

    Main_Player player;
    [Header("Script")]
    public Gauge gauge;
    public HitScript hitScript;

    [Header("SkillCheck1")]
    public bool isSkillOn = false;
    public bool isSkillUse = true;
    public bool isTest;

    [Header("SkillCheck2")]
    public bool RainSkillCheck = false;
    public bool WindSkillCheck = false;
    public bool DarkSkillUse = false;
    public bool CloudSkillCheck = false;
    private bool isDash;

    [Header("GameObject")]
    public TrailRenderer trailEffect;
    public GameObject FlyingObject;
    public GameObject CloudPos;
    public GameObject WindSkillPrefab;
    public GameObject WindHitBox;

    [Header("position")]
    public Vector3 posUp;
    public Vector3 postion;
    public Vector3 RainPos;

    [Header("UI")]
    public Canvas WindDirection;
    public Canvas ability2Canvas;
    public Image targetCircle;
    public Image SkillRange;

    [Header("SkillTime")]
    public float RainSkillTime;
    public float RainSkillCool;
    public float maxAbilityDistance;
    public float DarkSkill;

    private void Start()
    {
        gauge = FindObjectOfType<Gauge>();
        player = GetComponent<Main_Player>();
        targetCircle.GetComponent<Image>().enabled = false;
        SkillRange.GetComponent<Image>().enabled = false;
        CloudPos.gameObject.SetActive(false);
        WindDirection.enabled = false;
        Instance = this;
    }

    void Update()
    {
        WindSkillRange();
        CloudSkill();
        RainDropSkill();
        DarknessSKill();

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
            RainPos = new Vector3(transform.position.x, transform.position.y, transform.position.z) + hitPosDir * distance;
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

    public void WindTest()
    {
        WindSkillPrefab.transform.position = new Vector3(transform.position.x, transform.position.y + 3f, transform.position.z);
        WindSkillPrefab.transform.rotation = this.transform.rotation;
        WindSkillPrefab.SetActive(true);
        WindHitBox.SetActive(true);
        StartCoroutine(WindHitBoxCor());
    }

    IEnumerator WindHitBoxCor()
    {
        WindHitBox.SetActive(false);
        yield return null;
    }

    public void WindSkillRange()
    {
        if (Input.GetKeyDown(KeyCode.Q) && isSkillUse && !WindSkillCheck)
        {
            WindDirection.enabled = true;
            WindSkillCheck = true;
            GangrimSkillUi.instance.CurrentSkillUI("wind");
            isSkillUse = false;
        }
        else if (Input.GetKeyDown(KeyCode.Q) && !isSkillUse)
        {
            WindDirection.enabled = false;
            WindSkillCheck = false;
            isSkillUse = true;
        }
        if (Input.GetMouseButtonDown(0) && !isSkillUse)
        {
            isSkillUse = true;
            WindDirection.enabled = false;
            WindSkillCheck = false;
            player.Skill_Q();
        }
    }

    public void CloudSkill()
    {
        mousePos = Input.mousePosition;

        Ray ray = Camera.main.ScreenPointToRay(mousePos);
        Debug.DrawRay(ray.origin, ray.direction * 100f, Color.red);
        int layerMask = 1 << LayerMask.NameToLayer("Platform");

        if (Physics.Raycast(ray, out RaycastHit rayHit, Mathf.Infinity, layerMask))
        {
            if (rayHit.collider.tag == "Platform" && Input.GetKey(KeyCode.E) && !CloudSkillCheck)
            {
                transform.LookAt(rayHit.point);
                CloudPos.gameObject.SetActive(true);
                GangrimSkillUi.instance.CurrentSkillUI("cloud");
            }
            else
            {
                CloudPos.gameObject.SetActive(false);
                StartCoroutine(CloudCor());
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
            GangrimSkillUi.instance.CurrentSkillUI("rain");
        }
    }

    public void DarknessSKill()
    {
        if (Input.GetMouseButtonDown(1) && Gauge.sGauge >= gauge.maxGauge && !DarkSkillUse)
        {
            DarkSkillUse = true;
            DarkSkill = 10f;
            gauge.dark_gauge_check = true;
            hitScript.damage = 10;
            gauge.CorStart();
        }

        if (!gauge.dark_gauge_check)
        {
            hitScript.damage = 1;
            DarkSkillUse = false;
        }
    }

    void Rainnig()
    {
        if (isSkillOn == true)
        {
            if (!isTest)
            {
                isSkillUse = false;
                FlyingObject.SetActive(true);
                FlyingObject.transform.position = RainPos;
                FlyingObject.transform.rotation = Quaternion.Euler(new Vector3(-90, 0, 0));
                StartCoroutine(RainActive());
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

    private IEnumerator RainActive()
    {
        yield return new WaitForSeconds(3f);
        FlyingObject.SetActive(false);
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

    IEnumerator CloudCor()
    {
        CloudSkillCheck = false;
        yield return new WaitForSeconds(3.0f);
    }
}