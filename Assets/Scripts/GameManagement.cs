using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameManagement : Singleton<GameManagement>
{
    protected GameManagement() {}

    [SerializeField] private int m_score = 0;
    [SerializeField] public float m_hp = 100;
    public Slider myHealthBar;
    public float Max_HP = 100;
    public float fireRate = 0.5f;
    
    public float bulletForce = 2f;
    public float BulletDamage = 1f;
    void Start()
    {
        myHealthBar.maxValue = Max_HP;
    }
    void Update()
    {
        if (m_hp >= 0)
        {
            m_hp -= Time.deltaTime;
            //print("hp: " + m_hp);
        }
        myHealthBar.value = m_hp;
    }
}
