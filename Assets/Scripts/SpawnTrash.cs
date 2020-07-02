using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnTrash : MonoBehaviour
{

    [SerializeField]
    private PickUpItem[] trash;

    [SerializeField]
    private List<Transform> trashSpawns;
        
    [SerializeField]
    private int totalTrashToSpawn;

    private PickUpItem itemToSpawn;

    private int spawnedTrash = 0;

    private Transform spawn;
    // Start is called before the first frame update

    private void Awake()
    {
        FindObjectOfType<UIManager>().SetTotalTrash(totalTrashToSpawn);
    }
    void Start()
    {
        

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
