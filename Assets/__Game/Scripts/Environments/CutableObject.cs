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
        AudioManager.Instance.PlayAudioClip("Cut");

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
