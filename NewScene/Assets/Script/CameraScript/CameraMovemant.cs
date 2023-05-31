using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovemant : MonoBehaviour
{
    //public static CameraMovemant instance;

    public Transform objectTofollow;
    public float followSpeed;
    public float sensitivity;
    public float clampAngle;

    private float rotX;
    private float rotY;

    public Transform realCamera;
    public Vector3 dirNomarized;
    public Vector3 finalDir;

    public float minDistance;
    public float maxDistance;
    public float finalDistance;
    public float smoothness;

    //private void Awake()
    //{
    //    if(instance == null)
    //    {
    //        instance = this;
    //        DontDestroyOnLoad(this.gameObject);
    //    }
    //    else
    //    {
    //        Destroy(this.gameObject);
    //    }
    //}

    void Start()
    {
        rotX = transform.localRotation.eulerAngles.x;
        rotY = transform.localRotation.eulerAngles.y;

        dirNomarized = realCamera.localPosition.normalized;
        finalDistance = realCamera.localPosition.magnitude;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        rotX += -(Input.GetAxis("Mouse Y")) * sensitivity * Time.deltaTime;
        rotY += Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;

        rotX = Mathf.Clamp(rotX, -clampAngle, clampAngle);
        Quaternion rot = Quaternion.Euler(rotX, rotY, 0);
        transform.rotation = rot;
    }

    void LateUpdate()
    {
        transform.position = Vector3.MoveTowards(transform.position, objectTofollow.position, followSpeed * Time.deltaTime);

        finalDir = transform.TransformPoint(dirNomarized * maxDistance);

        RaycastHit hit;

        if (Physics.Linecast(transform.position, finalDir, out hit))
        {
            finalDistance = Mathf.Clamp(hit.distance, minDistance, maxDistance);
        }
        else
        {
            finalDistance = maxDistance;
        }
        realCamera.localPosition = Vector3.Lerp(realCamera.localPosition, dirNomarized * finalDistance, Time.deltaTime * smoothness);
    }
}
