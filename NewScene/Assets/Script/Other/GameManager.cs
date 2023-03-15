using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int currentPotion;

    void Start()
    {
        
    }

    
    void Update()
    {
        
    }

    public void AddPotion(int PotionAdd)
    {
        currentPotion += PotionAdd;
    }
}
