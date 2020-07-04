using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SpawnTrash : MonoBehaviour
{

    [SerializeField]
    private PickUpItem[] trash;

    [SerializeField]
    private GameObject[] puddleDust;

    private List<GameObject> trashSpawns;
        
    [SerializeField]
    private int totalTrashToSpawn, dustPuddleToSpawn;

    private int spawnedTrash = 0;

    // Start is called before the first frame update

    private void Awake()
    {
        FindObjectOfType<UIManager>().SetTotalTrash(totalTrashToSpawn, dustPuddleToSpawn);
    }
    void Start()
    {
        trashSpawns = GameObject.FindGameObjectsWithTag("TrashSpawn").ToList();
        var dustPuddlesArray = GameObject.FindGameObjectsWithTag("DustPuddleSpawn");
        var dustPuddlesList = dustPuddlesArray.ToList();

        for (int i = 0; i < dustPuddleToSpawn ; i++)
        {
            var spawn = dustPuddlesList[Random.Range(0, dustPuddlesList.Count)];

            Instantiate(puddleDust[Random.Range(0, puddleDust.Length)], spawn.transform.position, Quaternion.Euler(0, Random.Range(0, 360), 0));                
            
            dustPuddlesList.Remove(spawn);
        }


        for (int i = 0; i < totalTrashToSpawn; i++)
        {
            var spawn = trashSpawns[Random.Range(0, trashSpawns.Count)];

            Instantiate(trash[Random.Range(0, trash.Length)], spawn.transform.position, Random.rotation);            

            trashSpawns.Remove(spawn);
        }
    }


}
