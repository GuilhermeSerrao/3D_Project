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

    [SerializeField]
    private List<Transform> trashSpawns;
        
    [SerializeField]
    private int totalTrashToSpawn, dustPuddleToSpawn;

    private PickUpItem itemToSpawn;

    private int spawnedTrash = 0;

    private Transform spawn;
    // Start is called before the first frame update

    private void Awake()
    {
        FindObjectOfType<UIManager>().SetTotalTrash(totalTrashToSpawn, dustPuddleToSpawn);
    }
    void Start()
    {

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
            itemToSpawn = trash[Random.Range(0, trash.Length)];

            if ((int)itemToSpawn.category == 1)
            {
                foreach (var item in trashSpawns)
                {
                    if (item.tag == "FoodSpawn")
                    {
                        spawn = item;
                        Instantiate(itemToSpawn, item.position, Random.rotation);                        
                        spawnedTrash++;
                        break;
                    }
                }
            }
            else if ((int)itemToSpawn.category == 2)
            {
                foreach (var item in trashSpawns)
                {
                    if (item.tag == "ClothesSpawn")
                    {
                        spawn = item;
                        Instantiate(itemToSpawn, item.position, Random.rotation);
                        spawnedTrash++;
                        break;
                    }
                }
            }
            else if ((int)itemToSpawn.category == 3)
            {
                foreach (var item in trashSpawns)
                {
                    if (item.tag == "BooksSpawn")
                    {
                        spawn = item;
                        Instantiate(itemToSpawn, item.position, Random.rotation);                        
                        spawnedTrash++;
                        break;
                    }
                }
            }
            else if ((int)itemToSpawn.category == 4)
            {
                foreach (var item in trashSpawns)
                {
                    if (item.tag == "ElectronicSpawn")
                    {
                        spawn = item;
                        Instantiate(itemToSpawn, item.position, Random.rotation);                        
                        spawnedTrash++;
                        break;
                    }
                }
            }

            trashSpawns.Remove(spawn);
        }
    }


}
