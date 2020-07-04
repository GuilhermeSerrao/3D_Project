using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LampSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject lamp;

    [SerializeField]
    private int lampsToSpawn;

    // Start is called before the first frame update
    void Start()
    {
        var spawns = GameObject.FindGameObjectsWithTag("LampSpawn").ToList();

        for (int i = 0; i < lampsToSpawn; i++)
        {
            var temp = spawns[Random.Range(0, spawns.Count)];
            Instantiate(lamp, temp.transform.position, Quaternion.identity);

            spawns.Remove(temp);
            
        }
    }


}
