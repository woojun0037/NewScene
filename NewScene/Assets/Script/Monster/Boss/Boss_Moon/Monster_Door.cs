using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster_Door : Boss
{

    public GameObject SpawnMonsters;

    public GameObject FloorAattackObj; //바닥 장판 스킬

    Renderer DoorColor;

    public enum BossPattern //보스 공격패턴
    {
        Pattern_0 = 0,
        Pattern_1,
        Pattern_2,
        Pattern_3
    }
    public BossPattern bosspattern;

    private float random;
    private float pastrandom;

    [SerializeField] bool onetime = true;
    [SerializeField] float cooltime; 
    float timer = 0;
    [SerializeField]  float MonsterYPosition;

    [SerializeField] Transform[] AttackPosition; //이동 자리(3자리 중 랜덤)
    public GameObject PatternDoonBullet; //문 분신
    public Rigidbody PatternBullet; //8각 총알
    public GameObject updownBullet; //위아래 총알

    float speed = 20;
    int pattern3_random;
    bool getready; //AttackPosition 이동 자리에 도착함
    bool isz;
    bool ispattern;
    bool bullettime;
    bool spawnone;

    //패턴 2 공격
    [SerializeField] float SetY;
    [SerializeField] float upspeed; //올라가는 속도
    [SerializeField] float downspeed; //내려가는 속도
    [SerializeField] float chasespeed4;
    bool isdown;
    bool Upready;
    bool bullettime2;
    bool Pattern2Start;
    bool stop;
    bool readytogetpodition;

    [SerializeField] float pattern3Max_Z;
    Vector3 ReturnPosition; //돌아가는 위치

    protected override void Start()
    {
        base.Start();
        onetime = false;
        targetTransform = GameObject.FindWithTag("Main_gangrim").transform;
        DoorColor = gameObject.GetComponent<Renderer>();

        ReturnPosition = transform.position; //기존 위치

        stop = false;
        readytogetpodition = false;
    }

    void Update()
    {
        if (player.HP >= 0 && !isDie)
        {
            NotDamaged();
            DieMonster();


            timer += Time.deltaTime;

            if (timer >= cooltime) //쿨 타임 후에 랜덤 스킬
            {

                timer = 0;
                onetime = false;
            }


            //몬스터 패턴 시작 + 쿨 타임 주기마다 재사용을 위한 초기화 역할
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

                StartCoroutine("ColorChangeBack"); //$ 1 2 패턴에 필요 //색깔 초기화
                onetime = true;
            }


            if (ispattern)//보스 옆으로 공격 패턴 Update에서 돌아야되는 조건 패턴 3
            {
                StartCoroutine(rightattack());
            }
            else
            {
                //초기화
                StopCoroutine(rightattack());
                pattern3_random = UnityEngine.Random.Range(0, 3); // 0 1 2 
                getready = false;
                isz = false;//
                spawnone = false;
            }

            //Update에서 돌아야되는 패턴 2 내려찍기
            if (Pattern2Start)
            {
                if (transform.position.y < SetY)
                {
                    if (!stop)
                    {
                        Upready = true;
                        transform.Translate(0, Time.deltaTime * upspeed, 0);
                    }
                }

                if (Upready) //플레이어 위치 찾음
                {
                    Vector3 spawn = targetTransform.position;
                    spawn.y = SetY;

                    if (!isdown)
                    {
                        transform.position = Vector3.MoveTowards(gameObject.transform.position, spawn, chasespeed4 * Time.deltaTime);
                    }

                    //n 초후 내려찍음
                    StartCoroutine("downattack");
                    
                }

                if (readytogetpodition)
                {
                    transform.position = Vector3.MoveTowards(gameObject.transform.position, ReturnPosition, chasespeed4 * Time.deltaTime);

                    if (transform.position == ReturnPosition)
                    {
                        Pattern2Start = false;
                    }
                }
            }
            else
            {
                StopCoroutine("downattack");
                targetTransform = GameObject.FindWithTag("Main_gangrim").transform;
                stop = false;//
                readytogetpodition = false;
                Upready = false;
                isdown = false;//
                               //bullettime = false;//혹시 버그나면 주석해제
            }

        }
    }

    void ColorChange(int i) //몬스터 색깔 변경
    {
        if(i == 1)
        {
            if (DoorColor.material.color != Color.magenta)
                DoorColor.material.color = Color.magenta;
            else
                DoorColor.material.color = Color.white;
        }
        else if(i ==2)
        {
            if (DoorColor.material.color != Color.black)
                DoorColor.material.color = Color.black;
            else
                DoorColor.material.color = Color.white;
        }
        else
        {
            DoorColor.material.color = Color.white;
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
                ColorChange(1);
                monsterspawn();

                break;
            case BossPattern.Pattern_1: //바닥 장판 생성
                ColorChange(1);
                FloorAttack();

                break;
            case BossPattern.Pattern_2: //내려찍기
                Pattern2Start = true;

                break;

            case BossPattern.Pattern_3: //옆으로 밀기
                ispattern = true;

                break;

            default:
                Debug.Log("default.. is");
                break;

        }

    }

    void monsterspawn()
    {
        float spawnrandom;
        int i = 0;

        spawnrandom = Random.Range(1, 4); //1 2 3

        while(i < spawnrandom)
        {
            StartCoroutine(spawntime(i));
            i++;
        }
    }

    void FloorAttack() //장판 공격
    {
        FloorAattackObj.SetActive(true);
    }


    //코루틴
    IEnumerator downattack() //패턴 2 내려 찍기 패턴
    {
        yield return new WaitForSeconds(3f);
        isdown = true;

        if (transform.position.y >= MonsterYPosition)
        {
            if (!stop)
            {
                transform.Translate(0, -Time.deltaTime * downspeed, 0);
            }

        }
        else
        {
            stop = true;

            if (!bullettime2 && !readytogetpodition)
            {
                bullettime2 = true;
                StartCoroutine(BulletTime_()); //총알 위 아래 발사 코루틴
            }

            yield return new WaitForSeconds(3f);
            readytogetpodition = true;

        }
    }

    IEnumerator BulletTime_()
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

        bullettime2 = false;
    }

    IEnumerator rightattack() //패턴3
    {
        Vector3 trans_3 = AttackPosition[pattern3_random].position; //랜덤 넣고

        if (transform.position != trans_3 && !getready)
        {
            transform.position = Vector3.MoveTowards(gameObject.transform.position, trans_3, Time.deltaTime * speed);
            isz = false;

        }
        else
        {
            getready = true;
            yield return new WaitForSeconds(1f);

            if (transform.position.z <= pattern3Max_Z && !isz && getready) //x 축 좌표
            {
                transform.Translate(-1 * speed * Time.deltaTime, 0, 0); //돌아가 있는데 축 때문에 -1이동

                if (!bullettime)
                {
                    bullettime = true;
                    StartCoroutine(Pattern3BulletTime_()); //총알 위 아래 발사 코루틴
                }
            }
            else //원래 자리로 돌아감
            {
                Debug.Log("Back");
                StopCoroutine(Pattern3BulletTime_());
                isz = true;
                transform.position = Vector3.MoveTowards(gameObject.transform.position, ReturnPosition, Time.deltaTime * speed / 2 * 5);
                ispattern = false; //패턴 초기화
            }
        }

        if (getready)
        {
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
        }

    }

    IEnumerator Pattern3BulletTime_() //패턴 3 총알 위아래
    {
        //yield return new WaitForSeconds(0.5f);
        Vector3 sety = transform.position;
        sety.y = transform.position.y + 2;
        yield return new WaitForSeconds(0.1f);
        GameObject bullet = Instantiate(updownBullet, sety, transform.rotation);

        yield return new WaitForSeconds(0.5f);

        bullettime = false;
    }

    IEnumerator spawntime(int i)
    {
        yield return new WaitForSeconds(i);
        Vector3 spawn = transform.position;
        spawn.y = 0f;

        GameObject monsterspawns = Instantiate(SpawnMonsters, spawn, transform.rotation);
    }

    IEnumerator ColorChangeBack()
    {
        yield return new WaitForSeconds(cooltime / 1.5f);
        ColorChange(0); //초기화
    }

}
