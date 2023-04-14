using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Node : MonoBehaviour
{
    GameObject voltorbFlip;

    int nodeId;

    [SerializeField]
    int score;

    [SerializeField]
    bool hasbomb;

    bool state=false;//is node turned over

    [SerializeField]
    bool isinfo;

    bool islocked=false;

    int scoreInfo;
    int bombInfo;

    [SerializeField]
    Animator anim;
  



    public int Score { get => score; set { score = value; OnScoreChange(); }  }
    public bool Hasbomb { get => hasbomb; set { hasbomb = value; HasBomb(); }  }
    public bool State { get => state; set { state = value; OnStateChange(); } }
    public bool Isinfo { get => isinfo; set { isinfo = value; if (isinfo) { SetInfoNode(); } else { SetGameNode(); } }  }
    public int Scoreline { get => scoreInfo; set { scoreInfo = value; } }
    public int Bombline { get => bombInfo; set { bombInfo = value; }  }
    public GameObject VoltorbFlip { get => voltorbFlip; set { voltorbFlip = value; } }
    public int NodeId { get => nodeId; set => nodeId = value; }
    public bool Islocked { get => islocked; set => islocked = value; }

    [SerializeField]
    Sprite infobg;
    [SerializeField]
    Sprite gamenodebg;

    [SerializeField]
    Image Bg;


    [SerializeField]
    GameObject pointCountObj;
    [SerializeField]
    GameObject bombCountObj;
    [SerializeField]
    GameObject nodespark;

    void OnScoreChange() 
    {
        DebugText();
    }

    void OnStateChange() 
    {
    
    }

    void HasBomb() 
    {
    
    }



    void SetInfoNode() 
    {
        Bg.sprite = infobg;
        bombCountObj.GetComponent<TextMeshProUGUI>().text = bombInfo.ToString();
        pointCountObj.GetComponent<TextMeshProUGUI>().text = scoreInfo.ToString();
        pointCountObj.SetActive(true);
        bombCountObj.SetActive(true);
        GetComponent<Button>().interactable = false;
        gameObject.SetActive(true);
    }

    void SetGameNode() 
    {
        Bg.sprite = gamenodebg;
        GetComponent<Button>().interactable = true;
        pointCountObj.SetActive(false);
        bombCountObj.SetActive(false);
        gameObject.SetActive(true);
       
    }
    [SerializeField]
    Sprite mark1;
    [SerializeField]
    Sprite mark2;
    [SerializeField]
    Sprite mark3;
    [SerializeField]
    Sprite bomb;
    [SerializeField]
    Image markimg;
    int mark = 0;


    public void ResetMarks() 
    {
        mark = 0;

        Color tempcolor = markimg.color;
        tempcolor.a = 0f;
        markimg.color = tempcolor;
    }

    public void MarkNode() 
    {
        mark++;
        Color tempcolor = markimg.color;
        Color tempcolornoalpha = tempcolor;
        tempcolor.a = 0f;
       
       
        if (mark >= 5) { mark = 0; Debug.Log("mark exceeds limits"); }//make mark invis why is this read only 
        if (mark == 0) { markimg.color = tempcolor; Debug.Log("mark is zero,no color"); }//no mark
        if (mark == 1) { markimg.color = Color.white; ; markimg.sprite = mark1; Debug.Log("mark is one,should be one"); }//1
        if (mark == 2) { markimg.sprite = mark2; Debug.Log("mark 2"); }//2
        if (mark == 3) { markimg.sprite = mark3; Debug.Log("mark 3"); }//3
        if (mark == 4) { markimg.sprite = bomb; Debug.Log("mark bomb"); }//bomb
    }

    public void ButtonClick() 
    {
        if (state) { Debug.Log("node unavaible"); }
        else 
        {
            if (voltorbFlip.GetComponent<Test>().Canclicknode)
            {
                if (!isinfo) 
                {

                    voltorbFlip.GetComponent<Test>().Turn++;
                    if (voltorbFlip.GetComponent<Test>().Markingmode) { MarkNode(); }
                    else { ResetMarks(); TurnNode(); }


                   
                
                }
            }
        }


       
    }

    void TurnNode() 
    {
        if (hasbomb) { VoltorbFlip.GetComponent<Test>().Canclicknode = false; }

   
        state = !state;
        anim.Play("nodeanim");

        voltorbFlip.GetComponent<Test>().PlayerCoins(score);
    }

    public void SpawnNodeSpark()
    {
        GameObject Spark= Instantiate(nodespark, transform.parent.parent);
        Spark.transform.position = gameObject.transform.position;
    }


    public void ResetNodeImage() 
    {
        ResetMarks();
        anim.Play("resetnode");
    }

    public GameObject debug;
    public void DebugText() 
    {
        debug.GetComponent<TextMeshProUGUI>().text = score.ToString();
    }

    [SerializeField]
    AudioClip coin1;
    [SerializeField]
    AudioClip coin2;
    [SerializeField]
    AudioClip coin3;
    [SerializeField]
    AudioClip bombsound;
    AudioManager audiomanage;
    

    public void IconTrigger() 
    {
        audiomanage =  GameObject.Find("Audiomanager").GetComponent<AudioManager>();
        //for some reason besides being a bitch enabling icon image does not work in animation so it will be here
        //this gets called by animator event

        if (hasbomb) { anim.Play("nodebomb");audiomanage.PlaySound(bombsound); }
        else if(score==1) { anim.Play("nodeone"); audiomanage.PlaySound(coin1); SpawnNodeSpark(); }
        else if (score == 2) { anim.Play("nodetwo"); audiomanage.PlaySound(coin2); SpawnNodeSpark(); }
        else if (score == 3) { anim.Play("nodethree"); audiomanage.PlaySound(coin3); SpawnNodeSpark(); }
        else if (score == 4) { anim.Play("nodefour"); audiomanage.PlaySound(coin3); SpawnNodeSpark(); }
    }

    //locks or unlocks info nodes
    public void LockNode() 
    {
        
            bombCountObj.SetActive(false);
            pointCountObj.SetActive(false);
            //add lock png here
            islocked = true;
        
    }


    public void UnlockNode()
    {
        bombCountObj.SetActive(true);
        pointCountObj.SetActive(true);
        //disable lock png here
        islocked = false;

    }
}
