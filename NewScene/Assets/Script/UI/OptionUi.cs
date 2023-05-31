using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class OptionUi : MonoBehaviour
{
    private static OptionUi instance;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public GameObject OptionUI;
    public bool isUIActive;

    private void Update()
    {
        UiOn(); //OptionUi 켜짐 여부

        //Main Scene에서는 옵션 Ui 안켜지도록 설정함
        if (Input.GetKeyDown(KeyCode.Escape) && SceneManager.GetActiveScene().buildIndex != 0)
        {
            if (OptionUI.activeSelf)
                OptionUI.SetActive(false);
            else
                OptionUI.SetActive(true);
        }

    }

    void UiOn()
    {
        if (OptionUI.activeSelf)
        {
            PauseGame();
            isUIActive = true;
        }
        else
        {
            ResumeGame();
            isUIActive = false;
        }
    }

    private void PauseGame()
    {
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
    }

    private void ResumeGame()
    {
        Time.timeScale = 1f;
        if(SceneManager.GetActiveScene().buildIndex != 0)
            Cursor.lockState = CursorLockMode.Locked;
        else
            Cursor.lockState = CursorLockMode.None;
        
    }

    public void MainMenu_Button()
    {
        Cursor.lockState = CursorLockMode.None;
        SceneManager.LoadScene(0);
        OptionUI.SetActive(false);
    }

    public void GameExit_Button()
    {
        Application.Quit();
    }

    public void OptionUi_Exit()
    {
        OptionUI.SetActive(false);
    }
}
