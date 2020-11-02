using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetActiveForBoss3 : MonoBehaviour
{
    
    public GameObject FirePathern;
    public GameObject DoubleFire;
    public GameObject EnemyFire2;
    float timeStart=15;
    int stage=0;
    private  EnemyFirePattern enemyFirePattern;


    // Start is called before the first frame update
    void Start()
    {
        
        stage=1;
        enemyFirePattern=FirePathern.GetComponent<EnemyFirePattern>();

       
    }
    
    void TimeToChange()
    {
        
        enemyFirePattern.normalFire=true;
        DoubleFire.SetActive(false);
        EnemyFire2.SetActive(false);
        
            
            if(timeStart<=0)
            {
                stage=2;
                timeStart=15;
            }
            
            
        
    }
    void TimeToChange2()
    {
       enemyFirePattern.normalFire=false;
        DoubleFire.SetActive(false);
        EnemyFire2.SetActive(true);
        
            
            if(timeStart<=0)
            {
                stage=3;
                timeStart=15;
            }
            
        
    }
    void TimeToChange3()
    {
        
        
        enemyFirePattern.normalFire=false;
        DoubleFire.SetActive(true);
        EnemyFire2.SetActive(false);
            if(timeStart<=0)
            {
                stage=1;
                timeStart=15;
            }
            
        
    }
    // Update is called once per frame
    void LateUpdate()
    {
        switch(stage)
        {
            case 1 : TimeToChange();
            break;
            case 2 : TimeToChange2();
            break;
            case 3 : TimeToChange3();
            break;
            default:
            break;


        }
        timeStart -=Time.deltaTime;
        
        
        
    }
}
