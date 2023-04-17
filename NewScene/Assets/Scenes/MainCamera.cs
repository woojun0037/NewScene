using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour
{
    [SerializeField] Transform FollowTarget;
    public Vector3 FollowPlayer;
    private bool isShake;
    void Start()
    {
        isShake = false;
        FollowPlayer = this.transform.position - FollowTarget.position;
    }

    void LateUpdate()
    {
        if(isShake == false)
            this.transform.position = FollowTarget.transform.position + FollowPlayer;
    }

    public IEnumerator Shake(float Duration, float magnitude)
    {
        isShake=true;
        Vector3 originalPos = transform.localPosition;

        float elapsed = 0.0f;

        while (elapsed < Duration)
        {
            
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;

            transform.localPosition = new Vector3(originalPos.x - x, originalPos.y - y, originalPos.z);

            elapsed += Time.deltaTime;

            yield return null; 
        }
        transform.localPosition = originalPos;
        isShake = false;
    }
}