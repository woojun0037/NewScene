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

    //�����Լ��� ���� �ʿ��� �κ��� ���� ���� ��ɾ �߰��ϱ⸸ �ϸ� �ڵ����� �ʿ��� ��ɾ���� ����
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
