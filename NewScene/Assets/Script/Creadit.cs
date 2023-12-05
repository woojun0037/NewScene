using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Creadit : MonoBehaviour
{
    public float EndingTime;

    // Update is called once per frame
    void Update()
    {
        EndingTime += Time.deltaTime;

        if(EndingTime > 20.0f)
        {
            NextScene();
        }
    }

    private void NextScene()
    {
        SceneManager.LoadScene("Title");
    }
}
