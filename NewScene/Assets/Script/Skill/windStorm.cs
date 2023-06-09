using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindStorm : MonoBehaviour
{ 
    ParticleSystem _WindStorm;
    ParticleSystem.Particle[] particles;

    private void OnEnable()
    {
        StartCoroutine(SetActive());
    }

    private IEnumerator SetActive()
    {
        yield return new WaitForSeconds(1f);
        this.gameObject.SetActive(false);
    }

    void Start()
    {
        _WindStorm = GetComponent<ParticleSystem>();
        
        particles = new ParticleSystem.Particle[_WindStorm.main.maxParticles];
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Monster")
        {
            other.GetComponent<Enemy>().curHearth -= 3f;
        }
    }

    void Update()
    {
        int num = _WindStorm.GetParticles(particles);

        particles[0].velocity = new Vector3(0, 5, 3) * 10;
        particles[1].velocity = new Vector3(-1f, 5, 3) * 10;
        particles[2].velocity = new Vector3(1f, 5, 3) * 10;

        _WindStorm.SetParticles(particles, num);
    }
}
