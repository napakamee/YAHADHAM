using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageSelectScript : MonoBehaviour
{
    string sceneToLoad;
    int loadType;
    bool isCalledScene = false;
    bool isCallingScene = false;
    Animator animator;
    [SerializeField] FadeOffAction fadeOffAction;
    private void Awake()
    {
        sceneToLoad = null;
        isCallingScene = false;
        isCalledScene = false;
        animator = this.gameObject.GetComponent<Animator>();
        animator.SetBool("IsLoaded", true);
    }
    private void Update()
    {
        if (sceneToLoad != null && !isCalledScene)
        {
            if (animator.GetCurrentAnimatorStateInfo(0).IsName("Empty"))
            {
                if (loadType == 0)
                {
                    SceneManager.LoadScene(sceneToLoad, LoadSceneMode.Additive);
                    isCalledScene = true;
                    SceneManager.UnloadSceneAsync("StageSelect");
                }
                else if (loadType == 1)
                {
                    isCalledScene = true;
                    fadeOffAction.FadeOff();
                    fadeOffAction.SceneToLoad = sceneToLoad;
                }
            }
        }
    }

    public void LoadOtherScene_Additive(string SceneToLoad)
    {
        if (!isCallingScene)
        {
            sceneToLoad = SceneToLoad;
            loadType = 0;
            animator.SetBool("IsLoaded", false);
            isCallingScene = true;
        }
    }
    public void LoadOtherScene_Single(string SceneToLoad)
    {
        if (!isCallingScene)
        {
            sceneToLoad = SceneToLoad;
            loadType = 1;
            animator.SetBool("IsLoaded", false);
            isCallingScene = true;
        }
    }
    public void LoadMainmenu()
    {
        //Leave This Blank
    }
}
