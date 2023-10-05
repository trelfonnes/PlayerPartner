using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutableObject : MonoBehaviour, ICutable
{
    Animator anim;
    private void Start()
    {
        anim = GetComponent<Animator>();
    }
    public void Cut()
    {
        StartCutting();
        Debug.Log("Chop It Down!");
    }

    void StartCutting()
    {
        anim.SetBool("cut", true);
    }

    public void Destory()
    {
        gameObject.SetActive(false);
    }



}
