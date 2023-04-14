using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{

  

    [SerializeField]
    TextMeshProUGUI playergoldtext;
    [SerializeField]
    TextMeshProUGUI currentgoldtext;

    [SerializeField]
    TextMeshProUGUI gamewonorlost;

    [SerializeField]
    TextMeshProUGUI difficulty;

    public TextMeshProUGUI Playergoldtext { get => playergoldtext; set => playergoldtext = value; }
    public TextMeshProUGUI Currentgoldtext { get => currentgoldtext; set => currentgoldtext = value; }
    public TextMeshProUGUI Gamewonorlost { get => gamewonorlost; set {  gamewonorlost = value;  } }

    public TextMeshProUGUI Difficulty { get => difficulty; set => difficulty = value; }

    //ui button methods go here 

    [SerializeField]
    GameObject settingsPopup;


    [SerializeField]
    GameObject popupup;
    public void OpenPopupUp() 
    {
        popupup.GetComponent<Poppinup>().Openup();

    }

    public void ClosePopupUp()
    {
        popupup.GetComponent<Poppinup>().Closedown();
    }

    public void ChangePopupText(string newtext) { popupup.GetComponent<Poppinup>().ChangeText(newtext); }


    GameObject returnpopup;

    public void ReturnButton() 
    {
        //check if game is ongoing(turned at least one node)
       

        //check if returnpopup is active and showing info
        //get your coins and restart from difficulty whatever 
        //


       GameObject.FindGameObjectWithTag("voltorbflip").GetComponent<Test>();
       SceneManager.LoadScene("titlemenu");
       //this little thing lets you return to title screen,cash in without clearing the level and maybe more


    }

    [SerializeField]
    GameObject market;
    public void MarketButton() 
    {
        //no

        market.GetComponent<Market>().UIMarketButton();

        //yees
    }

    void CloseAllMenus() 
    {
    
    }

   

    [SerializeField]
    Sprite edgemarkcolor;
    [SerializeField]
    Sprite topthingmarkcolor;
    [SerializeField]
    Sprite bottomthingmarkcolor;
    [SerializeField]
    Sprite marketmarkcolor;


    [SerializeField]
    Sprite edgecolor;
    [SerializeField]
    Sprite topthingcolor;
    [SerializeField]
    Sprite bottomthingcolor;
    [SerializeField]
    Sprite marketcolor;




    [SerializeField]
    GameObject topuithing;
    [SerializeField]
    GameObject bottomuithing;
    [SerializeField]
    GameObject edges;
    [SerializeField]
    GameObject markmodenotifier;

    [SerializeField]
    GameObject marketpanel;


    public void MarkButton() 
    {

        //to enable disable node mark mode
        //ah yes do a different approach on every single method call what a genius design 
        //this will be so easy to work on later 
        //haha there isnt gonna be a later im done 
        GameObject.FindGameObjectWithTag("voltorbflip").GetComponent<Test>().OnChangingMarkMode();

        if (GameObject.FindGameObjectWithTag("voltorbflip").GetComponent<Test>().Markingmode)
        {
            topuithing.GetComponent<Image>().sprite = topthingmarkcolor;
            bottomuithing.GetComponent<Image>().sprite = bottomthingmarkcolor;
            edges.GetComponent<Image>().sprite = edgemarkcolor;
            marketpanel.GetComponent<Image>().sprite = marketmarkcolor;
            markmodenotifier.GetComponent<MarkModePopup>().Openup();
        }
        else 
        {
            topuithing.GetComponent<Image>().sprite = topthingcolor;
            bottomuithing.GetComponent<Image>().sprite = bottomthingcolor;
            edges.GetComponent<Image>().sprite = edgecolor;
            marketpanel.GetComponent<Image>().sprite = marketcolor;
            markmodenotifier.GetComponent<MarkModePopup>().Closedown();

        }


    }

    public void SettingsButton() 
    {
        settingsPopup.GetComponent<SettingsPopupController>().UISettingsButton();
    //open settings popup for maingame
    }






}