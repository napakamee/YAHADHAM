using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFollowBullet : MonoBehaviour
{
    public GameObject bullet;
    public float Speed=7f;
    Rigidbody2D rb;
    [SerializeField]
    GameObject target;
    Vector2 moveDir;
    // Start is called before the first frame update
    void Start()
    {
        rb=GetComponent<Rigidbody2D>();
        target=GameObject.FindGameObjectWithTag("Player");
        moveDir=(target.transform.position-transform.position).normalized*Speed;
        rb.velocity=new Vector2(moveDir.x,moveDir.y);
        Lean.Pool.LeanPool.Despawn(gameObject);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.tag.Equals("Player"))
        {
            
            Lean.Pool.LeanPool.Despawn(gameObject);
        }
    }

    
}
