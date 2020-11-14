using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossOneFollowingBullet : MonoBehaviour
{
    [SerializeField]
    GameObject bullet;
    public float hp = 100;
    public float fireRate;
    float nextFire;
    public int ammoAmount;
    float Interval = 1;
    public float delay = 1;
    bool LaserOn;
    public GameObject Laser;
    [SerializeField] bool isLaser;
    public bool IsStop;
    public float timeStart = 8;
    private Rigidbody2D rbb;

    public float speed = 3;
    public float damage = 5f;
    public int getScore = 10;
    public GameObject explosion;
    public GameObject bigExplode;
    private Vector2 screenBounds;

    void Start()
    {
        GetComponent<Rigidbody2D>().velocity = new Vector2(-speed, 0);
        rbb = this.GetComponent<Rigidbody2D>();
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        IsStop = false;
        ammoAmount = 4;
        fireRate = 1.0f;
        nextFire = Time.time;
        LaserOn = false;
        Laser.SetActive(false);
        rbb.freezeRotation = true;
    }

    void Update()
    {
        if (this.transform.position.x <= (screenBounds.x - 3f) && !IsStop)
        {
            rbb.velocity = new Vector2(0, -1);
            IsStop = true;
        }
        Interval -= Time.deltaTime;
        CheckToFire();
        if (ammoAmount >= 1 && Interval <= 0)
        {
            shot();
            ammoAmount--;
            Interval = 1;
        }
        if (transform.position.y >= 2.3f && IsStop)
        {
            rbb.velocity = new Vector2(0, -1);
        }
        if (transform.position.y <= -1.58f && IsStop)
        {
            rbb.velocity = new Vector2(0, 1);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<Collider2D>().tag.Contains("Bullet"))
        {
            HitByBullet(other);
        }

        if (other.GetComponent<Collider2D>().tag.Contains("Player") && !isLaser)
        {
            HitByPlayer(other);
        }

    }

    void HitByBullet(Collider2D other)
    {
        GameObject expl = Instantiate(explosion, transform.position, Quaternion.identity) as GameObject;
        hp -= GameManagement.Instance.BulletDamage;
        Destroy(other.gameObject);
        Destroy(expl, 1);

        if (hp <= 0)
        {
            GameObject b_expl = Instantiate(bigExplode, transform.position, Quaternion.identity) as GameObject;
            GameManagement.Instance.Score += getScore;
            Destroy(this.gameObject);
            Destroy(b_expl, 2);
        }
    }

    void HitByPlayer(Collider2D other)
    {
        GameObject b_expl = Instantiate(bigExplode, transform.position, Quaternion.identity) as GameObject;
        GameManagement.Instance.m_hp -= damage;
        Destroy(b_expl, 2);
        Destroy(this.gameObject);
    }

    void CheckToFire()
    {

        if (ammoAmount <= 0 && GameManagement.Instance.isStartGame)
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
