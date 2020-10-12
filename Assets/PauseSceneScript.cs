using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PauseSceneScript : MonoBehaviour
{
    [SerializeField] FadeOffAction fadeOffAction;
    [SerializeField] Animator animator;

    private void Awake()
    {
        Time.timeScale = 0;
    }

    public void ReturnToStageSelect()
    {
        Time.timeScale = 1;
        SceneManagementSingleton.Instance.isQuitingStage = true;
        fadeOffAction.SceneToLoad = "MainmenuBackground";
        animator.SetTrigger("FadeOff");
    }

    public void ResumeStage()
    {
        Time.timeScale = 1;
        SceneManager.UnloadSceneAsync("Pause");
    }

    public void ResetStage()
    {
        Time.timeScale = 1;
        GameManagement.Instance.ResetLevel();
        SceneManager.UnloadSceneAsync("Pause");
    }
}
