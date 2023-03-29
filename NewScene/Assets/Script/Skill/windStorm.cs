using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class windStorm : MonoBehaviour
{
    ParticleSystem WindStorm;
    ParticleSystem.Particle[] particles;
    
    void Start()
    { 
        WindStorm = GetComponent<ParticleSystem>();
        particles = new ParticleSystem.Particle[WindStorm.main.maxParticles];
    }

    //public void WindStormAnim()
    //{
    //    WindStorm = GetComponent<ParticleSystem>();
    //    int num = WindStorm.GetParticles(particles);


    //}

    void Update()
    {
        int num = WindStorm.GetParticles(particles);

        particles[0].velocity = new Vector3(0, 0, 1) * 5;
        particles[1].velocity = new Vector3(-0.3f, 0, 1) * 5;
        particles[2].velocity = new Vector3(0.3f, 0, 1) * 5;
        WindStorm.SetParticles(particles, num);
    }
}
