using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapplingHook : MonoBehaviour
{
    public GameObject Hook;
    public float HookShotSpeed;
    public Transform HookGunPos;
    public Transform crosshairPos;
    Vector2 Dir;
    bool shot;
    public LineRenderer rope;
    public bool Shot
    {
        get { return shot; }
        set { shot = value; }
    }
    GameObject traget;
    ShipControl shipControl;
    public AudioClip hookSound;
    private void Start()
    {
        rope.positionCount = 0;
        shipControl = GameObject.FindObjectOfType<ShipControl>();
    }
    private void Update()
    {
        if (Shot && traget != null)
        {
            rope.positionCount = 2;
            rope.SetPosition(0, HookGunPos.position);
            rope.SetPosition(1, traget.transform.position);
        }
        else
        {
            rope.positionCount = 0;
        }
    }

    public void Shoot()
    {
        GameObject HookIns = Instantiate(Hook, HookGunPos.position, HookGunPos.transform.rotation * Quaternion.Euler(0, 0, 90));
        HookIns.GetComponent<Rigidbody2D>().AddForce(transform.up * HookShotSpeed);
        traget = HookIns;
        Shot = true;
        this.GetComponent<AudioSource>().PlayOneShot(hookSound);
    }
    public void returned()
    {
        Shot = false;
        shipControl.HookAiming = false;
    }
}
