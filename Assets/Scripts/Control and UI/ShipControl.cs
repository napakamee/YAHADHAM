using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.UI;
using UnityEngine.Audio;
public class ShipControl : MonoBehaviour
{
    public float speed = 3.0f;
    public Vector2 movement;
    public GameObject m_Bullet;
    Rigidbody2D m_Rigid;
    private Vector2 screenBounds;
    private bool isFiring = false;
    public bool IsFiring
    {
        get { return isFiring; }
        set { isFiring = value; }
    }

    private float nextFire = 0.0f;
    public Transform _crosshairPos;
    private Vector2 crosshairPos;

    public Transform firePoint;
    Touch touch;
    Vector2 touchPos;
    public LineRenderer HookLineRenderer;
    public SpriteRenderer CrosshairRenderer;
    public SpriteRenderer PlayerRenderer;
    public Sprite ShootingPlayer;
    public Sprite HookPlayer;
    public SpriteRenderer SightRenderer;
    public Sprite ShootingSight;
    public Sprite HookSight;
    private bool hookAiming;
    public bool HookAiming
    {
        get { return hookAiming; }
        set { hookAiming = value; }
    }
    public GameObject HookButton;
    public Image DamageUpgradeStatus;
    public Image FireRateUpgradeStatus;
    public Image BulletSpeedUpgradeStatus;
    public Image MaxHPUpgradeStatus;

    public AudioClip fireSound;
    public AudioClip collectSound;
    public AudioClip changeModeSound;


    //public Slider healthBar;

    // Start is called before the first frame update
    void Start()
    {
        m_Rigid = GetComponent<Rigidbody2D>();
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        touchPos = Camera.main.ScreenToWorldPoint(touch.position);
    }

    // Update is called once per frame
    void Update()
    {
        if (HookAiming)
        {
            PlayerRenderer.sprite = HookPlayer;
            SightRenderer.sprite = HookSight;
        }
        else
        {
            PlayerRenderer.sprite = ShootingPlayer;
            SightRenderer.sprite = ShootingSight;
        }
        if (Input.touchCount > 0)
        {
            for (int i = 0; i < Input.touchCount; i++)
            {
                touch = Input.GetTouch(i);
                touchPos = Camera.main.ScreenToWorldPoint(touch.position);
                if (touchPos.x < -7)
                    break;

            }

            if (EventSystem.current.IsPointerOverGameObject(touch.fingerId) == HookButton)
            {
                if (touch.tapCount == 2)
                {
                    if (touch.phase == TouchPhase.Began)
                    {
                        if (HookAiming)
                        {
                            HookAiming = false;
                            HookLineRenderer.positionCount = 0;
                            CrosshairRenderer.enabled = false;
                        }
                        else
                        {
                            HookAiming = true;
                            HookLineRenderer.positionCount = 0;
                            CrosshairRenderer.enabled = false;
                            this.GetComponent<AudioSource>().PlayOneShot(changeModeSound);
                        }
                    }
                }
            }
            if (touch.phase == TouchPhase.Moved || touch.phase == TouchPhase.Stationary)
            {
                if (touchPos.x < -7)
                {
                    if (transform.position.y < touchPos.y)
                        transform.position = new Vector2(transform.position.x, transform.position.y + speed * Time.deltaTime);

                    if (transform.position.y > touchPos.y)
                        transform.position = new Vector2(transform.position.x, transform.position.y - speed * Time.deltaTime);

                }
            }
        }

        crosshairPos = _crosshairPos.position;

        if (isFiring)
        {
            if (Time.time > nextFire)
            {
                nextFire = Time.time + GameManagement.Instance.fireRate;
                Shoot();
            }
        }
    }
    void FixedUpdate()
    {
        crosshairPos = _crosshairPos.position;
        Vector2 lookDir = crosshairPos - m_Rigid.position;

        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
        m_Rigid.rotation = angle;
    }
    void Shoot()
    {
        GameObject bl = Instantiate(m_Bullet, firePoint.position, firePoint.rotation);
        Rigidbody2D rb_bl = bl.GetComponent<Rigidbody2D>();

        rb_bl.AddForce(firePoint.up * GameManagement.Instance.bulletForce / 20, ForceMode2D.Force);
        this.GetComponent<AudioSource>().PlayOneShot(fireSound);
    }

    public void PickDamageUpgrade()
    {
        if (GameManagement.Instance.BulletDamage < 6)
        {
            GameManagement.Instance.BulletDamage += 1;

            if (GameManagement.Instance.BulletDamage > 6)
            {
                GameManagement.Instance.BulletDamage = 6;

            }

            if (DamageUpgradeStatus.color.a < 1)
            {
                DamageUpgradeStatus.color = new Color(DamageUpgradeStatus.color.r, DamageUpgradeStatus.color.g, DamageUpgradeStatus.color.b, DamageUpgradeStatus.color.a + 0.2f);

            }

        }
        else
        {
            Debug.Log("Damage is Fully Upgraded(5).");
        }

        this.GetComponent<AudioSource>().PlayOneShot(collectSound);

    }
    public void PickFireRateUpgrade()
    {
        if (GameManagement.Instance.fireRate > 0.1f)
        {
            GameManagement.Instance.fireRate -= 0.1f;

            if (GameManagement.Instance.fireRate < 0.1)
            {
                GameManagement.Instance.fireRate = 0.1f;
            }

            if (FireRateUpgradeStatus.color.a < 1)
            {
                FireRateUpgradeStatus.color = new Color(FireRateUpgradeStatus.color.r, FireRateUpgradeStatus.color.g, FireRateUpgradeStatus.color.b, FireRateUpgradeStatus.color.a + 0.2f);

            }

        }
        else
        {
            Debug.Log("Fire Rate is Fully Upgraded(5).");
        }

        this.GetComponent<AudioSource>().PlayOneShot(collectSound);
    }
    public void PickBulletSpeedUpgrade()
    {
        if (GameManagement.Instance.bulletForce < 2.5)
        {
            GameManagement.Instance.bulletForce += 0.25f;

            if (GameManagement.Instance.bulletForce > 2.5)
            {
                GameManagement.Instance.bulletForce = 2.5f;
            }

            if (BulletSpeedUpgradeStatus.color.a < 1)
            {
                BulletSpeedUpgradeStatus.color = new Color(BulletSpeedUpgradeStatus.color.r, BulletSpeedUpgradeStatus.color.g, BulletSpeedUpgradeStatus.color.b, BulletSpeedUpgradeStatus.color.a + 0.2f);

            }

        }
        else
        {
            Debug.Log("Bullet Speed is Fully Upgraded(5).");
        }

this.GetComponent<AudioSource>().PlayOneShot(collectSound);
    }
    public void PickMaxHPUpgrade()
    {
        if (GameManagement.Instance.Max_HP < 200)
        {
            GameManagement.Instance.Max_HP += 20;
            GameManagement.Instance.m_hp += 20.0f;

            if (GameManagement.Instance.Max_HP > 200)
            {
                GameManagement.Instance.Max_HP = 200;
            }


            if (MaxHPUpgradeStatus.color.a < 1)
            {
                MaxHPUpgradeStatus.color = new Color(MaxHPUpgradeStatus.color.r, MaxHPUpgradeStatus.color.g, MaxHPUpgradeStatus.color.b, MaxHPUpgradeStatus.color.a + 0.2f);

            }
        }
        else
        {
            Debug.Log("Max HP is Fully Upgraded(5).");
        }
        this.GetComponent<AudioSource>().PlayOneShot(collectSound);
    }
    public void PickRepairKit()
    {
        if (GameManagement.Instance.m_hp < GameManagement.Instance.Max_HP)
        {
            GameManagement.Instance.m_hp += 10.0f;

            if (GameManagement.Instance.m_hp > GameManagement.Instance.Max_HP)
            {
                GameManagement.Instance.m_hp = GameManagement.Instance.Max_HP;

            }
        }
        this.GetComponent<AudioSource>().PlayOneShot(collectSound);
    }
}
