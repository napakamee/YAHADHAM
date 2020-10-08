using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FadeOffAction : MonoBehaviour
{
    private string sceneToLoad = null;
    public string SceneToLoad { get { return sceneToLoad; } set { sceneToLoad = value; } }
    [SerializeField] Animator fader = null;
    [SerializeField] bool ActiveFadeIn;
    private void Awake()
    {
        if (ActiveFadeIn)
        {
            FadeIn();
        }
    }
    public void OnFadeComplete()
    {
        SceneManager.LoadScene(sceneToLoad, LoadSceneMode.Single);
    }
    public void FadeOff()
    {
        fader.SetTrigger("FadeOff");
    }
    public void FadeIn()
    {
        fader.SetTrigger("FadeIn");
    }
}
