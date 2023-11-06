using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemyMove 
{
   public void StartMovement(float velocity, EnemyMovement movement);
}
