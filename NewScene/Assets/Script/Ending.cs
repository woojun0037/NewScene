using System;
using TMPro;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[Serializable]
public class EndingData
{
    public string chat;
    public Sprite image;
}

public class Ending : MonoBehaviour
{
    public EndingData[] chatList;
    public int index;
    public bool isChating;
    public Image bgImage;
    public TextMeshProUGUI text;
    public GameObject spaceText_obj;


    private void Awake()
    {
        EndChat(chatList[0]);
    }

    private void EndChat(EndingData chat)
    {
        spaceText_obj.SetActive(false);
        isChating = true;
        bgImage.sprite = chat.image;
        text.text = "";
        text.DOKill();
        text.DOText(chat.chat, 0.5f).OnComplete(() => { isChating = false; spaceText_obj.SetActive(true); });
        index++;
    }

    private void NextScene()
    {
        SceneManager.LoadScene("EndingCreadit");
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            if(!isChating)
            {
                if (index <= chatList.Length - 1)
                {
                    EndChat(chatList[index]);
                }
                else NextScene();
            }
        }
    }

}
