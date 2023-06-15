using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIDisable : MonoBehaviour
{
    private Enemy enemy;
    public GameObject ui;

    private void Start()
    {
        enemy = GetComponent<Enemy>();
    }
    // Update is called once per frame
    void Update()
    {
        if (enemy.curHearth < 1)
        {
            ui.SetActive(false);
        }
    }
}
