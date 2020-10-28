using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseScript : MonoBehaviour
{
    public void Pause()
    {
        SceneManager.LoadScene("Pause",  LoadSceneMode.Additive);
    }
}
