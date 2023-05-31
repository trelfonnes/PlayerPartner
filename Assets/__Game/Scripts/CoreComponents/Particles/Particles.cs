using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particles : CoreComponent
{
    private Transform particleContainer;
    public override void LogicUpdate()
    {
        base.LogicUpdate();
    }

    protected override void Awake()
    {
        base.Awake();
        particleContainer = GameObject.FindGameObjectWithTag("ParticleContainer").transform;

    }

    //To Use: Store particle effect game objects in particle container list and pass
    //them in where needed with a reference. e.g. Particles?.StartParticles(damageParticles);
    //can create very specific spawn locations. Transform.position -1 unit etc.
    public GameObject StartParticles(GameObject particlePrefab, Vector2 position, Quaternion rotation)
    {
        return Instantiate(particlePrefab, position, rotation, particleContainer);
    }
    public GameObject StartParticles(GameObject particlePrefab)
    {
        return StartParticles(particlePrefab, transform.position,Quaternion.identity);
    }
    public GameObject StartParticlesWithRandomRotation(GameObject particlePrefab) //this one for getting damaged effects
    {
        var randomRotation = Quaternion.Euler(0f, 0f, Random.Range(0f, 360f));
        return StartParticles(particlePrefab, transform.position, randomRotation);
    }

    public GameObject StartParticlesAtLocation(GameObject particlePrefab, Vector2 pos)//this one for attack effects
    {
        return StartParticles(particlePrefab, pos, Quaternion.identity);
    }
}
