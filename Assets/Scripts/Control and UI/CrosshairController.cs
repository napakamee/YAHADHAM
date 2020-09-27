using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrosshairController : MonoBehaviour
{
    public Camera cam;
    public GameObject crosshair;
    ShipControl shipControl;
    Touch touch;
    Vector2 touchPos;
    SpriteRenderer spriteRenderer;
    public Sprite ShootingCrosshair;
    public Sprite HookCrosshair;
    public LineRenderer lineRenderer;
    public GameObject HookStart;
    public GrapplingHook grapplingHook;

    void Start()
    {
        shipControl = GameObject.FindObjectOfType<ShipControl>();
        touchPos = cam.ScreenToWorldPoint(touch.position);
        spriteRenderer = this.GetComponent<SpriteRenderer>();
        spriteRenderer.enabled = false;
    }

    void Update()
    {
        if (shipControl.HookAiming) { spriteRenderer.sprite = HookCrosshair; }
        else { spriteRenderer.sprite = ShootingCrosshair; }

        if (Input.touchCount > 0 && !shipControl.HookAiming)
        {
            lineRenderer.positionCount = 0;
            for (int i = 0; i < Input.touchCount; i++)
            {
                touch = Input.GetTouch(i);
                touchPos = cam.ScreenToWorldPoint(touch.position);
                if (touchPos.x > -7)
                {
                    break;
                }
            }

            if (touch.phase == TouchPhase.Moved || touch.phase == TouchPhase.Stationary)
            {
                if (touchPos.x > -7)
                {
                    spriteRenderer.enabled = true;
                    crosshair.transform.position = new Vector2(touchPos.x - 1.5f, touchPos.y);
                    shipControl.IsFiring = true;
                }
                else
                {
                    shipControl.IsFiring = false;
                }
            }
            if (touch.phase == TouchPhase.Ended)
            {
                spriteRenderer.enabled = false;
                shipControl.IsFiring = false;
            }
        }
        else if (Input.touchCount > 0 && shipControl.HookAiming)
        {
            shipControl.IsFiring = false;

            for (int i = 0; i < Input.touchCount; i++)
            {
                touch = Input.GetTouch(i);
                touchPos = cam.ScreenToWorldPoint(touch.position);
                if (touchPos.x > -7)
                {
                    break;
                }
            }

            if (touch.phase == TouchPhase.Began || touch.phase == TouchPhase.Stationary || touch.phase == TouchPhase.Moved)
            {
                if (!grapplingHook.Shot)
                {
                    if (touchPos.x > -7)
                    {
                        spriteRenderer.enabled = true;
                        crosshair.transform.position = new Vector2(touchPos.x - 1.5f, touchPos.y);
                        lineRenderer.positionCount = 2;
                        lineRenderer.SetPosition(0, HookStart.transform.position);
                        lineRenderer.SetPosition(1, crosshair.transform.position);
                    }
                    else
                    {
                        lineRenderer.positionCount = 0;
                    }
                }
            }
            if (touch.phase == TouchPhase.Ended && lineRenderer.positionCount == 2)
            {
                if (!grapplingHook.Shot)
                {
                    grapplingHook.Shoot();
                }
                spriteRenderer.enabled = false;
                lineRenderer.positionCount = 0;
            }
        }
    }
}
