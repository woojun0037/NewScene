using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMoveScript : MonoBehaviour
{
    [SerializeField] Vector3 MovePlayer;
    [SerializeField] Rigidbody Rigid;
    [SerializeField] Collider Weapon;
    [SerializeField] Animator Anim;

    [SerializeField] float RotateSpeed = 10f;
    [SerializeField] float MoveSpeed = 1f;

    private Vector3 Dir = Vector3.zero;
    private Vector3 player_Move_Input;
    private Vector3 heading;

    float dash;
    float Player_Rotate;

    bool isdash;
    bool isMove = false;
    bool isAttack = false;

    public bool DEVCheck = false;

    void Awake()
    { 
        Rigid = this.GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        AnimationBoolCheck();
        Move();
    }

    private void Move()
    {

        // 입력값을 Vector3에 저장
        player_Move_Input = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        player_Move_Input.Normalize();

        // 카메라의 Forward를 가져옴
        heading = Camera.main.transform.forward;
        heading.y = 0;
        heading.Normalize();

        heading = heading - player_Move_Input;

        if (player_Move_Input != Vector3.zero && !isAttack)
        {
            isMove = true;
            float angle = Mathf.Atan2(heading.z, heading.x) * Mathf.Rad2Deg * -2;

            transform.rotation = Quaternion.Lerp(transform.rotation,
                                 Quaternion.Euler(0, angle, 0), Time.deltaTime * RotateSpeed);

            transform.Translate(Vector3.forward * Time.deltaTime * MoveSpeed);
        }
        else
        {
            isMove = false;
        }
    }

    private void AnimationBoolCheck()
    {
        Anim.SetBool("IsMove", isMove);
        Anim.SetBool("IsAttack", isAttack);
    }
}
