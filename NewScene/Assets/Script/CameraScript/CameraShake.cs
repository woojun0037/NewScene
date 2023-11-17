using System.Collections;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public float shakeDuration = 0.5f;
    public float shakeIntensity = 0.2f;

    private Vector3 originalPosition;
    protected Main_Player player;

    private void Start()
    {
        player = GameObject.FindWithTag("Main_gangrim").GetComponent<Main_Player>();


    }
    public void Update()
    {

        //if (Input.GetKeyDown(KeyCode.A))
        //{
        //Shake();
        //}
    }
    public void Shake()
    {
        StartCoroutine(ShakeCoroutine());
    }

    private IEnumerator ShakeCoroutine()
    {
        originalPosition = transform.position;

        float elapsedTime = 0f;

        while (elapsedTime < shakeDuration)
        {
            Vector3 shakeOffset = Random.insideUnitSphere * shakeIntensity;

            transform.position = originalPosition + shakeOffset;

            elapsedTime += Time.deltaTime;

            yield return null;
        }


    }
}