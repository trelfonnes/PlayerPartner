using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IKnockBackable
{
    public void KnockBack(Vector2 angle, float strenght, int directionX, int DirectionY);
}
