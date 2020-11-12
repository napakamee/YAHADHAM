using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    [SerializeField] GameObject[] UIs = null;
    [SerializeField] GameObject Player = null;
    [SerializeField] GameObject HowToPlay = null;
    Vector3[] UIs_pos;
    Vector3 Player_pos;
    Vector3 HowToPlay_pos;
    string sceneToLoad = "";
    bool transitioningOut = false;

    bool isCalledScene = false;
    bool isCallingScene = false;
    [SerializeField] FadeOffAction fadeOffAction;

    private void Awake()
    {
        sceneToLoad = null;
        isCallingScene = false;
        isCalledScene = false;
        transitioningOut = false;
        UIs_pos = new Vector3[UIs.Length];
        for (int i = 0; i < UIs.Length; i++)
        {
            UIs_pos[i] = Vector3.zero;
            UIs_pos[i] = UIs[i].transform.position;
        }
        Player_pos = Player.transform.position;
        HowToPlay_pos = HowToPlay.transform.position;

    }
    private void Start()
    {
        for (int i = 0; i < UIs.Length; i++)
        {
            UIs[i].transform.position = new Vector3((-220f * 0.33f * i) - 220f, UIs[i].transform.position.y, UIs[i].transform.position.z);
        }
        Player.transform.position = new Vector3(20f, Player.transform.position.y, Player.transform.position.z);
        HowToPlay.transform.position = new Vector3(HowToPlay.transform.position.x + 200, HowToPlay.transform.position.y, HowToPlay.transform.position.z);
    }

    private void Update()
    {
        if (!transitioningOut)
        {
            Transition_In();
        }
        else
        {
            Transition_Out(sceneToLoad);
        }
    }
    private void Transition_In()
    {
        for (int i = 0; i < UIs.Length; i++)
        {
            if (UIs[i].transform.position.x < UIs_pos[i].x - 2)
            {
                UIs[i].transform.position += new Vector3(Mathf.Abs(UIs[i].transform.position.x - UIs_pos[i].x) / 10, 0, 0);
            }
        }
        if (Player.transform.position.x > Player_pos.x + 2)
        {
            Player.transform.position -= new Vector3(Mathf.Abs(Player.transform.position.x - Player_pos.x) / 8f, 0, 0);
        }
        if (HowToPlay.transform.position.x > HowToPlay_pos.x + 2)
        {
            HowToPlay.transform.position -= new Vector3(Mathf.Abs(HowToPlay.transform.position.x - HowToPlay_pos.x) / 10, 0, 0);
        }
    }
    private void Transition_Out(string SceneToLoad)
    {
        for (int i = 0; i < UIs.Length; i++)
        {
            if (UIs[i].transform.position.x > (UIs_pos[i].x - (230f * 0.33f * (UIs.Length - i)) - 800) + 2)
            {
                UIs[i].transform.position -= new Vector3(Mathf.Abs(UIs[i].transform.position.x - (UIs_pos[i].x - (230f * 0.33f * (UIs.Length - i)) - 800)) / 30, 0, 0);
            }
        }

        if (Player.transform.position.x < 20f - 2)
        {
            Player.transform.position += new Vector3(Mathf.Abs(Player.transform.position.x - 20f) / 8f, 0, 0);
        }
        if (HowToPlay.transform.position.x < HowToPlay_pos.x + 198)
        {
            HowToPlay.transform.position += new Vector3(Mathf.Abs(HowToPlay.transform.position.x - HowToPlay_pos.x + 200) / 10f, 0, 0);
        }

        if (UIs[UIs.Length - 1].transform.position.x <= (UIs_pos[UIs_pos.Length - 1].x - (180f * 0.33f) - 180f) + 2 && !isCalledScene)
        {
            if (sceneToLoad == "Quit")
            {
                fadeOffAction.FadeOff();
                SoundManagerSingleton.Instance.MasterVolume -= Time.deltaTime * 100;
            }
            else
            {
                SceneManager.LoadScene(SceneToLoad, LoadSceneMode.Additive);
                isCalledScene = true;
            }
        }

        if (UIs[UIs.Length - 1].transform.position.x <= (UIs_pos[UIs_pos.Length - 1].x - (230f * 0.33f) - 800f) + 2)
        {
            if (sceneToLoad == "Quit")
            {
                Application.Quit();
            }
            else
            {
                SceneManager.UnloadSceneAsync("Mainmenu");
            }
        }
    }

    public void LoadStagesSelect()
    {
        if (!isCallingScene)
        {
            transitioningOut = true;
            sceneToLoad = "StageSelect";
            isCallingScene = true;
        }
    }
    public void LoadOptions()
    {
        if (!isCallingScene)
        {
            transitioningOut = true;
            sceneToLoad = "Options";
            isCallingScene = true;
        }
    }
    public void LoadCredits()
    {
        if (!isCallingScene)
        {
            transitioningOut = true;
            sceneToLoad = "Credits";
            isCallingScene = true;
        }
    }
    public void QuitGame()
    {
        if (!isCallingScene)
        {
            transitioningOut = true;
            sceneToLoad = "Quit";
            isCallingScene = true;
        }
    }
    public void LoadTutorials()
    {
        if (!isCallingScene)
        {
            transitioningOut = true;
            sceneToLoad = "StageTutorial";
            isCallingScene = true;
        }
    }
}
