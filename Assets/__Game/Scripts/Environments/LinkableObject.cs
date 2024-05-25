using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinkableObject : MonoBehaviour, ILinkable
{
    //place this script on obstacles that block paths until level is reached. (linkCable upgraded)
    Animator anim;
    [SerializeField] float linkLevelRequired;
    public void Link(float linkLevel)
    {
        CheckLinkLevel(linkLevel);
    }

    void Start()
    {
        anim = GetComponent<Animator>();   
    }

   void CheckLinkLevel(float currentLevel)
    {
        if(currentLevel >= linkLevelRequired)
        {
            //TODO: Execute functionality.
        }
        else
        {
            Debug.Log("Link Level is not high enough");
        }
    }

}
