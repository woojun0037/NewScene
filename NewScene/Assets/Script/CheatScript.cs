using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheatScript : MonoBehaviour
{
    public static CheatScript instance;
    [SerializeField]GameObject Gangrim;
    [SerializeField]public Main_Player player;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
        Gangrim = GameObject.Find("Main_gangrim2");
    }

    void Update()
    {
        CheatPad();
    }

    public void CheatPad()
    {
        if (Input.GetKey(KeyCode.Keypad0))
        {
            if(player.currentHp < player.HP)
            { 
                player.currentHp += 1f;
            }
        }
        if (Input.GetKeyDown(KeyCode.Keypad1))
        {
            Gangrim.gameObject.SetActive(true);
        }
    }
}
