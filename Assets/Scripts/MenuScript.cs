using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    [SerializeField] GameObject[] UIs;
    [SerializeField] GameObject Player;
    Vector3[] UIs_pos;
    Vector3 Player_pos;
    string sceneToLoad = "";
    bool transitioningOut = false;

    bool isCalledScene = false;

    private void Awake()
    {
        transitioningOut = false;
        UIs_pos = new Vector3[UIs.Length];
        for (int i = 0; i < UIs.Length; i++)
        {
            UIs_pos[i] = Vector3.zero;
            UIs_pos[i] = UIs[i].transform.position;
        }
        Player_pos = Player.transform.position;
    }
    private void Start()
    {
        for (int i = 0; i < UIs.Length; i++)
        {
            UIs[i].transform.position = new Vector3((-220f * 0.33f * i) - 220f, UIs[i].transform.position.y, UIs[i].transform.position.z);
        }
        Player.transform.position = new Vector3(20f, Player.transform.position.y, Player.transform.position.z); ;
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
    }
    private void Transition_Out(string SceneToLoad)
    {
        for (int i = 0; i < UIs.Length; i++)
        {
            if (UIs[i].transform.position.x > (UIs_pos[i].x - (230f * 0.33f * (UIs.Length - i)) - 230f) + 2)
            {
                UIs[i].transform.position -= new Vector3(Mathf.Abs(UIs[i].transform.position.x - (UIs_pos[i].x - (230f * 0.33f * (UIs.Length - i)) - 230f)) / 10, 0, 0);
            }
        }

        if (Player.transform.position.x < 20f - 2)
        {
            Player.transform.position += new Vector3(Mathf.Abs(Player.transform.position.x - 20f) / 8f, 0, 0);
        }

        if (UIs[UIs.Length - 1].transform.position.x <= (UIs_pos[UIs_pos.Length - 1].x - (180f * 0.33f) - 180f) + 2 && !isCalledScene)
        {
            SceneManager.LoadScene(SceneToLoad, LoadSceneMode.Additive);
            isCalledScene = true;
        }

        if (UIs[UIs.Length - 1].transform.position.x <= (UIs_pos[UIs_pos.Length - 1].x - (230f * 0.33f) - 230f) + 2)
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
        transitioningOut = true;
        sceneToLoad = "StageSelect";
    }
    public void LoadOptions()
    {
        transitioningOut = true;
        sceneToLoad = "Options";
    }
    public void LoadCredits()
    {
        transitioningOut = true;
        sceneToLoad = "Credits";
    }
    public void QuitGame()
    {
        transitioningOut = true;
        sceneToLoad = "Quit";
    }
}
