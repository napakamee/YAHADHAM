using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemScript : MonoBehaviour
{
    public int itemType;
    private ShipControl shipControl;

    private void Awake()
    {
        shipControl = GameObject.FindObjectOfType<ShipControl>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            switch (itemType)
            {
                case 0: Debug.Log("Damage Upgrade Picked up"); shipControl.PickDamageUpgrade(); break;
                case 1: Debug.Log("Fire Rate Upgrade Picked up"); shipControl.PickFireRateUpgrade();break;
                case 2: Debug.Log("Bullet Speed Upgrade Picked up"); shipControl.PickBulletSpeedUpgrade();break;
                case 3: Debug.Log("Max HP Upgrade Picked up"); shipControl.PickMaxHPUpgrade();break;
                case 4: Debug.Log("Repair Kit Picked up"); shipControl.PickRepairKit();break;
                default: break;
            }
            Destroy(this.gameObject);
        }
    }
}
