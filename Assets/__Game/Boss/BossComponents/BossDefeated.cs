using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class BossDefeated : BossCoreComponent
{
   // [SerializeField] GameObject rewardForDefeat;
    [SerializeField] Animator bossDefeatedAnim;
        [SerializeField] EnemyStatEvents statEvents;

    
    public void Defeated()
    {
        bossDefeatedAnim.SetBool("defeated", true);
       
    }

    public void CleanUp()
    {
        ParticleFX.StartParticles(ParticleType.Defeated, transform.position, Quaternion.identity);

        transform.parent.parent.gameObject.SetActive(false);
    }
    public void SpawnReward()
    {
     //   GameObject rewardInstance = Instantiate(rewardForDefeat, transform.position, Quaternion.identity);
    }

    public void ArenaEnemyDefeated()
    {
        statEvents.BossEnemyDefeated();
    }
}
