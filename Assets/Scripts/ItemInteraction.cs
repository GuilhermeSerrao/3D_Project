using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UI;

public class ItemInteraction : MonoBehaviour
{

    [SerializeField]
    private float grabRange;

    [SerializeField]
    private GameObject particles;

    private Transform closestObject;

    [SerializeField]
    private LayerMask layerMask;

    [SerializeField]
    private LayerMask trashCansMask;

    private Collider[] hitColliders;

    [SerializeField]
    private Transform rightHand;

    [SerializeField]
    private Transform leftHand;

    [SerializeField]
    private Image leftTrashIcon, rightTrashIcon;

    private PickUpItem pickedItemRight, pickedItemLeft;

    private bool rightHandFree = true, leftHandFree = true, leftClick, rightClick, hidden = false;

    private Transform rightObject, leftObject;

    private PlayerMovement player;

    private CameraMovement cam;

    public bool canGrab = true;

    private GameObject[] trashDropLeft, trashDropRight;

    void Start()
    {
        hitColliders = Physics.OverlapSphere(transform.position, grabRange, layerMask);
        player = FindObjectOfType<PlayerMovement>();
        cam = FindObjectOfType<CameraMovement>();
    }

    
    void Update()
    {
        if (!UIManager.paused && canGrab)
        {        
            if (!hidden)
            {              


                hitColliders = Physics.OverlapSphere(transform.position, grabRange, layerMask);
            

                if (hitColliders.Length != 0)
                {
                    foreach (var item in hitColliders)
                    {
                        if (!item.CompareTag("HidingSpot"))
                        {
                            closestObject = item.transform;

                        }
                    }
                
                }
                else
                {
                    closestObject = null;
                }

                var trashCans = GameObject.FindGameObjectsWithTag("TrashCan");

                if (!leftHandFree || !rightHandFree)
                {

                    foreach (var item in trashCans)
                    {
                        item.gameObject.GetComponent<ParticleSystem>().Play();
                    }
                }
                else
                {

                    foreach (var item in trashCans)
                    {
                        item.gameObject.GetComponent<ParticleSystem>().Stop();
                    }
                }

                if (leftHandFree || rightHandFree)
                {
                    foreach (var item in hitColliders)
                    {
                        if (!item.CompareTag("HidingSpot"))
                        {
                            if (Vector3.Distance(item.transform.position, transform.position) < Vector3.Distance(closestObject.position, transform.position))
                            {
                                closestObject = item.transform;
                            }
                        }

                    }
                }
            

                if (Input.GetKeyDown(KeyCode.F))
                {
                    foreach (var item in hitColliders)
                    {
                        if (item.CompareTag("HidingSpot") && item.transform.GetChild(0).gameObject.activeInHierarchy)
                        {
                            item.transform.GetChild(0).gameObject.SetActive(false);
                            player.transform.parent = item.transform;
                            hidden = true;
                            player.Hide(hidden);
                            cam.ChangeCameraTarget(item.transform);
                            ReleaseItems(2);
                        }
                    }
                }

                //------------------------------Right Hand detection

                if (closestObject && Input.GetMouseButtonDown(1) && rightHandFree)
                {

                    rightHandFree = false;
                    rightClick = true;
                    rightObject = closestObject;
                    pickedItemRight = rightObject.GetComponent<PickUpItem>();

                    if (rightObject.transform.parent != null)
                    {
                        if (rightObject.transform.parent.name == "Pivot")
                        {
                            rightObject.gameObject.layer = 17;
                            rightObject.transform.parent.transform.position = rightHand.position;
                            rightObject.transform.parent.transform.rotation = rightHand.rotation;
                            rightObject.transform.parent.transform.parent = rightHand;
                            rightObject.transform.parent.gameObject.GetComponent<Rigidbody>().isKinematic = true;
                            rightObject.gameObject.GetComponent<Rigidbody>().isKinematic = true;
                        }
                    }
                    else
                    {
                        rightTrashIcon.gameObject.SetActive(true);
                        rightTrashIcon.sprite = rightObject.GetComponent<PickUpItem>().icon;                        
                        rightObject.gameObject.layer = 17;
                        rightObject.transform.position = rightHand.position;
                        rightObject.transform.rotation = rightHand.rotation;
                        rightObject.transform.parent = rightHand;
                        rightObject.gameObject.GetComponent<Rigidbody>().isKinematic = true;
                    }
                }

                if (Input.GetMouseButtonUp(1))
                {
                    rightClick = false;
                }

                if (!rightHandFree && Input.GetMouseButtonDown(1) && !rightClick)
                {

                    ReleaseItems(1);
                }

                //-----------------------------------------------------------------------
                //------------------------------------Left Hand Detection

                if (closestObject && Input.GetMouseButtonDown(0) && leftHandFree)
                {
                    leftHandFree = false;
                    leftClick = true;
                    leftObject = closestObject;
                    pickedItemLeft = leftObject.GetComponent<PickUpItem>();
                    if (leftObject.transform.parent != null)
                    {
                        if (leftObject.transform.parent.name == "Pivot")
                        {
                            leftObject.gameObject.layer = 17;
                            leftObject.transform.parent.transform.position = leftHand.position;
                            leftObject.transform.parent.transform.rotation = leftHand.rotation;
                            leftObject.transform.parent.transform.parent = leftHand;
                            leftObject.transform.parent.gameObject.GetComponent<Rigidbody>().isKinematic = true;
                            leftObject.gameObject.GetComponent<Rigidbody>().isKinematic = true;
                        }
                    }
                    else
                    {
                        leftTrashIcon.gameObject.SetActive(true);
                        leftTrashIcon.sprite = leftObject.GetComponent<PickUpItem>().icon;
                        leftObject.gameObject.layer = 17;
                        leftObject.transform.position = leftHand.position;
                        leftObject.transform.rotation = leftHand.rotation;
                        leftObject.transform.parent = leftHand;
                        leftObject.gameObject.GetComponent<Rigidbody>().isKinematic = true;
                    }

                }

                if (pickedItemLeft != null)
                {
                    if ((int)pickedItemLeft.category == 1)
                    {
                        trashDropLeft = GameObject.FindGameObjectsWithTag("FoodTrash");
                        foreach (var item in trashDropLeft)
                        {
                            print(item.name);
                            item.GetComponent<TrashDrop>().StartParticles();
                        }
                    }
                    else if ((int)pickedItemLeft.category == 2)
                    {
                        trashDropLeft = GameObject.FindGameObjectsWithTag("ClothesTrash");
                        foreach (var item in trashDropLeft)
                        {
                            print(item.name);
                            item.GetComponent<TrashDrop>().StartParticles();
                        }
                    }
                    else if ((int)pickedItemLeft.category == 3)
                    {
                        trashDropLeft = GameObject.FindGameObjectsWithTag("BooksTrash");
                        foreach (var item in trashDropLeft)
                        {
                            print(item.name);
                            item.GetComponent<TrashDrop>().StartParticles();
                        }
                    }
                    else if ((int)pickedItemLeft.category == 4)
                    {
                        trashDropLeft = GameObject.FindGameObjectsWithTag("ElectronicTrash");
                        foreach (var item in trashDropLeft)
                        {
                            print(item.name);
                            item.GetComponent<TrashDrop>().StartParticles();
                        }
                    }
                }
                else
                {
                    trashDropLeft = null;
                }

                if (pickedItemRight != null)
                {
                    if ((int)pickedItemRight.category == 1)
                    {
                        trashDropRight = GameObject.FindGameObjectsWithTag("FoodTrash");
                        foreach (var item in trashDropRight)
                        {
                            item.GetComponent<TrashDrop>().StartParticles();
                        }
                    }
                    else if ((int)pickedItemRight.category == 2)
                    {
                        trashDropRight = GameObject.FindGameObjectsWithTag("ClothesTrash");
                        foreach (var item in trashDropRight)
                        {
                            item.GetComponent<TrashDrop>().StartParticles();
                        }
                    }
                    else if ((int)pickedItemRight.category == 3)
                    {
                        trashDropRight = GameObject.FindGameObjectsWithTag("BooksTrash");
                        foreach (var item in trashDropRight)
                        {
                            item.GetComponent<TrashDrop>().StartParticles();
                        }
                    }
                    else if ((int)pickedItemRight.category == 4)
                    {
                        trashDropRight = GameObject.FindGameObjectsWithTag("ElectronicTrash");
                        foreach (var item in trashDropRight)
                        {
                            item.GetComponent<TrashDrop>().StartParticles();
                        }
                    }
                }
                else
                {
                    trashDropRight = null;
                }

                if (Input.GetMouseButtonUp(0))
                {
                    leftClick = false;
                }

                if (!leftHandFree && Input.GetMouseButtonDown(0) && !leftClick)
                {

                    ReleaseItems(0);
                }
                //------------------------------------------------------------------------------
            }
            else if(hidden)
            {
                player.transform.position = player.transform.parent.transform.position;
                if (Input.GetKeyDown(KeyCode.F))
                {
                    player.transform.parent = null;
                    hidden = false;
                    player.Hide(hidden);
                    cam.ChangeCameraTarget(player.transform);
                }
            }


        }

    }

    public void ReleaseItems(int id)
    {
        var trashCans = Physics.OverlapSphere(transform.position, grabRange, trashCansMask);
        
        if (id == 1 || id == 2 && rightObject != null)
        {           

            if (trashCans.Length != 0 && !rightObject.CompareTag("Tool"))
            {
                foreach (var item in trashCans)
                {
                    if ((int)item.GetComponent<TrashDrop>().category == (int)pickedItemRight.category)
                    {
                        foreach (var drop in trashDropRight)
                        {
                            drop.GetComponent<TrashDrop>().StopParticles();
                        }
                        rightTrashIcon.gameObject.SetActive(false);
                        rightTrashIcon.sprite = null;
                        rightHandFree = true;
                        var tempTrash = rightObject;
                        rightObject = null;
                        Destroy(tempTrash.gameObject);
                        var UI = FindObjectOfType<UIManager>();
                        UI.SetTrashBar();
                        break;
                    }
                }              
               // UI.pickedTrash++;
               // UI.UpdateUI();

            }
            else
            {
                if (rightObject.transform.parent.name == "Pivot")
                {
                    rightObject.gameObject.layer = 16;
                    rightObject.transform.parent.transform.parent = null;
                    rightObject.transform.parent.gameObject.GetComponent<Rigidbody>().isKinematic = false;
                    rightObject.gameObject.GetComponent<Rigidbody>().isKinematic = false;
                }
                else
                {
                    rightTrashIcon.gameObject.SetActive(false);
                    rightTrashIcon.sprite = null;
                    rightObject.gameObject.layer = 16;
                    rightObject.transform.parent = null;
                    rightObject.gameObject.GetComponent<Rigidbody>().isKinematic = false;
                }

                rightHandFree = true;
                rightObject = null;
            }
            pickedItemRight = null;
            
        }

        if (id == 0 || id == 2 && leftObject != null)
        {
            
            if (trashCans.Length != 0 && !leftObject.CompareTag("Tool"))
            {
                foreach (var item in trashCans)
                {
                    if ((int)item.GetComponent<TrashDrop>().category == (int)pickedItemLeft.category)
                    {
                        foreach (var drop in trashDropLeft)
                        {
                            drop.GetComponent<TrashDrop>().StopParticles();
                        }
                        leftTrashIcon.gameObject.SetActive(false);
                        leftTrashIcon.sprite = null;
                        leftHandFree = true;
                        var tempTrash = leftObject;
                        leftObject = null;
                        Destroy(tempTrash.gameObject);
                        var UI = FindObjectOfType<UIManager>();
                        UI.SetTrashBar();
                        print("Teste");
                        break;
                    }
                }
               
               // UI.pickedTrash++;
               // UI.UpdateUI();
            }
            else
            {
                if (leftObject.transform.parent.name == "Pivot")
                {
                    leftObject.gameObject.layer = 16;
                    leftObject.transform.parent.transform.parent = null;
                    leftObject.transform.parent.gameObject.GetComponent<Rigidbody>().isKinematic = false;
                    leftObject.gameObject.GetComponent<Rigidbody>().isKinematic = false;
                }
                else
                {
                    leftTrashIcon.gameObject.SetActive(false);
                    leftTrashIcon.sprite = null;
                    leftObject.gameObject.layer = 16;
                    leftObject.transform.parent = null;
                    leftObject.gameObject.GetComponent<Rigidbody>().isKinematic = false;
                }

                leftHandFree = true;
                leftObject = null;
            }
            pickedItemLeft = null;
        }             
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, grabRange);
    }
}
