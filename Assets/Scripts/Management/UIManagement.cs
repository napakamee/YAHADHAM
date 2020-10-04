using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManagement : MonoBehaviour
{
    // Start is called before the first frame update
    public static UIManagement instance;

    public GameObject UIDashBoard;
    public ScoreControl m_MainScore;
    public ScoreControl m_DBScore;
    public ScoreControl m_DBBestScore;

    [SerializeField]
    private string m_KeySaveScore = "BestScore";
    public string KeySaveScore
    {
        get { return m_KeySaveScore; }
    }
    private void Awake()
    {
        if (instance == null)
            instance = this;
    }
    public void SetScore(int score, bool isStartGame)
    {
        m_MainScore.UpdateScore(score);

        if (isStartGame == false)
        {
            int m_bestScore = 0;
            m_bestScore = PlayerPrefs.GetInt(m_KeySaveScore);
            if (score > m_bestScore)
            {
                PlayerPrefs.SetInt(m_KeySaveScore, score);
                m_bestScore = score;
            }
            m_DBScore.UpdateScore(score);
            m_DBBestScore.UpdateScore(m_bestScore);
        }
    }
    
    public void SetupControl(bool isStartGame)
    {
        UIDashBoard.SetActive(!isStartGame);
        //Debug.Log(!isStartGame);
            
    }
}
