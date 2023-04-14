using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialPopupController : MonoBehaviour
{
    [SerializeField]
    GameObject[] pages;
    [SerializeField]
    GameObject activepage;
    int currentpage = 0;

    [SerializeField]
    GameObject bg;
    [SerializeField]
    GameObject bgedge;

    private void Awake()
    {
       
        activepage= pages[0];
    }

    public void NextPage() 
    {
        if (currentpage <=pages.Length) { currentpage++;activepage.SetActive(false);activepage = pages[currentpage]; activepage.SetActive(true); }
    }

    public void PrevPage() 
    {
        if (currentpage >0) { currentpage--;activepage.SetActive(false);activepage = pages[currentpage];activepage.SetActive(true); }
    }

    public void ClosePage() 
    {
        currentpage = 0;
        bg.SetActive(false);
        bgedge.SetActive(false);
        activepage.SetActive(false);
    }

    public void OpenPage()
    {
        bg.SetActive(true);
        bgedge.SetActive(true);
        activepage.SetActive(true);
    }

}
