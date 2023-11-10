using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public float currentPotion;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public void AddPotion(float PotionAdd)
    {
        while (0 < PotionAdd)
            currentPotion += Time.deltaTime;
    }
}
