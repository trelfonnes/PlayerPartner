using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBytes
{
    int GetBytesAmount();
    public void IncreaseBytes(int amount) { }

    public void DecreaseBytes(int amount) { }

    public void IncreaseMaxBytes(int amount) { }

}
