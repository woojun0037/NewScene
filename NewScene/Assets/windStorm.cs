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

        WindStorm.GetParticles(particles);
        particles[0].rotation3D = new Vector3(0, 45, 0);
        particles[1].rotation3D = new Vector3(0, 0, 0);
        particles[2].rotation3D = new Vector3(0, -45, 0);


        
    }

    void Update()
    {

        //particles[0].velocity += new Vector3()
        particles[1].velocity += Vector3.right * 3;
        particles[2].velocity += Vector3.right * 3;
        WindStorm.SetParticles(particles, 3);
    }
}
