using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

    public enum ParticleType
    {
        Defeated,
        Stunned,
        // Add more particle types as needed
    }

    [Serializable]
    public class ParticleMapping
    {
        public ParticleType particleType;
        public GameObject particlePrefab;
    }

    [CreateAssetMenu(fileName = "StoredParticles", menuName = "Particles/StoredParticles")]
    public class StoredParticles : ScriptableObject
    {
        [SerializeField]
        private ParticleMapping[] particleMappings;

        public GameObject GetParticlePrefab(ParticleType particleType)
        {
            foreach (var mapping in particleMappings)
            {
                if (mapping.particleType == particleType)
                {
                    return mapping.particlePrefab;
                }
            }

            Debug.LogError("Particle prefab not found for type: " + particleType);
            return null;
        }

    }
