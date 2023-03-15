using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster_Move : MonoBehaviour
{
    RaycastHit hit;
    Rigidbody rigid;
    //수치 확인 용 public
    //case 2 좌표 2개로 이동
    public Transform[] points;

    public GameObject targetPosition; //playerposition
    public Transform targetTransform;

    private Transform MonsterMovePosition;
    public float MaxDistance; //공격 길이
    public float AttackSpeed; //공격 속력

    private bool StartAttack;
    private bool AttackSuccess;
    public bool TargetHere = false; //player collision
    public bool getposition = false;

    public int nextIdx = 1;

    public float movespeed = 4;  // Default speed;
    public float chasespeed = 0.05f;  // chase speed;
    public float movetime = 3.0f;
    public float speed = 3.0f;
    public float damping = 5.0f;
    //높이면 endposition자리에 남은 시간만큼 있다 다시 startpositon (높이면 자연스러움)

    void Start()
    {
        targetPosition = GameObject.FindWithTag("Main_gangrim");
        targetTransform = GameObject.FindWithTag("Main_gangrim").transform;

        rigid = GetComponent<Rigidbody>();
        MonsterMovePosition = GetComponent<Transform>();
        points = GameObject.Find("WayPointGroup").GetComponentsInChildren<Transform>();
    }

    void FixedUpdate()
    {
        monsterMove();
    }

    public void monsterMove()
    {

        if (TargetHere)//player를 추적
        {
            transform.LookAt(targetTransform);
            transform.position = Vector3.MoveTowards(gameObject.transform.position, targetPosition.transform.position, chasespeed);
        }
        else //플레이어와 멀어짐, 원래 자리로 돌아가서 다시 이동 반복
        {
            if (getposition != true) //monster move != true goto StartPosition
            {
                Quaternion rot = Quaternion.LookRotation(points[nextIdx].position - MonsterMovePosition.position);
                MonsterMovePosition.rotation = Quaternion.Slerp(MonsterMovePosition.rotation, rot, Time.deltaTime * damping);
                MonsterMovePosition.Translate(Vector3.forward * Time.deltaTime * speed);
            }
        }
    }

    IEnumerator Movepositon()
    {
        yield return new WaitForSeconds(movetime);
        getposition = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Main_gangrim")
        {
            Debug.Log("Collision to Player");
            TargetHere = true;
        }

        if (other.gameObject.tag == "Way_Point")
        {
            nextIdx = (++nextIdx >= points.Length) ? 1 : nextIdx;
            getposition = true;
            StartCoroutine("Movepositon");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Main_gangrim")
        {
            TargetHere = false;
        }
    }
}