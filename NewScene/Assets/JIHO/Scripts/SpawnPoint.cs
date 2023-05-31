
using UnityEngine;
using UnityEngine.AI;

public class SpawnPoint : MonoBehaviour
{
    private Main_Player player;
    private NavMeshAgent nav;
    public bool isSpawn;

    private void Update()
    {
        if(!isSpawn)
        {
            player = FindObjectOfType<Main_Player>();
            CameraMovemant cam = FindObjectOfType<CameraMovemant>();
            cam.transform.position = this.transform.position;
            player.transform.position = this.transform.position;
            isSpawn = true;
            nav = player.transform.GetComponent<NavMeshAgent>(); 
            nav.enabled = true;
        }
    }
}
