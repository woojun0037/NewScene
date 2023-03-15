using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour
{
    [SerializeField] Transform FollowTarget;

    public Vector3 FollowPlayer;

    void Start()
    {
        FollowPlayer = this.transform.position - FollowTarget.position;
    }

    void LateUpdate()
    {
        this.transform.position = FollowTarget.transform.position + FollowPlayer;
    }
}