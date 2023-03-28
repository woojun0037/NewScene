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

    void Update()
    {
        int num = WindStorm.GetParticles(particles);

        particles[0].velocity = new Vector3(0, 0, 1f) * 3;
        particles[1].velocity = new Vector3(-0.3f, 0, 1) * 3;
        particles[2].velocity = new Vector3(0.3f, 0, 1) * 3;
        WindStorm.SetParticles(particles, num);
    }
}
