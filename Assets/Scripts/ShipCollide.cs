using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipCollide : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D other){
        if (other.gameObject.CompareTag("Bullet") || other.gameObject.CompareTag("Enemy")){
        Destroy(other.gameObject);
        GameManagement.Instance.m_hp -= 5.0f;
        }
    }
}
