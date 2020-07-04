using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{

    [SerializeField]
    private AudioClip ghostSFX;

    [SerializeField]
    private float moveSpeed;  

    [SerializeField]
    private float rotationSpeed, ghostDuration, ghostCooldownTimer;

    [SerializeField]
    private Transform cam;

    [SerializeField]
    private float gravity;

    [SerializeField]
    private PhysicMaterial groundedMaterial;

    [SerializeField]
    private PhysicMaterial airMaterial;

    [SerializeField]
    private int playerLayer, ghostLayer, lives, ghostUse, hiddenLayer;

    [SerializeField]
    private Image ghostOverlay;

    private float ghostStartTimer, ghostCooldownStart;

    private Rigidbody rb;    

    private bool hidden;

    private bool ghostMode;

    private UIManager Ui;
    

    [SerializeField]private Collider coll;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    private void Start()
    {      

        ghostCooldownStart = ghostCooldownTimer;
        ghostCooldownTimer = 0;
        ghostStartTimer = ghostDuration;
        Ui = FindObjectOfType<UIManager>();
        Ui.SetLivesUI(lives);
        Ui.SetGhostUses(ghostUse);
    }

    private void Update()
    {


        if (ghostUse < 2 )
        {
            if (ghostCooldownTimer < ghostCooldownStart)
            {
                ghostCooldownTimer += Time.deltaTime;
                Ui.GhostCooldown(ghostCooldownStart, ghostCooldownTimer);
            }
            else if (ghostCooldownTimer >= ghostCooldownStart)
            {
                ghostUse++;
                Ui.SetGhostUses(ghostUse);
                ghostCooldownTimer = 0;
            }
            
        }

        if (Input.GetKeyDown(KeyCode.LeftShift) && !UIManager.paused && ghostUse > 0)
        {
            
            if (!ghostMode && !hidden)
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
            if (ghostDuration > 0)
            {
                ghostDuration -= Time.deltaTime;
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
            GetComponent<Rigidbody>().velocity = Vector3.zero;
            transform.GetChild(0).GetChild(0).gameObject.SetActive(false);
            gameObject.layer = hiddenLayer;
            
        }
        else
        {
            GetComponent<Rigidbody>().velocity = Vector3.zero;

            gameObject.layer = playerLayer;
            transform.GetChild(0).GetChild(0).gameObject.SetActive(true);
            

        }
    }

    public void LoseHealth()
    {
        lives--;
        Ui.SetLivesUI(lives);
    }

    private void enableGhost()
    {
        GetComponent<AudioSource>().PlayOneShot(ghostSFX);        

        ghostUse--;
        Ui.SetGhostUses(ghostUse);
        ghostMode = true;
        FindObjectOfType<ItemInteraction>().canGrab = false;

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

    }

    private void disableGhost()
    {
        ghostDuration = ghostStartTimer;
        ghostMode = false;
        FindObjectOfType<ItemInteraction>().canGrab = true;

        var objects = FindObjectsOfType<GameObject>();
        foreach (var item in objects)
        {
            if (item.layer == 12)
            {
                item.layer = 19;
            }
        }

        ghostOverlay.gameObject.SetActive(false);

    }
}
