using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TitleScreenScript : MonoBehaviour
{
    bool _isTitleActive = true;
    bool _loadedMenuScene = false;
    [SerializeField] Text title;
    [SerializeField] Text pressToStart;
    float blinkValue = 1f;
    Color textBlink = new Color(1, 1, 1, 1);

    private void Awake() {
        if (SceneManagementSingleton.Instance.isQuitingStage)
        {
            _isTitleActive = false;
            SceneManagementSingleton.Instance.isQuitingStage = false;
            SceneManager.LoadScene("StageSelect", LoadSceneMode.Additive);
            gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        if (Input.touchCount > 0 || Input.anyKeyDown)
        {
            _isTitleActive = false;
        }
        if (!_isTitleActive && !_loadedMenuScene)
        {
            if (title.color.a > 0)
            {
                title.color = new Color(1, 1, 1, title.color.a - 2 * Time.deltaTime);
                if (textBlink.a > 0)
                {
                    textBlink = new Color(1, 1, 1, textBlink.a - 2 * Time.deltaTime);
                    pressToStart.color = textBlink;
                }
            }
            else {SceneManager.LoadScene("Mainmenu", LoadSceneMode.Additive); _loadedMenuScene = true; gameObject.SetActive(false); }
        }
        else if(!_loadedMenuScene) TitleMode();
    }

    private void TitleMode()
    {
        textBlink = new Color(1, 1, 1, textBlink.a + blinkValue * Time.deltaTime);
        if (textBlink.a <= .2f || textBlink.a >= 1)
        {
            if (textBlink.a <= .2f) textBlink.a = .2f;
            if (textBlink.a >= 1) textBlink.a = 1;
            blinkValue = blinkValue * -1;
        }
        pressToStart.color = textBlink;
    }

    /*private void ReLaunch()
    {
        _isTitleActive = true;
        _loadedMenuScene = false;
        blinkValue = 1f;
        textBlink = new Color(1, 1, 1, 1);
        pressToStart.color = textBlink;
        title.color = color.white;
    }*/
}
