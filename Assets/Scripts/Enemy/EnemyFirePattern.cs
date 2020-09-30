using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyBox;

public class EnemyFirePattern : MonoBehaviour
{
    public GameObject bullet;
    [Separator("Behavior")]
    [SerializeField] bool normalFire;
    [SerializeField] bool isSpread;
    [ConditionalField(nameof(isSpread))] [SerializeField] int bulletAmount = 3;
    [ConditionalField(nameof(isSpread))] [SerializeField] float startAngle = 90f, endAngle = 270f;

    private Vector2 bulletMoveDirection;


    [Separator("Normal Stat")]
    public float bulletSpeed = 10.0f;
    public float firerate = 2.0f;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(enemyFire());
    }
    private void spawnBullet()
    {

        GameObject b = Instantiate(bullet) as GameObject;
        b.transform.position = this.transform.position;
        b.transform.rotation = this.transform.rotation;
        b.GetComponent<Rigidbody2D>().velocity = new Vector2(-bulletSpeed, 0);
    }

    private void spawnSpreadBullet()
    {
        float angleStep = (endAngle - startAngle) / bulletAmount;
        float angle = startAngle;

        for (int i = 0; i < bulletAmount + 1; i++)
        {
            float bulDirX = transform.position.x + Mathf.Sin((angle * Mathf.PI) / 180f);
            float bulDirY = transform.position.y + Mathf.Cos((angle * Mathf.PI) / 180f);

            Vector3 bulMoveVector = new Vector3(bulDirX,bulDirY,0f);
            Vector2 bulDir = (bulMoveVector - transform.position).normalized;

            GameObject b = Instantiate(bullet) as GameObject;
            b.transform.position = transform.position;
            b.transform.rotation = transform.rotation;
            b.GetComponent<EnemyBullet>().SetMoveDirection(bulDir);
            //b.GetComponent<Rigidbody2D>();
            angle += angleStep;
        }
    }

    IEnumerator enemyFire()
    {
        while (true)
        {
            yield return new WaitForSeconds(firerate);
            if (normalFire)
                spawnBullet();

            if (isSpread)
                spawnSpreadBullet();
        }
    }
    // Update is called once per frame
    void Update()
    {

    }
}
