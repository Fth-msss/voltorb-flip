using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
 

    //stats for nerds up there


    // can player press ui buttons
    bool otherbuttons = false;


    public bool Otherbuttons { get => otherbuttons; set => otherbuttons = value; }


    PlayerPrefs gold;

    
    Test voltorbFlip;

    private void Awake()
    {
        voltorbFlip=GetComponent<Test>();
        voltorbFlip.GetComponent<Test>().UpdatePlayerCoins(PlayerPrefs.GetInt("gold"));
    }

    //add gold to player
    public void AddGold(int value)
    {
        PlayerPrefs.SetInt("gold", PlayerPrefs.GetInt("gold") + value);

        voltorbFlip.GetComponent<Test>().UpdatePlayerCoins(PlayerPrefs.GetInt("gold"));
    }


  

}
