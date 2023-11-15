using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster_Door : Boss
{
    public enum BossPattern //보스 공격패턴
    {
        Pattern_0 = 0,
        Pattern_1,
        Pattern_2,
        Pattern_3
    }
    public BossPattern bosspattern;

    public GameObject SpawnMonsters;
    public GameObject SpawnMonstersRange;
    public GameObject FloorAattackObj; //바닥 장판 스킬
    public GameObject PatternDoonBullet; //문 분신
    public GameObject DownAttackRange;
    public GameObject updownBullet; //위아래 총알
    public EmissionIntensityController emissionController;
    [SerializeField] Transform[] AttackPosition; //이동 자리(3자리 중 랜덤)
    public Rigidbody PatternBullet; //8각 총알

    private float random;
    private float pastrandom;

    bool onetime;
    bool spawnone;
    bool BossStartDelay;

    Vector3 ReturnPosition; //돌아가는 위치

    protected override void Start()
    {
        emissionController = GetComponent<EmissionIntensityController>();

        base.Start();
        StartCoroutine(attackstart()); //몬스터 최초 대기 시간
        onetime = false;
        targetTransform = GameObject.FindWithTag("Main_gangrim").transform;
        ReturnPosition = transform.position; //기존 위치

        BossStart();

    }

    protected override void BossStart()
    {
        base.BossStart();
    }

    protected override void BossHit()
    {
        base.BossHit();
    }

    private IEnumerator DieRotation()
    {
        while (Vector3.Distance(transform.position, ReturnPosition) > 0.01f)
        {

            transform.position = Vector3.MoveTowards(transform.position, ReturnPosition, Time.deltaTime * 10f * 1.5f);

            yield return null; // 한 프레임 대기
        }

        yield return new WaitForSeconds(1f);


        Quaternion startRotation = transform.rotation;
        Quaternion targetRotation = Quaternion.Euler(90, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);

        float elapsedTime = 0f;
        while (elapsedTime < 0.8f)
        {
            transform.rotation = Quaternion.Slerp(startRotation, targetRotation, elapsedTime / 0.8f);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        yield return new WaitForSeconds(2f);

        gameObject.SetActive(false);

    }

    void Update()
    {
        if (BossStartDelay && isBossStart && player.HP >= 0 && !isDie)
        {
            if (curHearth < 1)
            {
                StopAllCoroutines();
                FloorAattackObj.SetActive(false);

                onetime = true;
                isDie = true;
                StartCoroutine(DieRotation());

            }

            NotDamaged();
            //DieMonster();

            if (!onetime)
            {
                random = UnityEngine.Random.Range(0, 4);

                if (random == pastrandom)
                {
                    if (random >= 3)
                    {
                        random += -1;
                        BossAttack(random);
                        pastrandom = random;
                    }
                    else // 0 1 2
                    {
                        random += 1;
                        BossAttack(random);
                        pastrandom = random;
                    }
                }
                else
                {
                    BossAttack(random);
                    pastrandom = random;
                }

                onetime = true;
            }

        }
    }

    void BossAttack(float random) //보스 랜덤 공격
    {
        if (random == 0)
            bosspattern = BossPattern.Pattern_0;
        else if (random == 1)
            bosspattern = BossPattern.Pattern_1;
        else if (random == 2)
            bosspattern = BossPattern.Pattern_2;
        else if (random == 3)
            bosspattern = BossPattern.Pattern_3;

        //0 몬스터 생성
        switch (bosspattern) //초기화
        {
            case BossPattern.Pattern_0: //몬스터 생성
                monsterspawn();

                break;
            case BossPattern.Pattern_1: //바닥 장판 생성
                FloorAttack();

                break;
            case BossPattern.Pattern_2: //내려찍기
                StartCoroutine(downattack());
                break;

            case BossPattern.Pattern_3: //옆으로 밀기
                StartCoroutine(rightattack());

                break;

            default:
                break;
        }

    }

    void monsterspawn()
    {
        StartCoroutine(spawntime());
    }

    IEnumerator spawntime()
    {
        emissionController.selectcolor(255, 0, 0);
        emissionController.EmssionMaxMin();

        yield return new WaitForSeconds(1f);

        onetime = true;
        float spawnrandom;
        int i = 0;

        spawnrandom = Random.Range(1, 4); //1 2 3

        Vector3 spawn = transform.position;
        spawn.x = transform.position.x + 2f;
        spawn.y = transform.position.y;
        GameObject monsterspawns = Instantiate(SpawnMonstersRange, spawn, transform.rotation);

        yield return new WaitForSeconds(1f);

        while (i < spawnrandom)
        {
            spawn.y = transform.position.y;
            spawn.x = transform.position.x + 2f;

            GameObject Spawnmonster = Instantiate(SpawnMonsters, spawn, transform.rotation);
            i++;
            yield return new WaitForSeconds(1f);
        }

        Destroy(monsterspawns);
        yield return new WaitForSeconds(4f);

        onetime = false;
    }

    void FloorAttack() //장판 공격
    {
        StartCoroutine(FloorBullet());
    }

    bool downrange = false;
    GameObject rangeObject;

    IEnumerator FloorBullet() {
        emissionController.selectcolor(5, 255, 0);
        emissionController.EmssionMaxMin();

        yield return new WaitForSeconds(1f);

        onetime = true;
        FloorAattackObj.SetActive(true);
        yield return new WaitForSeconds(8f);
        FloorAattackObj.SetActive(false); 
        onetime = false;
    }


    IEnumerator downattack() //패턴 2 내려 찍기 패턴
    {
        emissionController.selectcolor(0, 97, 191);
        emissionController.EmssionMaxMin();

        yield return new WaitForSeconds(1f);

        onetime = true;
  
        while(transform.position.y < 8) //최대 높이
        {
            transform.Translate(0, Time.deltaTime * 5f, 0); //upspeed
            yield return null;
        }

        yield return new WaitForSeconds(0.6f);

        float elapsedTime = 0f;
        rangeObject = Instantiate(DownAttackRange, targetTransform.position, Quaternion.identity);

        while (elapsedTime < 3f) //플레이어 쫒아오는 시간
        {
            Vector3 spawn = targetTransform.position;
            spawn.y = transform.position.y;

            Vector3 rangemoveDirection = targetTransform.position;
            rangemoveDirection.y = targetTransform.position.y;

            rangeObject.transform.position = rangemoveDirection;
            transform.position = Vector3.MoveTowards(transform.position, spawn, 10f * Time.deltaTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        Destroy(rangeObject);

        yield return new WaitForSeconds(0.7f);

        while (transform.position.y > ReturnPosition.y)
        {
            Vector3 rangemoveDirection = transform.position;
            rangemoveDirection.y = ReturnPosition.y;

            transform.position = Vector3.MoveTowards(transform.position, rangemoveDirection, 30f * Time.deltaTime); 
            yield return null;
        }

        yield return new WaitForSeconds(0.1f);

        for(int i =0; i < 4; i++)
        {
            StartCoroutine(downattackBullet()); //8방향 아래 발사 코루틴

            yield return new WaitForSeconds(0.5f);
        }

        yield return new WaitForSeconds(3f);


        while (Vector3.Distance(transform.position, ReturnPosition) > 0.01f) //return transpos
        {
            transform.position = Vector3.MoveTowards(transform.position, ReturnPosition, Time.deltaTime * 10f);

            yield return null;
        }

        yield return new WaitForSeconds(3f);
        onetime = false;
    }

    IEnumerator downattackBullet()
    {
        yield return new WaitForSeconds(0.5f);

        Vector3[] directions = new Vector3[]
        {
        new Vector3(0, 0, -1),
        new Vector3(-1, 0, -1),
        new Vector3(1, 0, -1),
        new Vector3(1, 0, 1),
        new Vector3(-1, 0, 1),
        new Vector3(-1, 0, 0),
        new Vector3(0, 0, 1),
        new Vector3(1, 0, 0)
        };

        for (int i = 0; i < directions.Length; i++)
        {
            Rigidbody bullet = Instantiate(PatternBullet, transform.position, Quaternion.LookRotation(directions[i] * 90));
            bullet.velocity = bullet.transform.forward * 10;
        }
    }



    IEnumerator rightattack() //패턴3 분신소환
    {
        emissionController.selectcolor(132, 0, 181);
        emissionController.EmssionMaxMin();

        yield return new WaitForSeconds(1f);

        onetime = true;

        int pattern3_random = UnityEngine.Random.Range(0, AttackPosition.Length);
        Vector3 targetPosition = AttackPosition[pattern3_random].position;
        float speed = 10f;

        while (Vector3.Distance(transform.position, targetPosition) > 0.01f)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, Time.deltaTime * speed * 1.5f);
            yield return null; // 한 프레임 대기
        }

        yield return new WaitForSeconds(2f);

        switch (pattern3_random)
        {
            case 0:
                if (!spawnone)
                {
                    spawnone = true;
                    GameObject bullet = Instantiate(PatternDoonBullet, AttackPosition[1].position, transform.rotation);
                    GameObject bullet2 = Instantiate(PatternDoonBullet, AttackPosition[2].position, transform.rotation);
                }
                break;
            case 1:
                if (!spawnone)
                {
                    spawnone = true;
                    GameObject bullet = Instantiate(PatternDoonBullet, AttackPosition[0].position, transform.rotation);
                    GameObject bullet2 = Instantiate(PatternDoonBullet, AttackPosition[2].position, transform.rotation);

                }
                break;
            case 2:
                if (!spawnone)
                {
                    spawnone = true;
                    GameObject bullet = Instantiate(PatternDoonBullet, AttackPosition[0].position, transform.rotation);
                    GameObject bullet2 = Instantiate(PatternDoonBullet, AttackPosition[1].position, transform.rotation);
                }
                break;
            default:
                break;
        }

        float bulletTime = 0;
        float elapsedTime = 0;

        while (elapsedTime < 4f)
        {
            transform.Translate(-1 * speed * Time.deltaTime, 0, 0); //돌아가 있는데 축 때문에 -1이동

            bulletTime += Time.deltaTime;
            elapsedTime += Time.deltaTime;

            if (bulletTime > 1f)
            {
                GameObject bullet = Instantiate(updownBullet, transform.position, transform.rotation);
                bulletTime = 0;
            }

            yield return null;
        }

        yield return new WaitForSeconds(1f);

        while (Vector3.Distance(transform.position, ReturnPosition) > 0.01f)
        {

            transform.position = Vector3.MoveTowards(transform.position, ReturnPosition, Time.deltaTime * speed * 1.5f);

            yield return null; // 한 프레임 대기
        }

        yield return new WaitForSeconds(2f);
        spawnone = false;
        onetime = false;
    }


    private IEnumerator attackstart()
    {
        yield return new WaitForSeconds(3f);
        BossStartDelay = true;
    }

}