using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class GameManagement : Singleton<GameManagement>
{
    protected GameManagement() {}

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
    public float Max_HP = 100;
    public float fireRate = 0.5f;
    public float bulletForce = 2f;
    public float BulletDamage = 1f;

    public bool isStartGame = false;

    public ShipControl m_player;
    
    void Start()
    {
        myHealthBar.maxValue = Max_HP;
        isStartGame = true;
        SetupControl();
    }
    void Update()
    {
        if (m_hp >= 0)
        {
            m_hp -= Time.deltaTime;
            //print("hp: " + m_hp);
        }
        myHealthBar.value = m_hp;

        if (m_player.isDead){
            isStartGame = false;
            SetupControl();
        }
    }

    public void PlusScore(float factor){
        score += (int)(10 * factor);
    }
    void SetupControl(){
        UIManagement.instance.SetupControl(isStartGame);
        UIManagement.instance.SetScore(score, isStartGame);
        
    }

    public void ResetLevel()
    {
        SceneManager.LoadScene("MainGame");
    }
}
