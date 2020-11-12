using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseScript : MonoBehaviour
{
    [SerializeField] FadeOffAction fadeOffAction;
    [SerializeField] Animator animator;
    public void Pause()
    {
        SceneManager.LoadScene("Pause", LoadSceneMode.Additive);
    }
    public void ReturnToMenu()
    {
        if (fadeOffAction != null && animator != null)
        {
            SceneManagementSingleton.Instance.isQuitingStage = true;
            SceneManagementSingleton.Instance.isQuitingTutorial = true;
            fadeOffAction.SceneToLoad = "MainmenuBackground";
            animator.SetTrigger("FadeOff");
        }
    }
}
