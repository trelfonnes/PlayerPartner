using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossRoomTrigger : MonoBehaviour
{
    //[SerializeField] GameObject boss;
    [SerializeField] float roomLightIntensity = 1f;
    [SerializeField] EnemyStatEvents bossStatEvents;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            bossStatEvents.BattleStart();
            TimeOfDayManager.Instance.ChangeGlobalLightIntensity(roomLightIntensity);
        }
    }


}
