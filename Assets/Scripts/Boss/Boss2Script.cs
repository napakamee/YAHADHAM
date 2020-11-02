using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyBox;


public class Boss2Script : MonoBehaviour
{
    
    [Separator("Behavior")]
    [SerializeField] bool isFollowPlayer;
    [ConditionalField(nameof(isFollowPlayer))] [SerializeField] Transform player;
    [SerializeField] bool isRandomSpeed;
    [SerializeField] bool isLaser;
    [ConditionalField(nameof(isRandomSpeed))] public float minSpeed, maxSpeed, randSpeed;

    [Separator("Normal Stat")]
    public float speed = 3.0f;
    public float damage = 5f;
    public float hp = 3;
    public int getScore = 10;
    public GameObject explosion;
    public GameObject bigExplode;
    private Rigidbody2D rb;
    private Vector2 screenBounds;

    public bool isStop =false;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Rigidbody2D>().velocity=new Vector3(-1,0,0);
        gameObject.name="Boss2";
        rb = this.GetComponent<Rigidbody2D>();

        if (!isFollowPlayer && !isRandomSpeed)
            rb.velocity = new Vector2(-speed, 0);

        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        rb.freezeRotation = true;

        if (isRandomSpeed)
        {
            randSpeed = Random.Range(minSpeed, maxSpeed);
            rb.velocity = new Vector2(-randSpeed, 0);
        }

        if (GameManagement.Instance.isStartGame)
            player = GameObject.FindGameObjectWithTag("Player").transform;

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
    void Update()
    {
       
        if(this.transform.position.x<=4 && !isStop)
        {
            isStop=true;
            rb.velocity = new Vector2(0,-1);
        }
        if(GetComponent<Transform>().position.y>2.5f&& isStop)
        {
           
            rb.velocity = new Vector2(0,-1);

        }
        if(GetComponent<Transform>().position.y<-1.5f&& isStop)
        {
            
            rb.velocity = new Vector2(0,1);

        }
        
        if (transform.position.x < screenBounds.x * -1.2)
            Destroy(this.gameObject);

        if (GameManagement.Instance.isStartGame)
        {
            if (isFollowPlayer)
                transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
        }


    }
}


