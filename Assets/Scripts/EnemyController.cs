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

    [SerializeField]
    private float range;

    [SerializeField]
    private LayerMask playerLayerMask, wallsLayerMask;

    [SerializeField]
    private float detectionTimer;

    private float tempTimer;

    private UIManager UI;

    private PlayerMovement player;

    public bool patrol = true;

    private Vector3 target;

    private bool canTakeLive;

    

    

    //private Vector3 previousTarget;

    // Start is called before the first frame update
    void Start()
    {
        tempTimer = detectionTimer;
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
        if (!canTakeLive)
        {
            detectionTimer -= Time.deltaTime;
            if (detectionTimer <= 0)
            {
                canTakeLive = true;
                detectionTimer = tempTimer;
            }
        }       
        

        RaycastHit hit;

        if (patrol && canTakeLive)
        {
            /*if (Physics.Raycast(transform.position, player.transform.position - transform.position, out hit, playerLayerMask | wallsLayerMask))
            {
                
                if (hit.transform.GetComponent<PlayerMovement>())
                {
                    UI.ReduceLives();
                    canTakeLive = false;
                    target = player.transform.position;
                    SetDestination(target);
                }
                else
                {
                    SetDestination(target);
                }
            }*/

            if (transform.position.x == target.x && transform.position.z == target.z)
            {

                NewTargetLocation();

                SetDestination(target);
            }
        }
        else
        {
            if (transform.position.x == target.x && transform.position.z == target.z)
            {

                NewTargetLocation();

                SetDestination(target);
            }
        }
        
    }

    private void NewTargetLocation()
    {
        //previousTarget = target;
        target = targetLocations[Random.Range(0, targetLocations.Length)].position;
        
    }

    private void SetDestination(Vector3 pos)
    {
        agent.SetDestination(pos);
    }
}
