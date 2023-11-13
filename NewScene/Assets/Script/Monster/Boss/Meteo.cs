using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteo : MonoBehaviour
{
    public float GetDamaged;

    private void Start()
    {
        GetDamaged = 10.0f; 
    }

    public class ParticleCollisionHandler : MonoBehaviour
    {
        private Meteo parentMeteo;

        private void Start()
        {
            parentMeteo = GetComponentInParent<Meteo>();
        }

        void OnParticleCollision(GameObject other)
        {
            Debug.Log("meteooooooooooo");
            //if (other.CompareTag("Main_gangrim"))
            //{
            //    Debug.Log("meteooooooooooo");
            //    FindObjectOfType<HealthManager>().HurtPlayer(parentMeteo.GetDamaged);
            //}
            //Debug.Log("mon");

            if (other.gameObject.tag == "Main_gangrim")
            {
                Debug.Log("meteooooooooooo");
                FindObjectOfType<HealthManager>().HurtPlayer(3);
            }
        }

        private void OnParticleTrigger(GameObject other)
        {
            if (other.CompareTag("Main_gangrim"))
            {
                Debug.Log("meteooooooooooo");
                FindObjectOfType<HealthManager>().HurtPlayer(parentMeteo.GetDamaged);
            }

            Debug.Log("mon");
        }


    }
}