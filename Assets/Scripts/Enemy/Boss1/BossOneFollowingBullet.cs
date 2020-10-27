using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossOneFollowingBullet : MonoBehaviour
{
    [SerializeField]
    GameObject bullet;
    public float fireRate;
    float nextFire;
    public int ammoAmount;
    float Interval = 1;
    public float delay = 1;
    bool LaserOn;
    public GameObject Laser;
    bool IsStop;
    public float timeStart = 8;
    Rigidbody2D rbb;

    public float speed = 3;
    void Start()
    {
        rbb = GetComponent<Rigidbody2D>();

        rbb.velocity = new Vector2(-speed, 0);
        IsStop = false;
        ammoAmount = 4;
        fireRate = 1.0f;
        nextFire = Time.time;
        LaserOn = false;
        Laser.SetActive(false);

    }

    void Update()
    {
        Interval -= Time.deltaTime;
        CheckToFire();
        if (ammoAmount >= 1 && Interval <= 0)
        {
            shot();
            ammoAmount--;
            Interval = 1;
        }
        if(transform.position.x <= 6.82f && !IsStop)
        {
            rbb.velocity = new Vector2(0,-1);
            IsStop = true;
        }
        if(transform.position.y >= 2.3f){
            rbb.velocity = new Vector2(0,-1);
        }
        if(transform.position.y <= -1.58f)
        {
            rbb.velocity = new Vector2(0,1);
        }

    }

    void CheckToFire()
    {

        if (ammoAmount <= 0)
        {
            delay -= Time.deltaTime;
            if (delay <= 0)
            {
                LaserOn = true;
                Laser.SetActive(true);
                Debug.Log("LaserOn");
                TimerLaser();
                //delay=2;
            }
        }
    }

    void shot()
    {
        if (Time.time > nextFire)
        {
            Instantiate(bullet, transform.position, Quaternion.identity);
            nextFire = Time.time + fireRate;
        }
    }
    void TimerLaser()
    {
        timeStart -= Time.deltaTime;
        if (timeStart <= 1)
        {
            Debug.Log("LaserOff");
            ammoAmount = 4;
            LaserOn = false;
            Laser.SetActive(false);
            timeStart = 8;
            delay = 2;
        }
    }
}
