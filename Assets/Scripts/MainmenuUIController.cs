using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainmenuUIController : MonoBehaviour
{
    [SerializeField]
    TutorialPopupController tutorialUI;
    [SerializeField]
    LevelLoader levelloader;


    //button methods down here

    public void StartGame() 
    {
        levelloader.LoadNextLevel();
    }

    public void OpenTutorialPopup() 
    {
        tutorialUI.OpenPage();
    
    }

   public void CloseTutorialPopup() 
    {
        tutorialUI.ClosePage();
    }
}
