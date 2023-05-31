
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    private Main_Player player;
    public bool isSpawn;

    private void Update()
    {
        if(!isSpawn)
        {
            player = FindObjectOfType<Main_Player>();
            player.transform.position = this.transform.position;
            isSpawn = true;
        }
    }
}
