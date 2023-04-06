using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindStorm : MonoBehaviour
{

    ParticleSystem _WindStorm;
    ParticleSystem.Particle[] particles;

    void Start()
    {
        _WindStorm = GetComponent<ParticleSystem>();
        particles = new ParticleSystem.Particle[_WindStorm.main.maxParticles];
    }

    //public void WindStormAnim()
    //{
    //    WindStorm = GetComponent<ParticleSystem>();
    //    int num = WindStorm.GetParticles(particles);
    //}

    void Update()
    {
        int num = _WindStorm.GetParticles(particles);

        particles[0].velocity = new Vector3(0, 4, 3) * 10;
        particles[1].velocity = new Vector3(-2f, 4, 3) * 10;
        particles[2].velocity = new Vector3(2f, 4, 3) * 10;

        _WindStorm.SetParticles(particles, num);
    }
}
