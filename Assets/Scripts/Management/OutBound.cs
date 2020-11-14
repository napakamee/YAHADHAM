using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutBound : MonoBehaviour
{
    private Rigidbody2D rb;
    private Vector2 screenBounds;
    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x > screenBounds.x * 1.2 || transform.position.x < screenBounds.x * -1.2
        || transform.position.y > screenBounds.y * 1.5 || transform.position.y < screenBounds.y * -1.5)
        {
            Lean.Pool.LeanPool.Despawn(this.gameObject);
        }
    }
}
