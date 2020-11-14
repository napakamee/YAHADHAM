using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetActiveForBoss2 : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject Pathern;

    public float firstStateFirerate = 0;
    float timeStart = 10;
    int stage = 0;
    private EnemyFirePattern pathern;
    public GameObject Meteorite;
    public Transform[] spawnPosition;
    public float delay = 0;
    private float nextSpawn = 0;




    // Start is called before the first frame update
    void Start()
    {

        stage = 1;
        pathern = Pathern.GetComponent<EnemyFirePattern>();



    }

    void TimeToChange()
    {

        pathern.normalFire = true;
        pathern.isSpread = false;
        pathern.firerate = firstStateFirerate;




        if (timeStart <= 0)
        {
            stage = 2;
            timeStart = 10;
        }



    }
    void TimeToChange2()
    {
        pathern.normalFire = false;
        pathern.isSpread = true;
        pathern.firerate = firstStateFirerate;




        if (timeStart <= 0)
        {
            stage = 1;
            timeStart = 10;
        }


    }

    void MeteorSpawn()
    {
        GameObject meteorite = Instantiate(Meteorite, spawnPosition[Random.Range(0, spawnPosition.Length)]) as GameObject;

    }
    // Update is called once per frame
    void Update()
    {

    }
    void LateUpdate()
    {
        nextSpawn -= Time.deltaTime;
        if (nextSpawn <= 0)
        {
            nextSpawn = delay;
            MeteorSpawn();
        }
        switch (stage)
        {
            case 1:
                TimeToChange();
                break;
            case 2:
                TimeToChange2();
                break;

            default:
                break;


        }
        timeStart -= Time.deltaTime;



    }
}
