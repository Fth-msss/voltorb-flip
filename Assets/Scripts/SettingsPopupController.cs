using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsPopupController : MonoBehaviour
{

    [SerializeField]
    GameObject bg;
    [SerializeField]
    GameObject bgedge;
    [SerializeField]
    GameObject openpage;

    void ClosePage()
    {
       
        bg.SetActive(false);
        bgedge.SetActive(false);
        openpage.SetActive(false);

    }

    void OpenPage()
    {
        bg.SetActive(true);
        bgedge.SetActive(true);
        openpage.SetActive(true);
    }

    bool ispageopen = false;
    public void UISettingsButton() 
    {
        //this method gets called from uicontroller
        if (ispageopen) 
        {
            ClosePage();
            ispageopen = !ispageopen;
        }

        else 
        {
            OpenPage();
            ispageopen = !ispageopen;
        }
    }

    [SerializeField]
    Slider masterslider;
    [SerializeField]
    Slider musicslider;
    [SerializeField]
    Slider soundslider;

    AudioManager audiomanage;

    private void Awake()
    {
       
        audiomanage= GameObject.FindGameObjectsWithTag("music")[0].GetComponent<AudioManager>();
        


        masterslider.onValueChanged.AddListener(audiomanage.ChangeMasterVolume);
        musicslider.onValueChanged.AddListener(audiomanage.ChangeMusicVolume);
        soundslider.onValueChanged.AddListener(audiomanage.ChangeSoundVolume);

        masterslider.value = audiomanage.Mastervolume;
        musicslider.value = audiomanage.Musicvolume;
        soundslider.value = audiomanage.Soundvolume;

    }

  

}
