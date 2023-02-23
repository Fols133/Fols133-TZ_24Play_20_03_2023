using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingLevel : MonoBehaviour
{
    [Header("Prefabs")]
    [SerializeField] private GameObject pickupPref;
    [SerializeField] private List<GameObject> wallPrefs = new List<GameObject>();
    [SerializeField] private GameObject platformPref;
        
    [Header("Other Objects")]
    [SerializeField] private GameObject startPlatformObj;
    [SerializeField] private Transform player;

    [Header("Settings building")]
    [SerializeField] private int countSpawnPlatform = 4;
    [SerializeField] private int countSpawnPickupPlatform = 3;
    [SerializeField] private int fieldLimit = 2;


    private float countSpawnPickup;
    private float spawnPos;
    private float platformsLenght;
    private List<GameObject> platformsList = new List<GameObject>();

    void Start()
    {
        platformsList.Add(startPlatformObj);
        platformsLenght = platformPref.transform.localScale.z;
        spawnPos = platformsLenght;
        countSpawnPickup = countSpawnPickupPlatform / 4f;
        for (int i = 0; i < countSpawnPlatform; i++)
        {
            SpawnPlatform();
        }
    }
    
    void Update()
    {
        if ((player.position.z - platformsLenght) > spawnPos - (countSpawnPlatform * platformsLenght))
        {
            SpawnPlatform();
            RemovePlatform();
        }
    }

    public void SpawnPlatform()
    {
        GameObject platform = Instantiate(platformPref, transform.forward * spawnPos, Quaternion.identity);
        spawnPos += platformsLenght;
        platformsList.Add(platform);
        SpawnObject(platform);
    }

    private void RemovePlatform()
    {
        Destroy(platformsList[0]);
        platformsList.RemoveAt(0);
    }

    private void SpawnObject(GameObject platform)
    {
        int indexRandom = 0;
        for (int i = -fieldLimit; i <= fieldLimit; i++)
        {
            indexRandom = Random.Range(0, wallPrefs.Count - 1);
            Vector3 posWall = platform.transform.position + (Vector3.forward * platformsLenght);
            posWall.x = i;
            GameObject wall = Instantiate(wallPrefs[indexRandom], posWall, Quaternion.identity);
            wall.transform.SetParent(platform.transform);
        }

        for (float i = 0.25f; i <= countSpawnPickup; i += 0.25f)
        {
            Vector3 posPickup = platform.transform.position + (Vector3.up * 0.5f) + Vector3.forward * (i * platformsLenght) + (Vector3.right * Random.Range(-fieldLimit, fieldLimit));
            Instantiate(pickupPref, posPickup, Quaternion.identity);
        }
    }
}
