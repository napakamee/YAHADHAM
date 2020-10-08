using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundManager : MonoBehaviour
{
    [Space(2)]
    [Header("Nebula Sprites")]
    [SerializeField] GameObject nebulaSprite1;
    [SerializeField] GameObject nebulaSprite2;
    [SerializeField] Vector3 nebulaRespawnPoint;
    [SerializeField] float nebulaSpriteSpeed= 0;
    
   [Space(2)]

    [Header("Planet Sprites")]
    [SerializeField] GameObject[] PlanetSprites;
    [SerializeField] float PlanetsSpawner = 0;
    [SerializeField] float PlanetsRespawnPoint= 0;
    [SerializeField] float PlanetsSpriteSpeed= 0;

    private void Update()
    {
        nebulaSprite1.transform.position += new Vector3(Time.deltaTime * -nebulaSpriteSpeed, 0, 0);
        nebulaSprite2.transform.position += new Vector3(Time.deltaTime * -nebulaSpriteSpeed, 0, 0);
        for (int i = 0; i < PlanetSprites.Length; i++)
        {
            PlanetSprites[i].transform.position += new Vector3(Time.deltaTime * -PlanetsSpriteSpeed, 0, 0);
        }
    }
    private void LateUpdate()
    {

        if (nebulaSprite1.transform.position.x < nebulaRespawnPoint.x)
        {
            nebulaSprite1.transform.position = new Vector3(nebulaSprite2.transform.position.x + nebulaSprite2.GetComponent<Collider2D>().bounds.size.x, 0, 0);
        }
        if (nebulaSprite2.transform.position.x < nebulaRespawnPoint.x)
        {
            nebulaSprite2.transform.position = new Vector3(nebulaSprite1.transform.position.x + nebulaSprite1.GetComponent<Collider2D>().bounds.size.x, 0, 0);
        }
        for (int i = 0; i < PlanetSprites.Length; i++)
        {
            if (PlanetSprites[i].transform.position.x < PlanetsRespawnPoint)
            {
                PlanetSprites[i].transform.position = new Vector3(PlanetsSpawner, PlanetSprites[i].transform.position.y, PlanetSprites[i].transform.position.z);
            }
        }
    }
}
