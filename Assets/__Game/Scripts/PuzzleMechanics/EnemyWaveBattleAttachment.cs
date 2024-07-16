using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWaveBattleAttachment : MonoBehaviour
{
    [SerializeField] WaveBattleManager thisEnemyWaveManager;
    private void OnDisable()
    {
        thisEnemyWaveManager.OnEnemyDefeated();
    }
}
