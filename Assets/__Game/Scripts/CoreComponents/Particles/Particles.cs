using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particles : CoreComponent
{
    [SerializeField]
    private StoredParticles storedParticles;

    public GameObject StartParticles(ParticleType particleType, Vector2 position, Quaternion rotation)
    {
        GameObject particlePrefab = storedParticles.GetParticlePrefab(particleType);

        if (particlePrefab != null)
        {
            GameObject particleObject = Instantiate(particlePrefab, position, rotation);
            Particle particleComponent = particleObject.GetComponent<Particle>();

            if (particleComponent != null)
            {
                particleComponent.SetPosition(position);
            }

            return particleObject;
        }

        return null;
    }
   
}
