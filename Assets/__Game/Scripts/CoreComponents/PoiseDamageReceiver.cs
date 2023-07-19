using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoiseDamageReceiver : CoreComponent, IPoiseDamageable
{
    [SerializeField] GameObject stunnedParticles;

    Poise poise;
   // Particles particles;  //might get a null reference exception with accessing core this way??

    public void DamagePoise(float amount)
    {
        poise.DecreasePoise(amount);
       // particles.StartParticlesWithRandomRotation(stunnedParticles); //TODO: might not be best way to set stun particles if any
    }
    protected override void Awake()
    {
        base.Awake();
        poise = core.GetCoreComponent<Poise>();
      //  particles = core.GetCoreComponent<Particles>();

    }
}
