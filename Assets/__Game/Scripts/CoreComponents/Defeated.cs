using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Defeated : CoreComponent
{
  
    public override void LogicUpdate()
    {
        base.LogicUpdate();
    }

    protected override void Awake()
    {
        base.Awake();
       
    }
    protected override void Start()
    {
        base.Start();
       // Stats.statEvents.onCurrentHealthZero += EntityDefeated;//called here to prevent being subscribed before reference in corehandler is made

    }
    public virtual void EntityDefeated() //child classes will override this function to implement specific behavior
                                     //for their death components. e.g. PlayerDefeated, partnerdefeated, enemydefeated
        {
       // foreach (var particle in defeatedParticles)
        {
           // Particles?.StartParticles(particle);
        }

        core.transform.parent.gameObject.SetActive(false);
        }

    private void OnEnable()
    {
    }
    private void OnDisable()
    {
        //Stats.statEvents.onCurrentHealthZero -= EntityDefeated;
    }

}
