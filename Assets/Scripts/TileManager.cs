using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour
{
    public GameObject[] prefabTiles;
    private Transform PlayerTransform;
    private float spawnZ = -6.0f;
    private float tileLength = 81.7f;
    private float SaveZone = 85.0f;
    private int amnTileOnScreen = 3;
    private int lastPrefabIndex = 0; 
    private List<GameObject> activeTiles;
    // Start is called before the first frame update
    private void Start()
    {
        activeTiles = new List<GameObject>(); 
        PlayerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        for (int i = 0; i < amnTileOnScreen; i++)
        {
            SpawnTile();
        }
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Quit();
        }
        if (PlayerTransform.position.z-SaveZone > (spawnZ - amnTileOnScreen * tileLength))
        {
            SpawnTile();
            DeletTile();
        }
    }
    void SpawnTile(int prefabIndex = -1)
    {
        GameObject go;
        go = Instantiate(prefabTiles[RandomPrefabIndex()]) as GameObject;
        go.transform.SetParent(transform);
        go.transform.position = Vector3.forward * spawnZ;
        spawnZ += tileLength;
        activeTiles.Add(go);
    }
    private void DeletTile()
    {
        Destroy(activeTiles[0]);
        activeTiles.RemoveAt(0);
    }
    private int RandomPrefabIndex()
    {
        if (prefabTiles.Length <= 1)
        
            return 0;
        int randomIndex = lastPrefabIndex;
        while (randomIndex == lastPrefabIndex)
        {
            randomIndex = Random.Range(0, prefabTiles.Length);
        }
        lastPrefabIndex = randomIndex;
        return randomIndex;
        
    }
    public void Quit()
    {
        Application.Quit();
    }
}
