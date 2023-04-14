using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Market : MonoBehaviour
{
    //temps down here get em to playerprefs
    int money;
    [SerializeField]
    GameObject voltorbflip;
  

   //public void ParaHarca(int mon) { money -= mon; }

    public void BuyItems(int id) 
    {
        money = PlayerPrefs.GetInt("gold");
        //PlayerPrefs.SetInt("gold", PlayerPrefs.GetInt("gold") + value);

        switch (id)
    {
            case 0:
                if (money < 100) { Debug.Log("not enough money for shield"); }//olm büyüktür yapsana þunlarý elseye gerek kalmasýn
                else
                {
                   
                   
                    PlayerPrefs.SetInt("gold", PlayerPrefs.GetInt("gold") - 2);
                    UsedShield();
                    //SetItemButton(SearchItem("firstturnshield"), false, "purchased!");
                    PurchaseSuccess();
                }
            break;
            case 1:
                if (money < 2) { Debug.Log("not enough money for special node transaction"); }
                else 
                {
                 

                   
                    PlayerPrefs.SetInt("gold", PlayerPrefs.GetInt("gold") - 2);
                    UnlockSpecialNode();
                   // SetItemButton(SearchItem("specialinfo"), false, "purchased");
                    PurchaseSuccess();
                }
                break;
            case 2:
                if (money < 300) { }//do nothin
                else { }
                break;
            case 3:
                if (money < 400) { }//do nothin
                else { }
                break;
            case 4:
                if (money < 500) { }//do nothin
                else { }
                break;


        }

    
    }



    
    public void UsedShield() 
    {
        //only usable on first turn
        //saves you from a bomb 
        //disables after first turn
        voltorbflip.GetComponent<Test>().ActivateShield();
    }

    public void UnlockSpecialNode() 
    {
        //open special node up
        voltorbflip.GetComponent<Test>().UnlockSpecialNode();
    }


    //ui things under here
    [SerializeField]
    GameObject page;

    bool marketopen = false;
    public void UIMarketButton() 
    {
        if (marketopen) { CloseUIPage(); marketopen=!marketopen; }
        else { OpenUIPage(); marketopen = !marketopen; }
    }

    public void OpenUIPage() { CheckForEveryItem(); page.SetActive(true); }

    public void CloseUIPage() { page.SetActive(false); }


    //all the market item buttons do are to call these methods. they should not have any brain they should not even be active if they eeeeeeeeeee

   

    public void CheckForEveryItem() 
    {
        Test vflip = voltorbflip.GetComponent<Test>();
        //a giant list of ifs to check every time if an item is avaible to purchase

        //for first turn shield:

        //is powerup active
        if (!vflip.Firstturnshieldactive) 
        {
            //is it first turn
            if (vflip.Turn < 1)
            {
                Debug.Log("can buy shield!");
                SetItemButton(SearchItem("firstturnshield"),true);
               
            }
            else { SetItemButton(SearchItem("firstturnshield"), false, "avaible on first turn!"); Debug.Log("cant buy shield,not first turn"); }
        }
        else { SetItemButton(SearchItem("firstturnshield"), false); Debug.Log("shield already active"); }
      

        //for edge info node:
        if (!vflip.Specialinfoactive)
        {
            Debug.Log("can buy info");
            SetItemButton(SearchItem("specialinfo"), true);
        }
        else { SetItemButton(SearchItem("specialinfo"), false); Debug.Log("info already purchased"); }


    }



    [SerializeField]
    MarketItem[] items; 

    public MarketItem SearchItem(string name) 
    {
    foreach(MarketItem item in items) 
        {
            if (item.Name == name) { return item; }
        }
        Debug.Log("item not found");
        return null;
    }

    public void SetItemButton(MarketItem item,bool state) 
    {
        item.State = state;
    }

    public void SetItemButton(MarketItem item, string reason) 
    {
        item.Reason = reason;
    }

    public void SetItemButton(MarketItem item, bool state,string reason)
    {
        item.Reason = reason;
        item.State = state;
    }


    //if they active deactive em eh aaaaaaaaaaaaa
    //check powerups active when market menu is open
    //and when purchase success calls

 


    public void PurchaseSuccess()
    {
        Debug.Log("purchase successful");
        voltorbflip.GetComponent<Test>().UpdatePlayerCoins(PlayerPrefs.GetInt("gold"));
        CheckForEveryItem();

        //ui feedback
        
    }


 

}


