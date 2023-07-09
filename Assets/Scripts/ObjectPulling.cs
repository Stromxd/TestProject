using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPulling : MonoBehaviour
{
    public GameObject[] tilePrefabs;

    public Transform playerTransform;
    private float spawnZ = 4500.56f;
    private float tileLength = 2.0f;
    private int amtOfTilesOnScreen = 2;

    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        for (int i = 0; i < amtOfTilesOnScreen; i++)
        {
            SpawnTile(0);
        }
    }

    void Update()
    {
        if (playerTransform.position.z > (spawnZ - amtOfTilesOnScreen * tileLength))
        {
            SpawnTile(0);
        }
    }

    private void SpawnTile(int prefabIndx = 0)
    {
        GameObject go;
        go = Instantiate(tilePrefabs[prefabIndx]) as GameObject;
        go.transform.SetParent(transform);
        go.transform.position = Vector3.forward * spawnZ;
        spawnZ += tileLength;
    }
}

