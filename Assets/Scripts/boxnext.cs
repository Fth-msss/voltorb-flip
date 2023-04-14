using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class boxnext : MonoBehaviour
{
    //this script makes things go go rocket boots
    [SerializeField]
    GameObject[] scenes;
    [SerializeField]
    GameObject Currentscene;
    int number = 0;
    int maxnumber = 69;

    public void Nextscene() 
    {
        if (number-1 >= 0) { Destroy(Currentscene); number++; Currentscene = scenes[number]; }
       

    }

    public void Prevscene() 
    {
        if (number + 1 <= maxnumber)
        { Destroy(Currentscene); number--; Currentscene = scenes[number]; }
           

    }
}
