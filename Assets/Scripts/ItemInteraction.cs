using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ItemInteraction : MonoBehaviour
{

    [SerializeField]
    private float grabRange;   

    private Transform closestObject;

    [SerializeField]
    private LayerMask layerMask;

    private Collider[] hitColliders;

    [SerializeField]
    private Transform rightHand;

    [SerializeField]
    private Transform leftHand;

    private bool rightHandFree = true;

    private bool leftHandFree = true;

    private Transform rightObject;

    private Transform leftObject;

    private bool rightClick;

    private bool leftClick;

    private bool hidden = false;

    private PlayerMovement player;

    private CameraMovement camera;

    void Start()
    {
        hitColliders = Physics.OverlapSphere(transform.position, grabRange, layerMask);
        player = FindObjectOfType<PlayerMovement>();
        camera = FindObjectOfType<CameraMovement>();
    }

    
    void Update()
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

            if (Input.GetKeyDown(KeyCode.F))
            {
                foreach (var item in hitColliders)
                {
                    if (item.CompareTag("HidingSpot") && item.transform.GetChild(0).gameObject.activeInHierarchy)
                    {
                        item.transform.GetChild(0).gameObject.SetActive(false);
                        hidden = true;
                        player.Hide(hidden);
                        camera.ChangeCameraTarget(item.transform);
                        ReleaseItems(2);
                    }
                }
            }

            //------------------------------Right Hand detection

            if (closestObject && Input.GetMouseButtonDown(0) && rightHandFree)
            {

                rightHandFree = false;
                rightClick = true;
                rightObject = closestObject;
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
                    rightObject.gameObject.layer = 17;
                    rightObject.transform.position = rightHand.position;
                    rightObject.transform.rotation = rightHand.rotation;
                    rightObject.transform.parent = rightHand;
                    rightObject.gameObject.GetComponent<Rigidbody>().isKinematic = true;
                }
            }

            if (Input.GetMouseButtonUp(0))
            {
                rightClick = false;
            }

            if (!rightHandFree && Input.GetMouseButtonDown(0) && !rightClick)
            {

                ReleaseItems(0);
            }

            //-----------------------------------------------------------------------
            //------------------------------------Left Hand Detection

            if (closestObject && Input.GetMouseButtonDown(1) && leftHandFree)
            {
                leftHandFree = false;
                leftClick = true;
                leftObject = closestObject;
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
                    leftObject.gameObject.layer = 17;
                    leftObject.transform.position = leftHand.position;
                    leftObject.transform.rotation = leftHand.rotation;
                    leftObject.transform.parent = leftHand;
                    leftObject.gameObject.GetComponent<Rigidbody>().isKinematic = true;
                }

            }

            if (Input.GetMouseButtonUp(1))
            {
                leftClick = false;
            }

            if (!leftHandFree && Input.GetMouseButtonDown(1) && !leftClick)
            {

                ReleaseItems(1);
            }
            //------------------------------------------------------------------------------
        }
        else if(hidden)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                hidden = false;
                player.Hide(hidden);
                camera.ChangeCameraTarget(player.transform);
            }
        }


    }

    private void ReleaseItems(int id)
    {
        if (id == 0 || id == 2)
        {
            if (rightObject)
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
                    rightObject.gameObject.layer = 16;
                    rightObject.transform.parent = null;
                    rightObject.gameObject.GetComponent<Rigidbody>().isKinematic = false;
                }

                rightHandFree = true;
                rightObject = null;
            }
        }

        if (id == 1 || id == 2)
        {
            if (leftObject)
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
                    leftObject.gameObject.layer = 16;
                    leftObject.transform.parent = null;
                    leftObject.gameObject.GetComponent<Rigidbody>().isKinematic = false;
                }

                leftHandFree = true;
                leftObject = null;
            }
        }     

        
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, grabRange);
    }
}
