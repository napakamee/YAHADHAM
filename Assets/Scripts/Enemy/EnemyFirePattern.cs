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

    [SerializeField] bool isLaser;
    [ConditionalField(nameof(isLaser))] public GameObject laserStart, laserMiddle, laserEnd;
    private GameObject start;
    private GameObject middle;
    private GameObject end;
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

            Vector3 bulMoveVector = new Vector3(bulDirX, bulDirY, 0f);
            Vector2 bulDir = (bulMoveVector - transform.position).normalized;

            GameObject b = Instantiate(bullet) as GameObject;
            b.transform.position = transform.position;
            b.transform.rotation = transform.rotation;
            b.GetComponent<EnemyBullet>().SetMoveDirection(bulDir);
            //b.GetComponent<Rigidbody2D>();
            angle += angleStep;
        }
    }

    private void LaserFire()
    {
        if (start == null)
        {
            start = Instantiate(laserStart) as GameObject;
            start.transform.parent = this.transform;
            start.transform.localPosition = Vector2.zero;
        }

        // Laser middle
        if (middle == null)
        {
            middle = Instantiate(laserMiddle) as GameObject;
            middle.transform.parent = this.transform;
            middle.transform.localPosition = Vector2.zero;
        }

        // Define an "infinite" size, not too big but enough to go off screen
        float maxLaserSize = 25f;
        float currentLaserSize = maxLaserSize;

        // Raycast at the right as our sprite has been design for that
        Vector2 laserDirection = this.transform.up;
        Debug.DrawRay(transform.position, laserDirection, Color.green);
        RaycastHit2D hit = Physics2D.Raycast(this.transform.position, laserDirection, maxLaserSize, 1 << LayerMask.NameToLayer("Player"));

        if (hit.collider != null)
        {
            // We touched something!
            //if (hit.collider.gameObject.CompareTag("Player"))
           // {
                // -- Get the laser length
                currentLaserSize = Vector2.Distance(hit.point, this.transform.position);

                // -- Create the end sprite
                if (end == null)
                {
                    end = Instantiate(laserEnd) as GameObject;
                    end.transform.parent = this.transform;
                    end.transform.localPosition = Vector2.zero;
                }
            //}

        }
        else
        {
            // Nothing hit
            // -- No more end
            if (end != null) Destroy(end);
        }

        // Place things
        // -- Gather some data
        float startSpriteWidth = start.GetComponent<Renderer>().bounds.size.y;
        float endSpriteWidth = 0f;
        if (end != null) endSpriteWidth = end.GetComponent<Renderer>().bounds.size.y;

        // -- the middle is after start and, as it has a center pivot, have a size of half the laser (minus start and end)
        middle.transform.localScale = new Vector3(currentLaserSize - startSpriteWidth, middle.transform.localScale.y, middle.transform.localScale.z);
        middle.transform.localPosition = new Vector2(0f, (currentLaserSize / 2f));

        // End?
        if (end != null)
        {
            end.transform.localPosition = new Vector2(0f, currentLaserSize);
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
        if (isLaser)
            LaserFire();
    }
}
