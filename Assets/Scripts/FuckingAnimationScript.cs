using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FuckingAnimationScript : MonoBehaviour
{
    [SerializeField]
    Sprite image1;

    [SerializeField]
    Sprite image2;
    [SerializeField]
    float timer = 0;
    [SerializeField]
    float length = 1;
    // Start is called before the first frame update
    [SerializeField]
    Image help;

   

    bool x;
    void Swapimage() 
    {
        if (x) { help.overrideSprite = image1; }
        else   { help.overrideSprite = image2; }
    }


    
    // Update is called once per frame
    void Update()
    {
        if (Time.time > timer)
        {
            timer = Time.time + length;
            Swapimage();

        }
    }
}
