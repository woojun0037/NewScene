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
    public bool isShake = false;

    [Header("SkillCheck2")]
    public bool RainSkillCheck = false;
    public bool WindSkillCheck = false;
    public bool DarkSkillUse = false;
    public bool CloudSkillCheck = false;
    private bool isDash;

    [Header("GameObject")]
    public GameObject FlyingObject;
    public GameObject Tornado;
    public GameObject WindSkillPrefab;
    public GameObject DarkSkillEffect;
   
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
    public float WindSkillTime;
    public float WindSkillCool;
    public float TornadoSkillTime;
    public float TornadoSkillCool;


    public float maxAbilityDistance;
    public float DarkSkill;

    private void Start()
    {
        gauge = FindObjectOfType<Gauge>();
        player = GetComponent<Main_Player>();
        targetCircle.GetComponent<Image>().enabled = false;
        SkillRange.GetComponent<Image>().enabled = false;
        WindDirection.enabled = false;
        Instance = this;
    }

    void Update()
    {
        if(!player.isAttack)
        {
            WindSkill();
            TornadoSkill();
            DarknessSKill();
            RainSkill();
        }

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

        //if (isSkillUse)
        //{
        //    int layerMask = 1 << LayerMask.NameToLayer("Platform");

        //    if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
        //    {
        //        if (hit.collider.gameObject != this.gameObject)
        //        {
        //            posUp = new Vector3(hit.point.x, 2, hit.point.z);
        //            postion = hit.point;
        //        }
        //    }

        //    비 스킬 이미지 인풋
        //    var hitPosDir = (new Vector3(hit.point.x, 2, hit.point.z) - transform.position).normalized;
        //    float distance = Vector3.Distance(hit.point, transform.position);
        //    distance = Mathf.Min(distance, maxAbilityDistance);

        //    var newHitPos = transform.position + hitPosDir * distance;
        //    RainPos = posUp;
        //    ability2Canvas.transform.position = (newHitPos);
        //}
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

    public void WindSkillRange()
    {
        //if (WindSkillTime > 0) return;

        //if (Input.GetKeyDown(KeyCode.Q) && isSkillUse && !WindSkillCheck && WindSkillTime <= 0)
        //{
        //    WindDirection.enabled = true;
        //    WindSkillCheck = true;
            
        //    isSkillUse = false;
        //}
        //else if (Input.GetKeyDown(KeyCode.Q) && !isSkillUse)
        //{
        //    WindDirection.enabled = false;
        //    WindSkillCheck = false;
        //    isSkillUse = true;
        //}
        
        //if (Input.GetMouseButtonDown(0) && !isSkillUse && WindSkillCheck)
        //{
        //    GangrimSkillUi.instance.CurrentSkillUI("wind");
        //    player.isAttack = true;
        //    isSkillUse = true;
        //    player.Skill_Q();
        //    StartCoroutine(QskillCoolDown());
        //}
    }

    

    public void WindSkill()
    {
        if (Input.GetKeyDown(KeyCode.Q) && WindSkillTime <= 0)
        {
            GangrimSkillUi.instance.windDot.DORestart();
            GangrimSkillUi.instance.CurrentSkillUI("wind");
            player.isAttack = true;
            player.Anim.SetTrigger("WindSkill");
            StartCoroutine(QskillCoolDown());
        }
    }

    public void WindActive(int a = 0)
    {
        if (a == 0)
        {
            WindSkillPrefab.transform.position = this.transform.position;
            WindSkillPrefab.transform.forward = this.transform.forward;

            WindSkillPrefab.SetActive(true);
        }
        else
        {
            WindSkillPrefab.SetActive(false);
            player.isAttack = false;
        }
    }

    private IEnumerator QskillCoolDown()
    {
        WindSkillTime = WindSkillCool;
        while (WindSkillTime > 0)
        {
            WindSkillTime -= Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
    }

    public void TornadoSkill()
    {
        if(Input.GetKeyDown(KeyCode.E) && TornadoSkillTime <= 0)
        {
            GangrimSkillUi.instance.tornadoDot.DORestart();
            GangrimSkillUi.instance.CurrentSkillUI("tornado");
            Tornado.transform.position = this.transform.position;
            Tornado.SetActive(true);
            StartCoroutine(ESkillCoolDown());
        }
    }

    private IEnumerator ESkillCoolDown()
    {
        TornadoSkillTime = TornadoSkillCool;
        while (TornadoSkillTime > 0)
        {
            TornadoSkillTime -= Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
    }

    public void RainSkill()
    {
        if (Input.GetKeyDown(KeyCode.R) && RainSkillTime <= 0)
        {
            GangrimSkillUi.instance.rainDot.DORestart();
            GangrimSkillUi.instance.CurrentSkillUI("rain");
            FlyingObject.transform.rotation = Quaternion.Euler(new Vector3(-90, 0, 0));
            FlyingObject.transform.position = this.transform.position + this.transform.forward * 5;
            FlyingObject.SetActive(true);
            StartCoroutine(RainActive());
            StartCoroutine(RSkillCoolDown());
        }
    }

    private IEnumerator RainActive()
    {
        yield return new WaitForSeconds(3f);
        FlyingObject.SetActive(false);
    }

    private IEnumerator RSkillCoolDown()
    {
        RainSkillTime = RainSkillCool;
        while (RainSkillTime > 0)
        {
            RainSkillTime -= Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
    }

    public void DarknessSKill()
    {
        if (Input.GetMouseButtonDown(1) && Gauge.sGauge >= gauge.maxGauge && !DarkSkillUse)
        {
            DarkSkill = 10f;
            DarkSkillEffect.SetActive(true);
            DarkSkillUse = true;
            gauge.dark_gauge_check = true;
            hitScript.damage = 10;
            gauge.CorStart();
        }

        if (!gauge.dark_gauge_check)
        {
            hitScript.damage = 1;
            DarkSkillEffect.SetActive(false);
            DarkSkillUse = false;
        }
    }
}