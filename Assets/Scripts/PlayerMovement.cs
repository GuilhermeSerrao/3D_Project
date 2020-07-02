using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{

    [SerializeField]
    private float moveSpeed;  

    [SerializeField]
    private float rotationSpeed, ghostTimer, ghostCooldownTimer;

    [SerializeField]
    private Transform cam;

    [SerializeField]
    private float gravity;

    [SerializeField]
    private PhysicMaterial groundedMaterial;

    [SerializeField]
    private PhysicMaterial airMaterial;

    [SerializeField]
    private int playerLayer, ghostLayer, lives, ghostUse;

    [SerializeField]
    private Image ghostOverlay;

    private float originalSpeed, ghostStartTimer, ghostCooldownStart;

    private float airSpeed;

    private float distanceGround;

    private bool isGrounded = false;

    private Rigidbody rb;

    private RaycastHit groundHit;

    private bool hidden;

    private bool ghostMode, canTurnGhost, ghostInCooldown = false;

    private UIManager Ui;
    

    [SerializeField]private Collider coll;

    private void Awake()
    {
        //coll = GetComponent<Collider>();
        rb = GetComponent<Rigidbody>();
        distanceGround = GetComponent<Collider>().bounds.extents.y;
    }

    private void Start()
    {      

        ghostCooldownStart = ghostCooldownTimer;
        ghostStartTimer = ghostTimer;
        originalSpeed = moveSpeed;
        airSpeed = moveSpeed / 2;
        Ui = FindObjectOfType<UIManager>();
        Ui.SetLivesUI(lives);
    }

    private void Update()
    {

        if (ghostCooldownTimer > 0 && ghostInCooldown)
        {
            ghostCooldownTimer -= Time.deltaTime;
            canTurnGhost = false;
        }
        else if(ghostCooldownTimer <= 0 && ghostInCooldown)
        {
            ghostInCooldown = false;
            canTurnGhost = true;
            ghostCooldownTimer = ghostCooldownStart;
            
        }

        if (ghostUse > 0 && !ghostInCooldown)
        {
            canTurnGhost = true;
        }
        else
        {
            canTurnGhost = false;
        }

        if (lives <= 0)
        {
            SceneManager.LoadScene("GameOverScreen", LoadSceneMode.Single);
        }

        if (Input.GetKeyDown(KeyCode.LeftShift) && !UIManager.paused)
        {
            var UI = FindObjectOfType<UIManager>();
            if (!ghostMode && canTurnGhost)
            {
                enableGhost();
            }
            else if(ghostMode)
            {
                disableGhost();
            }
        }

        if (!UIManager.paused && ghostMode)
        {
            if (ghostTimer > 0)
            {
                ghostTimer -= Time.deltaTime;
            }
            else
            {
                ghostMode = false;
                disableGhost();
            }            
        }
    }

    private void FixedUpdate()
    {
        if (!hidden && !UIManager.paused)
        {
            
            float verticalAxis = Input.GetAxisRaw("Vertical");
            float horizontalAxis = Input.GetAxisRaw("Horizontal");
            float jumpAxis = Input.GetAxisRaw("Jump");

            Vector3 camf = cam.forward;
            Vector3 camR = cam.right;

            camf.y = 0;
            camR.y = 0;

            camf = camf.normalized;
            camR = camR.normalized;

            Vector3 direction = ((horizontalAxis * camR) + (verticalAxis * camf)).normalized;

            if (direction != Vector3.zero)
            {
                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(direction), rotationSpeed);
            }

            rb.MovePosition(transform.position + direction * (moveSpeed * Time.deltaTime));
        }

    }

    public void Hide(bool hide)
    {
        hidden = hide;
        if (hide)
        {
            FindObjectOfType<EnemyController>().patrol = false;
            transform.GetChild(0).gameObject.SetActive(false);
            
            //transform.localScale = Vector3.zero;
        }
        else
        {
            FindObjectOfType<EnemyController>().patrol = true;
            transform.GetChild(0).gameObject.SetActive(true);
            

            //transform.localScale = Vector3.one;
        }
    }

    public void LoseHealth()
    {
        lives--;
        Ui.SetLivesUI(lives);
    }

    private void enableGhost()
    {
        print("ghost mode");
        ghostUse--;
        ghostMode = true;
        FindObjectOfType<ItemInteraction>().canGrab = false;

        //UI.UpdateUI();
        var objects = FindObjectsOfType<GameObject>();
        foreach (var item in objects)
        {
            if (item.layer == 19)
            {
                item.layer = 12;
            }
        }


        ghostOverlay.gameObject.SetActive(true);
        transform.GetComponent<ItemInteraction>().ReleaseItems(2);

        ghostInCooldown = false;
    }

    private void disableGhost()
    {
        ghostTimer = ghostStartTimer;
        ghostMode = false;
        FindObjectOfType<ItemInteraction>().canGrab = true;

        //UI.UpdateUI();
        var objects = FindObjectsOfType<GameObject>();
        foreach (var item in objects)
        {
            if (item.layer == 12)
            {
                item.layer = 19;
            }
        }

        ghostOverlay.gameObject.SetActive(false);

        ghostInCooldown = true;
    }
}
