using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class Boss_Asura : Boss
{
    public GameObject Meteo_Obj;
    public GameObject Rock_Wall;
    public GameObject FireRing;
    private Animator anim;

    bool isattack;
    bool die;

    float currentrandom;
    public float randomAttack = 0;

    public GameObject RightHandPos;
    public GameObject FireLine;



    protected override void Awake()
    {
        anim = GetComponent<Animator>();
    }

    protected override void Start()
    {
        base.Start();

        StartCoroutine(attackstart());
        player = GameObject.FindWithTag("Main_gangrim").GetComponent<Main_Player>();
        BossStart();
    }

    protected override void BossHit()
    {
        base.BossHit();
    }

    protected override void BossStart()
    {
        base.BossStart();
    }

    // Update is called once per frame
    void Update()
    {

        if (player.HP >= 0 && !isattack && !die)
        {
            NotDamaged();

            //같은 공격 연속 방지
            randomAttack = Random.Range(0, 4);

            if (randomAttack == currentrandom)
            {
                if (randomAttack < 3)
                    randomAttack++;
                else
                    randomAttack--;
            }

            currentrandom = randomAttack;

            switch (currentrandom)
            {
                case 0:
                    Meteo();
                    break;
                case 1:
                    Attack_2();
                    break;
                case 2:
                    Punch();
                    break;
                case 3:
                    Slice();
                    break;
            }
        }
        DieMonster();

    }

    private void RotateTowardsTarget(Vector3 targetPosition)
    {
        Vector3 targetDirection = (targetPosition - transform.position).normalized;
        targetDirection.y = 0; // y 축 회전 무시

        Quaternion rotation = Quaternion.LookRotation(targetDirection);
        Quaternion startRotation = transform.rotation;

        float rotationSpeed = 2.0f;
        float t = 0;
        while (t < 1)
        {
            t += Time.deltaTime * rotationSpeed;
            transform.rotation = Quaternion.Slerp(startRotation, rotation, t);
        }
    }

    protected override void DieMonster()
    {
        if (curHearth < 1)
        {
            die = true;
            StopAllCoroutines();
            BossEffectManager.SetActive(true);
        }
    }

    void Meteo()
    {
        StartCoroutine(Meteo_Attack());
    }

    private IEnumerator Meteo_Attack()
    {
        isattack = true;
        anim.SetBool("isGrowl", true);

        yield return new WaitForSeconds(0.5f);
        anim.SetBool("isGrowl", false);

        Vector3 spawn = player.transform.position;
        spawn.y = transform.position.y + 10f;

        Vector3 targetDirection = (spawn - transform.position).normalized;
        targetDirection.y = 0; // y 축 회전 무시

        Quaternion rotation = Quaternion.LookRotation(targetDirection);
        Quaternion startRotation = transform.rotation;

        float rotationSpeed = 2.0f;
        float t = 0;
        while (t < 1)
        {
            t += Time.deltaTime * rotationSpeed;
            transform.rotation = Quaternion.Slerp(startRotation, rotation, t);
            yield return null;
        }

        Quaternion rotationpos = player.transform.rotation * Quaternion.Euler(-90, 0, 0);
        GameObject ThrowRockrigid = Instantiate(Meteo_Obj, spawn, rotationpos);

        yield return new WaitForSeconds(5f);
        ThrowRockrigid.SetActive(false);

        yield return new WaitForSeconds(1f);//cool
        isattack = false;

    }


    void Attack_2() //돌로 막고 불꽃링
    {
        StartCoroutine(Attack_22_Rock());
    }


    private IEnumerator Attack_22_Rock()
    {
        isattack = true;
        anim.SetBool("isGrowl", true);

        yield return new WaitForSeconds(0.5f);
        anim.SetBool("isGrowl", false);
        Vector3 spawn = player.transform.position;
        spawn.y = transform.position.y;
        transform.LookAt(spawn);
        GameObject Rock = Instantiate(Rock_Wall, spawn, transform.rotation);

        

        yield return new WaitForSeconds(10f);
        //Rock.SetActive(false);
        Destroy(Rock);
        yield return new WaitForSeconds(2f);
        isattack = false;
    }

    
    void Punch()
    {
        StartCoroutine(Punch_Attack());
    }

    private IEnumerator Punch_Attack()
    {
        isattack = true;
        yield return new WaitForSeconds(1f);

        Vector3 spawn = player.transform.position;
        spawn.y = transform.position.y + 10f;

        Vector3 targetDirection = (spawn - transform.position).normalized;
        targetDirection.y = 0; // y 축 회전 무시

        Quaternion rotation = Quaternion.LookRotation(targetDirection);
        Quaternion startRotation = transform.rotation;

        float rotationSpeed = 1.5f;
        float t = 0;
        while (t < 1) //플레이어 방향으로 바라봄
        {
            t += Time.deltaTime * rotationSpeed;
            transform.rotation = Quaternion.Slerp(startRotation, rotation, t);
            yield return null;
        }

        yield return new WaitForSeconds(1f); //

        anim.SetBool("isPunch", true);
        yield return new WaitForSeconds(0.1f);//cool
        anim.SetBool("isPunch", false);

        yield return new WaitForSeconds(4f);//cool
        isattack = false;

    }


    void Slice()
    {
        StartCoroutine(Slice_Attack());
    }

    private IEnumerator Slice_Attack()
    {
        isattack = true;
        yield return new WaitForSeconds(2f);

        Vector3 spawn = player.transform.position;
        spawn.y = transform.position.y + 10f;

        Vector3 targetDirection = (spawn - transform.position).normalized;
        targetDirection.y = 0; // y 축 회전 무시

        Quaternion rotation = Quaternion.LookRotation(targetDirection);
        Quaternion startRotation = transform.rotation;

        float rotationSpeed = 2.0f;
        float t = 0;
        while (t < 1)
        {
            t += Time.deltaTime * rotationSpeed;
            transform.rotation = Quaternion.Slerp(startRotation, rotation, t);
            yield return null;
        }

        yield return new WaitForSeconds(1f);

        anim.SetBool("isSlice", true);

        yield return new WaitForSeconds(3f);
        anim.SetBool("isSlice", false);
        Vector3 right = RightHandPos.transform.position;
        right.y = transform.position.y;

        Quaternion rotationpos = RightHandPos.transform.rotation * Quaternion.Euler(5, 90, 90);

        GameObject FireLine_1 = Instantiate(FireLine, right, rotationpos);

        yield return new WaitForSeconds(2f);

        yield return new WaitForSeconds(1f);//cool
        isattack = false;

    }

    private IEnumerator attackstart()
    {
        isattack = true;
        yield return new WaitForSeconds(3f);
        isattack = false;
    }
}
