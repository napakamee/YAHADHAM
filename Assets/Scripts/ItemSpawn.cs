using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyBox;
public class ItemSpawn : MonoBehaviour
{
    public GameObject[] Item;
    public float respawnTime = 10.0f;
    public bool isRandomTime;
    [ConditionalField(nameof(isRandomTime))] public float maxTime = 20, minTime = 10;
    private Vector2 screenBounds;
    // Start is called before the first frame update
    void Start()
    {
        if (isRandomTime)
        {
            respawnTime = Random.Range(minTime, maxTime);
        }

        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        StartCoroutine(itemWave());
    }

    private void spawnItem()
    {
        if (isRandomTime)
            respawnTime = Random.Range(minTime, maxTime);

        GameObject item = Instantiate(Item[Random.Range(0, Item.Length)]);
        item.transform.position = new Vector2(Random.Range(-screenBounds.x + 10, screenBounds.x - 2), -screenBounds.y * 1.3f);
    }
    IEnumerator itemWave()
    {
        while (true)
        {
            yield return new WaitForSeconds(respawnTime);
            spawnItem();
        }
    }
    void Update()
    {

    }
}
