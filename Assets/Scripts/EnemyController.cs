using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

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


    [SerializeField]
    private Image detectbar1, detectbar2;

    [SerializeField]
    private float detectTimer;

    private float startDetectTimer;

    //private Vector3 previousTarget;

    // Start is called before the first frame update
    void Start()
    {
        startDetectTimer = detectTimer;
        detectTimer = 0;
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

        if (hasPlayer)
        {
            if (detectTimer < startDetectTimer)
            {
                detectTimer += Time.deltaTime;
                print(detectTimer / 1);
                detectbar1.fillAmount = detectTimer / 1;
                detectbar2.fillAmount = detectTimer / 1;
            }
            else if (detectTimer >= startDetectTimer)
            {
                player.LoseHealth();
                detectTimer = 0;
                
            }                   

        }
        else
        {
            detectTimer = 0;
            detectbar1.fillAmount = 0;
            detectbar2.fillAmount = 0;
        }

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
