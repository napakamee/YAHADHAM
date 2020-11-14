using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    Animator animator;
    public Animator FishAnim;
    string sceneToLoad = "";
    bool transitioningOut = false;

    bool isCalledScene = false;
    bool isCallingScene = false;
    [SerializeField] FadeOffAction fadeOffAction;

    private void Awake()
    {
        animator = this.gameObject.GetComponent<Animator>();
        animator.SetBool("IsLoaded", true);
        FishAnim.SetBool("IsLoaded", true);
        sceneToLoad = null;
        isCallingScene = false;
        isCalledScene = false;
    }

    public void TransOut()
    {
        if (animator.GetBool("IsLoaded"))
        {
            animator.SetBool("IsLoaded", false);
        }
        if (FishAnim.GetBool("IsLoaded"))
        {
            FishAnim.SetBool("IsLoaded", false);
        }
    }
    public void LoadOrder()
    {
        if (sceneToLoad == "StageTutorial")
        {
            SceneManager.LoadScene(sceneToLoad, LoadSceneMode.Single);
        }
        else if (sceneToLoad != "Quit")
        {
            SceneManager.LoadScene(sceneToLoad, LoadSceneMode.Additive);
        }
    }
    public void UnloadOrder()
    {
        if (sceneToLoad == "Quit") { Application.Quit(); }
        SceneManager.UnloadSceneAsync("Mainmenu");
    }

    public void LoadStagesSelect()
    {
        if (!isCallingScene)
        {
            TransOut();
            transitioningOut = true;
            sceneToLoad = "StageSelect";
            isCallingScene = true;
        }
    }
    public void LoadOptions()
    {
        if (!isCallingScene)
        {
            TransOut();
            transitioningOut = true;
            sceneToLoad = "Options";
            isCallingScene = true;
        }
    }
    public void LoadCredits()
    {
        if (!isCallingScene)
        {
            TransOut();
            transitioningOut = true;
            sceneToLoad = "Credits";
            isCallingScene = true;
        }
    }
    public void QuitGame()
    {
        if (!isCallingScene)
        {
            TransOut();
            transitioningOut = true;
            sceneToLoad = "Quit";
            isCallingScene = true;
        }
    }
    public void LoadTutorials()
    {
        if (!isCallingScene)
        {
            fadeOffAction.FadeOff();
            TransOut();
            transitioningOut = true;
            sceneToLoad = "StageTutorial";
            isCallingScene = true;
        }
    }
}
