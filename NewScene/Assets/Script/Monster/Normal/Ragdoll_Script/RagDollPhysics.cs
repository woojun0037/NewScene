using UnityEngine;

public class RagDollPhysics : MonoBehaviour
{
    [SerializeField]
    Rigidbody spineRigidBody;
    Vector3 direction;
    GameObject Player;

    private void Start()
    {
        //Player = FindObjectOfType<GameObject>();
        Player = GameObject.FindWithTag("Main_gangrim");
        Vector3 dir = transform.position - Player.transform.position;


        spineRigidBody.AddForce(dir.normalized * 10000f);

    }

}