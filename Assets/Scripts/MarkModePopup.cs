using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarkModePopup : MonoBehaviour
{
    [SerializeField]
    Animator anim;


    private void Awake()
    {
        anim.Play("marknotifonhold");
    }
    public void Openup()
    {
        anim.Play("marknotifanim");
    }
    public void Closedown()
    {
        anim.Play("marknotifanimclose");
    }

}
