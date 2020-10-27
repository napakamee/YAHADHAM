using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed =5f;

    Rigidbody2D rb;

    [SerializeField]
    GameObject target;
 //   int ammoAmount;

 //   bool LaserOn;
  //  public float timeStart = 8;
 //   public GameObject Laser;
    

    Vector2 moveDir;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        target = GameObject.FindGameObjectWithTag("Player");
        moveDir = (target.transform.position - transform.position).normalized * speed;
        rb.velocity = new Vector2(moveDir.x, moveDir.y);
      //  Destroy (gameObject,5f);
     //   ammoAmount =4;
        //LaserOn = false;
    }

   
   
void OnTriggerEnter2D(Collider2D col)
{
    if(col.gameObject.tag.Equals("Player"))
    {
            Destroy (gameObject);
    }
}
}
