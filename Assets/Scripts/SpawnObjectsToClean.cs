using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SpawnObjectsToClean : MonoBehaviour
{
    
    private List<GameObject> liquidSpawnLocations, trashSpawnLocations;

    [SerializeField]
    private GameObject[] liquidsToSpawn, trashToSpawn;

    [SerializeField]
    private int maxLiquidsSpawned, maxTrashSpawned;

    private int liquidsSpawned = 0, trashSpawned = 0;

    private void Awake()
    {
        liquidSpawnLocations = GameObject.FindGameObjectsWithTag("LiquidSpawn").ToList();
        trashSpawnLocations = GameObject.FindGameObjectsWithTag("TrashSpawn").ToList();
    }
    void Start()
    {
        for (liquidsSpawned = 0; liquidsSpawned < maxLiquidsSpawned; liquidsSpawned++)
        {
            var location = Random.Range(0, liquidSpawnLocations.Count);
            Instantiate(liquidsToSpawn[Random.Range(0, liquidsToSpawn.Length)], liquidSpawnLocations[location].transform);
            liquidSpawnLocations.Remove(liquidSpawnLocations[location]);
        }

        for (trashSpawned = 0; trashSpawned < maxTrashSpawned; trashSpawned++)
        {
            var location = Random.Range(0, trashSpawnLocations.Count);
            Instantiate(trashToSpawn[Random.Range(0, trashToSpawn.Length)], trashSpawnLocations[location].transform);
            trashSpawnLocations.Remove(trashSpawnLocations[location]);
        }

    }

    void Update()
    {
        
    }
}
