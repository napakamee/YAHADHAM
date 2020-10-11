using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class GameManagement : MonoBehaviour
{
    //protected GameManagement() {}

    [SerializeField]
    private int score = 0;
    public int Score
    {
        get { return score; }
        set
        {
            //SetScore(value);
            score = value;
            UIManagement.instance.SetScore(score, isStartGame);
            
        }
    }    
    [SerializeField] public float m_hp = 100;
    public Slider myHealthBar;
    public static GameManagement Instance = null;
    public float Max_HP = 100;
    public float fireRate = 0.5f;
    public float bulletForce = 2f;
    public float BulletDamage = 1f;

    public bool isStartGame = false;
    public bool isWin = false;
    public bool isLose = false;
    public ShipControl m_player;
    void Awake()
    {
        if(Instance == null)
            Instance = this;
        
    }
    void Start()
    {
        myHealthBar.maxValue = Max_HP;
        isStartGame = true;
        SetupControl();
    }
    void Update()
    {
        if (m_hp >= 0 && !isWin)
        {
            m_hp -= Time.deltaTime;
            //print("hp: " + m_hp);
        }
        myHealthBar.value = m_hp;

        if (m_player.isDead){
            isLose = true;
            isStartGame = false;
            SetupControl();
        }
        if (isWin){
            isStartGame = false;
            SetupControl();
            
        }
    }

    public void PlusScore(float factor){
        score += (int)(10 * factor);
    }
    void SetupControl(){
        UIManagement.instance.SetupControl(isLose, isWin, isStartGame);
        UIManagement.instance.SetScore(score, isStartGame);
        
    }

    public void ResetLevel()
    {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.UnloadSceneAsync(scene.name);
        SceneManager.LoadScene(scene.name);
    }

    public void ChangeStage(){
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.UnloadSceneAsync(scene.name);
        if (scene.name == "Stage1")
            SceneManager.LoadScene("Stage2");
        if (scene.name == "Stage2")
            SceneManager.LoadScene("Stage3");
        if (scene.name == "Stage3")
            SceneManager.LoadScene("MainmenuBackground");
    }
}
