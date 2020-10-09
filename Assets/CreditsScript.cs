using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditsScript : MonoBehaviour
{
    Animator animator;
    private void Awake()
    {
        animator = this.gameObject.GetComponent<Animator>();
        animator.SetBool("IsLoaded", true);
    }

    public void TransOut()
    {
        if (animator.GetBool("IsLoaded"))
        {
            animator.SetBool("IsLoaded", false);
        }
    }
    public void LoadMainmenu()
    {
        if (!animator.GetBool("IsLoaded"))
        {
            SceneManager.LoadScene("Mainmenu", LoadSceneMode.Additive);
            SceneManager.UnloadSceneAsync("Credits");
        }
    }
}
