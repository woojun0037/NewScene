using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountEnemy : MonoBehaviour
{
    public QuestScript questmonster;
    // Start is called before the first frame update
    void Start()
    {
        questmonster = GetComponent<QuestScript>();
    }

    // Update is called once per frame
    void Update()
    {
        questmonster.CountUp();
    }
}
