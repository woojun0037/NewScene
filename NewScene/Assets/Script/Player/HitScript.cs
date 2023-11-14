using UnityEngine;

public class HitScript : MonoBehaviour
{
    [SerializeField] private Main_Player player;


    public Main_Player Player 
    { 
      get => player; 
      set => player = value; 
    }

    public bool isAttack;
    public float damage = 1f;
    public float HitGauge = 1f;
}
