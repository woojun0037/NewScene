using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HitScript : MonoBehaviour
{
    [SerializeField] private Main_Player player;

    public Main_Player Player { get => player; set => player = value; }

    public int damage;
    public bool isAttack;
}
