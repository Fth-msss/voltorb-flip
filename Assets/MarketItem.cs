using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MarketItem : MonoBehaviour
{
    bool state=true;//if false is purchased
    string reason = "";
    [SerializeField]
    string itemName;

    public bool State { get { return state; } set { state = value;OnStateChange(); } }
    public string Reason { get { return reason; } set { reason = value; OnReasonChange(); } }

    public string Name { get => itemName; set => itemName = value; }

    [SerializeField]
    TextMeshProUGUI description;

    [SerializeField]
    Button button;
    [SerializeField]
    TextMeshProUGUI statetext;
    [SerializeField]
    Image checkmark;

    public void OnReasonChange() 
    {
     
        statetext.text = reason;
    }

    public void OnStateChange()
    {
  
        if (state) { OpenButton();  }

        else { CloseButton(); }
    }

    void CloseButton() 
    {
        button.interactable = false;
        
    }

    void OpenButton()
    {
        button.interactable = true;
      
    }


}
