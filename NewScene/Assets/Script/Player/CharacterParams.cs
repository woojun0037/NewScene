using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterParams : MonoBehaviour
{
    public int level { get; set; }
    public int maxHp { get; set; }
    public int curHp { get; set; }
    public int attackMin { get; set; }
    public int attackMax { get; set; }
    public int defense { get; set; }
    public bool isDead {get; set;}

    void Start()
    {
        InitParams();
    }

    //가상함수를 만들어서 필요한 부분을 따로 만들어서 명령어를 추가하기만 하면 자동으로 필요한 명령어들이 실행
    public virtual void InitParams()
    {

    }

    public int GetRandomAttack()
    {
        int randAttack = Random.Range(attackMin, attackMax + 1);
        return randAttack; 
    }

    public void SetEnemyAttack(int enemyAttackPower)
    {
        curHp -= enemyAttackPower;
        UpdateAfterReceiveAttack();
    }
    protected virtual void UpdateAfterReceiveAttack()
    {
        print(name + "sHP:" + curHp);
    }
}
