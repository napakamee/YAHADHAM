using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyBox;

public class Enemy01 : MonoBehaviour
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
    private Vector2 norm_velocity;
    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();

        if (!isFollowPlayer && !isRandomSpeed)
        {
            rb.velocity = new Vector2(-speed, 0);
            norm_velocity = rb.velocity;
        }
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        rb.freezeRotation = true;

        if (isRandomSpeed)
        {
            randSpeed = Random.Range(minSpeed, maxSpeed);
            rb.velocity = new Vector2(-randSpeed, 0);
            norm_velocity = rb.velocity;
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
        if (transform.position.x < screenBounds.x * -1.2)
            Destroy(this.gameObject);

        if (GameManagement.Instance.isStartGame)
        {
            if (isFollowPlayer)
            {
                transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
            }
            else
            {
                rb.velocity = norm_velocity;
            }
        }

    }
}
