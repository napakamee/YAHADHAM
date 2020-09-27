using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy01 : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] bool isFollowPlayer;
    public float speed = 3.0f;
    public float hp = 3;
    private Rigidbody2D rb;
    private Vector2 screenBounds;
    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();

        if (!isFollowPlayer)
        {
            rb.velocity = new Vector2(-speed, 0);
        }

        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        rb.freezeRotation = true;
        player = GameObject.FindGameObjectWithTag("Player").transform;

    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<Collider2D>().tag.Contains("Bullet"))
        {
            hp--;
            Destroy(other.gameObject);
            if (hp <= 0)
            {
                Destroy(this.gameObject);
            }
        }
        if (other.GetComponent<Collider2D>().tag.Contains("Player"))
            Destroy(this.gameObject);
    }
    void Update()
    {
        //print ("obj" + transform.position.x);
        //print ("scr" + screenBounds.x * 2);
        if (transform.position.x < screenBounds.x * -1.2)
            Destroy(this.gameObject);

        if (isFollowPlayer)
            transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
    }
}
