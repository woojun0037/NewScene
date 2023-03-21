using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class PlayerSkill : MonoBehaviour
{
    public static PlayerSkill Instance;

    public TrailRenderer trailEffect;
    public Transform CloudPos;
    public GameObject Cloudprab;

    public Transform target;// 부채꼴에 포함되는지 판별할 타겟

    public float angleRange = 30f;
    public float radius = 3f;

    private Vector3 mousePos;
    private Color _blue = new Color(0f, 0f, 1f, 0.2f);
    private Color _red = new Color(1f, 0f, 0f, 0.2f);

    bool isCollision = false;
    bool CloudisDelay = true;

    void Update()
    {
        Skill();
    }
    void Skill()
    {
        if (Input.GetKey(KeyCode.L))
        {
            Vector3 interV = target.position - transform.position;

            // target과 나 사이의 거리가 radius 보다 작다면
            if (interV.magnitude <= radius)
            {
                // '타겟-나 벡터'와 '내 정면 벡터'를 내적
                float dot = Vector3.Dot(interV.normalized, transform.forward);

                // 두 벡터 모두 단위 벡터이므로 내적 결과에 cos의 역을 취해서 theta를 구함
                float theta = Mathf.Acos(dot);

                // angleRange와 비교하기 위해 degree로 변환
                float degree = Mathf.Rad2Deg * theta;

                // 시야각 판별
                if (degree <= angleRange / 2f)
                    isCollision = true;
                else
                    isCollision = false;
            }
            else
                isCollision = false;
        }

        
        mousePos = Input.mousePosition;
        if (Input.GetKeyDown(KeyCode.R))//구름 스킬
        {
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

        }
    }

    IEnumerator CloudTime()
    {
        GameObject intantCloud = Instantiate(Cloudprab, CloudPos.position, CloudPos.rotation);
        Rigidbody CloudRigid = intantCloud.GetComponent<Rigidbody>();
        CloudRigid.velocity = transform.forward * 50;

        yield return new WaitForSeconds(0.5f);
        CloudisDelay = true;
    }

    // 유니티 에디터에 부채꼴을 그려줄 메소드
    private void OnDrawGizmos()
    {
        Handles.color = isCollision ? _red : _blue;
        // DrawSolidArc(시작점, 노멀벡터(법선벡터), 그려줄 방향 벡터, 각도, 반지름)
        Handles.DrawSolidArc(transform.position, Vector3.up, transform.forward, angleRange / 2, radius);
        Handles.DrawSolidArc(transform.position, Vector3.up, transform.forward, -angleRange / 2, radius);
    }
}