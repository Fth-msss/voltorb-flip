using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public Animator transition;
    static bool firstboot=false;

   


    public void LoadNextLevel() 
    {

        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1)); 
        
    }

    IEnumerator LoadLevel(int levelIndex) 
    {
        Debug.Log("here we fucking go ahgain");
        if (levelIndex == 0) { transition.SetTrigger("start1"); }
        if (levelIndex == 1) { transition.SetTrigger("start2"); }


        yield return new WaitForSeconds(1);

        SceneManager.LoadScene(levelIndex);
    }

    public void EndTransition(int sceneindex) 
    {


        Debug.LogWarning("firstboot:" + firstboot + "sceneindex:" + sceneindex);
        if (!firstboot)
        {

            Debug.LogWarning("trigger end anim");
            if (sceneindex == 0) { transition.SetTrigger("end1"); }
            if (sceneindex == 1) { transition.SetTrigger("end2"); }
            if (sceneindex == 0) { transition.SetTrigger("end1"); }
            if (sceneindex == 1) { transition.SetTrigger("end2"); }
            if (sceneindex == 0) { transition.SetTrigger("end1"); }
            if (sceneindex == 1) { transition.SetTrigger("end2"); }
            if (sceneindex == 0) { transition.SetTrigger("end1"); }
            if (sceneindex == 0) { transition.SetTrigger("end1"); }
            if (sceneindex == 0) { transition.SetTrigger("end1"); }
            if (sceneindex == 1) { transition.SetTrigger("end2"); }
            if (sceneindex == 1) { transition.SetTrigger("end2"); }
            if (sceneindex == 1) { transition.SetTrigger("end2"); }
        }
        else { firstboot = false; Debug.LogWarning("firstboot:" + firstboot); }




    }



}
