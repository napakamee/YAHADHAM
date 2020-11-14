using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipCollide : MonoBehaviour
{
    public GameObject explosion;
    public GameObject laserExplode;
    public AudioClip explodeSound;
    private float effectInterval = 0;
    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Bullet"))
        {

            //this.GetComponent<AudioSource>().PlayOneShot(explodeSound);
            GameObject expl = Lean.Pool.LeanPool.Spawn(explosion, transform.position, Quaternion.identity) as GameObject;
            Lean.Pool.LeanPool.Despawn(expl, 1);
            Lean.Pool.LeanPool.Despawn(other.gameObject);
            GameManagement.Instance.m_hp -= 5.0f;
        }

    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("LaserBeam"))
        {
            if (effectInterval >= 0)
            {
                effectInterval -= Time.deltaTime;
            }
            if (effectInterval <= 0)
            {
                GameObject expl = Lean.Pool.LeanPool.Spawn(laserExplode, transform.position, Quaternion.identity) as GameObject;
                Lean.Pool.LeanPool.Despawn(expl, .5f);
                effectInterval = 0.3f;
            }
            //Debug.Log("Laser Hit.");

            GameManagement.Instance.m_hp -= 10 * Time.deltaTime;
        }
    }
}
