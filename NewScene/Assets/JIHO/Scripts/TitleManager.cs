using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleManager : MonoBehaviour
{
    public GameObject CreaditUI;
    bool CreaditOn;
    public GameObject[] ClickText;

    public void GameStart()
    {
        if(!CreaditOn)
        SceneManager.LoadScene("ChatScene");
    }


    public void ToggleUIPanel()
    {
        CreaditUI.SetActive(!CreaditUI.activeSelf);
        foreach (GameObject obj in ClickText)
        {
            obj.SetActive(!obj.activeSelf);
        }
    }
}
