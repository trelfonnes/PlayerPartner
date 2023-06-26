using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeartDisplayUI : MonoBehaviour
{
    [SerializeField] List<Image> heartsFull = new List<Image>();
    [SerializeField] List<Image> heartsEmpty = new List<Image>();

    public void UpdateHeartDisplay(float currentHealth, float maxHealth)
    {
        int fullHeartsCount = Mathf.CeilToInt(currentHealth);
        int emptyHeartsCount = Mathf.CeilToInt(maxHealth);

        for (int i = 0; i < heartsFull.Count; i++)
        {
            if(i < fullHeartsCount)
            {
                heartsFull[i].gameObject.SetActive(true);
            }
            else
            {
                heartsFull[i].gameObject.SetActive(false);
            }

        }
        for (int i = 0; i < heartsEmpty.Count; i++)
        {
            if(i < emptyHeartsCount)
            {
                heartsEmpty[i].gameObject.SetActive(true);
            }
            else
            {
                heartsEmpty[i].gameObject.SetActive(false);
            }
        }

    }

}
