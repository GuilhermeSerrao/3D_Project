using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    [SerializeField]
    private FieldOfView fieldOfView;

    [SerializeField]
    private Transform[] targetLocations;

    [SerializeField]
    private NavMeshAgent agent;

    private UIManager UI;

    private PlayerMovement player;

    public bool patrol = true;

    private Vector3 target = Vector3.zero;



    private bool hasPlayer;

    //private Vector3 previousTarget;

    // Start is called before the first frame update
    void Start()
    {
        UI = FindObjectOfType<UIManager>();
        player = FindObjectOfType<PlayerMovement>();
        target = targetLocations[Random.Range(0, targetLocations.Length)].position;
        agent.SetDestination(target);
    }

    // Update is called once per frame
    void Update()
    {
        fieldOfView.SetAimDirection(-transform.right);
        fieldOfView.SetOrigin(transform.position); 
 


        if (Mathf.Approximately(transform.position.x, target.x) && Mathf.Approximately(transform.position.z, target.z))
        {
            NewTargetLocation();

            SetDestination(target);
        }     
        
        
    }

    private void NewTargetLocation()
    {
        var prevTarget = target;
        target = targetLocations[Random.Range(0, targetLocations.Length)].position;
       
        
    }

    private void SetDestination(Vector3 pos)
    {
        
        agent.SetDestination(pos);

    }

    public void SetPlayer(bool player)
    {
        hasPlayer = player;
    }
}
