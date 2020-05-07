using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerItemInteraction : MonoBehaviour
{

    [SerializeField]
    private Transform leftHand, rightHand;

    [SerializeField]
    private Rigidbody leftJoint, rightJoint;

    private bool leftHandFree = true, rightHandFree = true;



    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("PickUpTrash") && Input.GetKeyDown(KeyCode.E) && (rightHand || leftHand))
        {
            other.gameObject.layer = 14;
            var rbody = other.GetComponent<Rigidbody>();
            rbody.isKinematic = true;
            rbody.useGravity = false;

            FixedJoint fixedJoint = other.gameObject.AddComponent<FixedJoint>();


            if (rightHandFree)
            {
                rightHandFree = false;
                other.transform.position = rightHand.position;
                other.transform.parent = rightHand;
                fixedJoint.anchor = rightHand.position;
                fixedJoint.connectedAnchor = rightJoint.position;

            }
            else if (leftHandFree)
            {
                leftHandFree = false;
                other.transform.position = leftHand.position;
                other.transform.parent = leftHand;
                fixedJoint.anchor = leftHand.position;
                fixedJoint.connectedAnchor = leftJoint.position;
            }
        }

        if (other.CompareTag("Tool") && Input.GetKeyDown(KeyCode.E) && (rightHand || leftHand))
        {
            var rbody = other.GetComponent<Rigidbody>();
            rbody.isKinematic = true;
            rbody.useGravity = false;

            FixedJoint fixedJoint = other.gameObject.AddComponent<FixedJoint>();            
           

            if (rightHandFree)
            {
                rightHandFree = false;
                other.transform.position = rightHand.position;
                other.transform.parent = rightHand;
                fixedJoint.anchor = rightHand.position;
                fixedJoint.connectedAnchor = rightJoint.position;
                
            }
        }
    }

 
}
