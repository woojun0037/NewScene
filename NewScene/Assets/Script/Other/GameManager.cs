using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public float currentPotion;

    void Start()
    {
        
    }

    
    void Update()
    {

    }

    public void AddPotion(float PotionAdd)
    {
        while (0 < PotionAdd)
            currentPotion += Time.deltaTime;
    }
}
