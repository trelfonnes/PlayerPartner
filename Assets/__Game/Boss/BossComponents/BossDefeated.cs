using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDefeated : BossCoreComponent
{
    [SerializeField] GameObject rewardForDefeat;
    [SerializeField] Animator bossDefeatedAnim;

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
        GameObject rewardInstance = Instantiate(rewardForDefeat, transform.position, Quaternion.identity);
    }

}
