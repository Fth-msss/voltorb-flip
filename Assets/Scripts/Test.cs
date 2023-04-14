using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    //these variables were exposed to menu for debugging 
    [SerializeField]
    GameObject[] allPlayNodes;
    [SerializeField]
    GameObject[] playNodes;//list of node prefabs
    [SerializeField]
    GameObject[] infoNodesHorizontal;
    [SerializeField]
    GameObject[] infoNodesVertical;
    [SerializeField]
    GameObject infoNodeSpecial;
    int[] NodeValues;//inside values

    [SerializeField]
    PlayerControls player;
    int currentcoins;

   

    [SerializeField]
    GameObject nodespark;
    [SerializeField]
    GameObject nodePanel;
    [SerializeField]
    UIController uicontroller;
    bool firstturnshieldactive = false;
    bool markingmode=false;
    bool canclicknode=true;
    int turn;

    public bool Canclicknode { get => canclicknode; set => canclicknode = value; }
    public bool Markingmode { get => markingmode; set => markingmode = value; }



    //market bools
    [SerializeField]
    Market market;
    public bool Specialinfoactive { get {return specialinfoactive; }  set { specialinfoactive = value;  }  }
    public bool Firstturnshieldactive { get { return firstturnshieldactive; } set { firstturnshieldactive = value; }  }

    public int Turn { get => turn; set => turn = value; }

    public void OnChangingMarkMode() 
    {
        //do an anim and sound to feedback mark mode is on/off
        Markingmode = !Markingmode;
        if (Markingmode) { }
        else { }
    }

    //game board setup values
    int length = 5;
    int width = 5;
    int points = 10;
    int bombs = 6;
    int difficulty = 0;
    bool specialinfoactive = false;
  

    //get width and legth for setting up
    void GetSetupInfo(int length, int width, int points, int bombs)
    {
        this.length = length;
        this.width = width;
        this.points = points;
        this.bombs = bombs;

    }

    //set bombs and points
    //these are hard caps for difficulty
    int maxbombs = 12;
    int maxcoins = 20;

    void SetDifficulty()
    {

        bombs = 5 + difficulty;
        if (bombs > maxbombs) { bombs = maxbombs; }


        points = 10 + difficulty;
        if (points > maxcoins) { points = maxcoins; }

    }

    void GetArraySizes()
    {
        //get gameobjects of all nodes
        allPlayNodes = new GameObject[nodePanel.transform.childCount];
        int i = 0;
        foreach (Transform child in nodePanel.transform)
        {
            allPlayNodes[i] = child.gameObject;
            allPlayNodes[i].GetComponent<Node>().NodeId = i;
            allPlayNodes[i].GetComponent<Node>().VoltorbFlip = gameObject;
           i++;
        }
    }

    void SetPlayField(int length, int width)
    {

        //set arrays sizes
        playNodes = new GameObject[length * width];
        infoNodesVertical = new GameObject[length];
        infoNodesHorizontal = new GameObject[width];

        int i;
        int x = 0;
        int y = 0;
        int xfactor = length;

        //this gets all play nodes and vertical info nodes
        for (i = 0; i <(( length + 1) *( width + 1))-width-1; i++)
        {
           
            if (x < xfactor) { playNodes[x] = allPlayNodes[i]; x++; /*Debug.Log("playNodes:" + playNodes[x-1]);*/ }
            else { infoNodesVertical[y] = allPlayNodes[i]; xfactor += length; y++; /*Debug.Log("infoNodesVertical:" + infoNodesVertical[y-1]);*/ }

        }

        x = 0;
        for (i = allPlayNodes.Length - width-1; i < allPlayNodes.Length-1; i++)
        {
            infoNodesHorizontal[x] = allPlayNodes[i]; x++;
        }

        infoNodeSpecial = allPlayNodes[allPlayNodes.Length-1];
    }

    int goldgoal=1;
    void SetPlayValues(int points, int bombcount)
    {
        

        int remainingpoints = points;
        int gamesizex = 5;
        int gamesizey = 5;
        NodeValues = new int[gamesizex * gamesizey];

        for (int i = 0; i < gamesizex * gamesizey; i++)
        {
            if (i <= bombcount)
            {
                NodeValues[i] = -1;
            }
            else { NodeValues[i] = 1; }


        }

        for (int i = 0; i < NodeValues.Length; i++)
        {
            //Debug.Log("remaining points:"+remainingpoints);
            Debug.Log("points sent to distribute:"+points);
          
            if (remainingpoints >= 3 && NodeValues[i] == 1) { int g = Random.Range(2, 4); NodeValues[i] = g; remainingpoints -= g;/* Debug.Log("got3. node value:"+NodeValues[i]);*/ }
            else if (remainingpoints >= 2 && NodeValues[i] == 1) { NodeValues[i] = 2; remainingpoints -= 2; Debug.Log("got2. node value:" + NodeValues[i]); }


        }

        if (remainingpoints > 0) { Debug.LogWarning("points are not fully distributed"); }


        int tempint;
        //shuffle
        for (int i = 0; i < NodeValues.Length; i++)
        {
        int rnd = Random.Range(0, NodeValues.Length);
        tempint = NodeValues[rnd];
        NodeValues[rnd] = NodeValues[i];
        NodeValues[i] = tempint;

        }

        //multiply all values besides 0s to get max coin

        for (int i = 0; i < NodeValues.Length; i++)
        {

            if (NodeValues[i] != 0) 
            {
               
   
                goldgoal = goldgoal * NodeValues[i];
                if (goldgoal < 0) { goldgoal = Mathf.Abs(goldgoal); }//i legit have no idea why goldgoal sometimes turns minus(found it bombs are -1)
                Debug.Log("goldgoal:" + goldgoal+" goldgoals multiplier:"+NodeValues[i]);
            }
            
           

        }

        
        //put values in nodes



        for (int i = 0; i < playNodes.Length; i++)
        {
          // Debug.Log(i+"insert values in nodes. node:"+playNodes[i]+" inserted value:?");
            if (NodeValues[i].Equals(-1)) { SetPlayNode(playNodes[i].GetComponent<Node>(), 0, true); }
            else { SetPlayNode(playNodes[i].GetComponent<Node>(), NodeValues[i], false); }



        }

    }

    void SetInfoHorizontal(Node infonode,int width) 
    {
        int i;
        int linescore = 0;
        int bombcount = 0;
       // Debug.Log("horizontal info calculator values. node:"+ infonode + "length:"+ width);

        for (i = infonode.NodeId -= width+1; i > -1; i -= width+1)
        {
           // Debug.Log("node:"+ allPlayNodes[i]+"width:"+width+"info node:"+ infonode);
            if (allPlayNodes[i].GetComponent<Node>().Hasbomb) { bombcount++; }
            else { linescore += allPlayNodes[i].GetComponent<Node>().Score; }
        }

      

        SetInfoNode(infonode, linescore, bombcount);
    }

    void SetInfoVertical(Node infonode, int length)
    {
        int i;
        int linescore = 0;
        int bombcount = 0;
        //Debug.Log("vertical info calculator values. node:" + infonode + "length:" + length);



          for (i= infonode.NodeId-1;i > infonode.NodeId - (1 + length) ;i--) 
        {
           
            if (allPlayNodes[i].GetComponent<Node>().Hasbomb) { bombcount++; }
            else { linescore += allPlayNodes[i].GetComponent<Node>().GetComponent<Node>().Score; }

        }

        SetInfoNode(infonode, linescore, bombcount);
    }

    void SetInfoSpecial(Node infonode,int width) 
    {
        int i;
        int linescore = 0;
        int bombcount = 0;



        for (i = infonode.NodeId -= width+2; i > -1; i -= width+2)
        {
           // Debug.Log("node:" + allPlayNodes[i] + "width:" + width + "info node:" + infonode);
            if (allPlayNodes[i].GetComponent<Node>().Hasbomb) { bombcount++; }
            else { linescore += allPlayNodes[i].GetComponent<Node>().Score; }
        }


        SetInfoNode(infonode, linescore, bombcount);
        LockInfoNode(infonode);
    }
  
    void SetInfo() 
    {
        //set info nodes in horizontal nodes by getting width and looping with i-- by width
        //set info nodes in vertical nodes by getting length and looping with i-width
        //set special node by starting from 0 and until it gets to w*l. increase by width+1

        int i;
        

        //for (i = 0; i < length; i++)
        //{


        //    if (playNodes[i].GetComponent<Node>().Hasbomb) { bombcount++; }
        //    else { linescore += playNodes[i].GetComponent<Node>().Score; }

        //    if (i == 0) { i += 1; length += length + 1; }
        //}


        for (i = 0; i < infoNodesHorizontal.Length; i++)
        {
            SetInfoHorizontal(infoNodesHorizontal[i].GetComponent<Node>(),width);

           // SetInfoNode(infoNodesHorizontal[i].GetComponent<Node>(), linescore, bombcount);
        }


        for (i = 0; i < infoNodesVertical.Length; i++)
        {
            SetInfoVertical(infoNodesVertical[i].GetComponent<Node>(), length);

            //SetInfoNode(infoNodesVertical[i].GetComponent<Node>(), linescore, bombcount);
        }
        
        SetInfoSpecial(infoNodeSpecial.GetComponent<Node>(), width);  
        
    }

    void CreateGameSession() 
    {
        GetSetupInfo(5, 5, 10, 6);
        GetArraySizes();
        SetPlayField(length, width);
        SetDifficulty();
        SetPlayValues(points, bombs);
        SetInfo();
        UpdateDifficulty();
    }

    [SerializeField]
    LevelLoader levelloader;

    private void Awake()
    {
        //this is the worst way to make a level loader
        levelloader.EndTransition(1);
        CreateGameSession();

    }


    //settings for node gameobjects
    void SetInfoNode(Node node,int pointcount ,int bombcount) 
    {

        node.Bombline = bombcount;
        node.Scoreline = pointcount;
        node.Isinfo = true;
    }

    void LockInfoNode(Node node) 
    {
        node.LockNode();
    }

    void UnlockInfoNode(Node node) 
    {
        node.UnlockNode();
    }

    void SetPlayNode(Node playfield,int point,bool hasbomb) 
    {
        playfield.Score = point;
        playfield.Hasbomb = hasbomb;
        playfield.State = false; //closed
        playfield.Isinfo = false;
    }


   
    int lvldown=0;
    //play level lowers to lvldown if you get bombed
    public void PlayerCoins(int multiplier) 
    {
       //this gets the value from node and acts 
       //instant lose if zero without shield
       //for every node not zero,lvldown gets ++
       //if lost or cashed in difficulty drops down to lvldown

        if (multiplier == 0) { LostGame(); return; }
     
        if (currentcoins == 0) { currentcoins += multiplier;lvldown++; UpdateCurrentCoins(); }
        else { currentcoins *= multiplier; lvldown++; UpdateCurrentCoins(); }

        
        
        if (goldgoal <= currentcoins) { Wongame(); }

        if (Firstturnshieldactive) { Firstturnshieldactive = !Firstturnshieldactive; }
    }



   

    //these methods update ui values
    public void UpdatePlayerCoins(int pcoins) {  uicontroller.Playergoldtext.text = "all coins:<color=\"yellow\">" + pcoins.ToString(); }

    void UpdateCurrentCoins() { uicontroller.Currentgoldtext.text = "current coins:<color=\"yellow\">" + currentcoins.ToString(); }

    //void UpdateEndgamePopup(string text) { uicontroller.Gamewonorlost.text = text; }

    void UpdateDifficulty() { uicontroller.Difficulty.text = "difficulty: <color=\"yellow\">" + difficulty.ToString(); }

    void Openpopupup() { uicontroller.OpenPopupUp(); }

    void Closepopupup() { uicontroller.ClosePopupUp(); }

    void UpdatePopupText(string text) { uicontroller.ChangePopupText(text); }

    


    void Wongame() 
    {
        
        Debug.Log("game won by currentcoins: "+currentcoins+" requiredcoins: "+goldgoal);
        Canclicknode = false;
        player.AddGold(currentcoins);
       
        difficulty++;

        //UpdateEndgamePopup("game won");

        UpdatePopupText("game won! \n won  <color=\"yellow\">"+currentcoins+ "<color=\"white\"> coins! \n difficulty increased to <color=\"yellow\">" + difficulty);

        Openpopupup();
        StartCoroutine("WaitForAnim");
    }

    void LostGame() { 
        UpdateCurrentCoins();
        Canclicknode = false;

        if (difficulty < lvldown) { }
        else { difficulty = lvldown; }

        UpdatePopupText("game lost \n difficulty reduced to <color=\"yellow\">"+difficulty);
        Openpopupup();
        StartCoroutine("WaitForAnim");

    }

    bool keypress = false;
    private IEnumerator WaitForAnim()
    {
        
            yield return new WaitForSeconds(1);
        keypress = true;


    }


    private void Update()
    {
        if (keypress) 
        {
            if (Input.anyKey) { keypress = false; Restart(); }
        }
    }

    public void ActivateShield() 
    {
        firstturnshieldactive = true;
    }

    public void UnlockSpecialNode()
    {
        specialinfoactive = true;
            UnlockInfoNode(infoNodeSpecial.GetComponent<Node>());
        
        
    }

    public void Restart() 
    {

        
        Closepopupup();
        keypress = false;
        Specialinfoactive = false;
        Firstturnshieldactive = false;
         currentcoins = 0;
        goldgoal = 1;
        turn = 0;
        UpdateCurrentCoins();
      
        foreach (GameObject node in allPlayNodes) 
        {
            node.GetComponent<Node>().ResetNodeImage();
        }


        CreateGameSession();
        StartCoroutine("WaitForReset");




        Debug.Log("required coins to win:"+ goldgoal);
        Debug.Log("current coins:" + currentcoins);
        Debug.Log("difficulty:"+difficulty);
    }

    private IEnumerator WaitForReset()
    {

        yield return new WaitForSeconds(1);
        Canclicknode = true;


    }

}
