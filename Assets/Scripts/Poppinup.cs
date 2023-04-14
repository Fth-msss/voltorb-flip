using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Poppinup : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI text;


    [SerializeField]
    Animator anim;


    public void Openup() 
    {
        anim.Play("popuptop");
    }
    public void Closedown()
    {
        anim.Play("popupdown");
    }


    public void ChangeText(string newtext) 
    {
        text.text = newtext;
    }
}
