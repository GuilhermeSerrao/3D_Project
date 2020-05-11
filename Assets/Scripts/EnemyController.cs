using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    [SerializeField]
    private Transform[] targetLocations;

    [SerializeField]
    private NavMeshAgent agent;

    [SerializeField]
    private float range;

    [SerializeField]
    private LayerMask playerLayerMask, wallsLayerMask;

    private PlayerMovement player;

    private bool patrol = true;

    private Vector3 target;

    //private Vector3 previousTarget;

    // Start is called before the first frame update
    void Start()
    {
       
        player = FindObjectOfType<PlayerMovement>();
        target = targetLocations[Random.Range(0, targetLocations.Length)].position;
        agent.SetDestination(target);
    }

    // Update is called once per frame
    void Update()
    {
        

        RaycastHit hit;

        if (Physics.Raycast(transform.position, player.transform.position - transform.position, out hit, playerLayerMask | wallsLayerMask))
        {
            print(hit.transform.gameObject.name);
        }

        if (transform.position.x == target.x && transform.position.z == target.z)
        {
          
            NewTargetLocation();
            
            agent.SetDestination(target);
        }
    }

    private void NewTargetLocation()
    {
        //previousTarget = target;
        target = targetLocations[Random.Range(0, targetLocations.Length)].position;
        
    }
}
