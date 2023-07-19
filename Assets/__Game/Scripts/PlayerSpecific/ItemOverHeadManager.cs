using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemOverHeadManager : MonoBehaviour, ISpriteChange
{
    [SerializeField]
   List<Sprite> overHeadItems = new List<Sprite>();

    void DisplaySprite()
    {
        gameObject.SetActive(true);
    }


    void ChooseSprite(int spriteNumber)
    {
        for (int i = 0; i < overHeadItems.Count; i++)
        {

        }
    }
}
