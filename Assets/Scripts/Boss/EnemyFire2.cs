using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFire2 : MonoBehaviour
{
    public GameObject bullet;
    private float angle=0f;
    public float delay=1;
    private float nextShot=2;
    // Start is called before the first frame update
    void Start()
    {
        nextShot=Time.time+nextShot;
    }
   void Update()
   {
       
       if(Time.time>=nextShot)
       {
           nextShot=Time.time+delay;
           Fire();
       }
   }
    private void Fire()
    {
        float bulDirX=transform.position.x+Mathf.Sin((angle*Mathf.PI)/120f);
        float bulDirY=transform.position.y+Mathf.Cos((angle*Mathf.PI)/120f);

        Vector3 bulMoveVector = new Vector3(bulDirX,bulDirY,0f);
        Vector2 bulDir=(bulMoveVector-transform.position).normalized;

        GameObject b = Lean.Pool.LeanPool.Spawn(bullet) as GameObject;
        b.transform.position=transform.position;
        b.transform.rotation=transform.rotation;
        b.SetActive(true);
        b.GetComponent<EnemyBullet>().SetMoveDirection(bulDir);

        angle +=10f;
        Debug.Log("EnemyFire2");
    }

    
}
