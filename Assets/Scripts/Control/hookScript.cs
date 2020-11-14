using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hookScript : MonoBehaviour
{
    GrapplingHook grappling;
    FixedJoint2D joint2D;
    bool returning;
    Transform player;
    Rigidbody2D rb;
    Vector2 returnMovement;
    public float returnSpeed = 15f;
    public float MAXDISTANCE = 20f;

    // Start is called before the first frame update
    void Start()
    {
        grappling = GameObject.FindGameObjectWithTag("Player").GetComponent<GrapplingHook>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = this.GetComponent<Rigidbody2D>();
        returning = false;
    }
    void Update()
    {
        if ((this.transform.position - player.transform.position).magnitude > MAXDISTANCE)
        {
            returning = true;
        }
        if (returning)
        {
            Vector3 dir = player.position - transform.position;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            rb.rotation = angle + 180;
            dir.Normalize();
            returnMovement = dir;
        }
    }
    void FixedUpdate()
    {
        if (returning)
        {
            ReturnToPlace(returnMovement);
        }
    }
    void ReturnToPlace(Vector2 dir)
    {
        rb.MovePosition((Vector2)transform.position + (dir * returnSpeed * Time.deltaTime));
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Item")
        {
            joint2D = gameObject.AddComponent<FixedJoint2D>();
            joint2D.connectedBody = other.gameObject.GetComponent<Rigidbody2D>();
            this.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            returning = true;
        }
        if (other.gameObject.tag == "Player" && returning)
        {

            foreach (FixedJoint2D fj in GetComponents<FixedJoint2D>())
            {
                Destroy(fj);
            }
            rb.velocity = Vector2.zero;
            joint2D = null;
            returning = false;
            grappling.returned();
            Lean.Pool.LeanPool.Despawn(this.gameObject);
        }
    }
}
