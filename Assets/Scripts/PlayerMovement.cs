using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    [SerializeField]
    private float moveSpeed;   

    [SerializeField]
    private float jumpForce;

    [SerializeField]
    private Transform camera;

    [SerializeField]
    private PhysicMaterial groundedMaterial;

    [SerializeField]
    private PhysicMaterial airMaterial;

    private float originalSpeed;

    private float airSpeed;

    private float distanceGround;

    private bool isGrounded = false;

    private Rigidbody rb;

    private void Awake()
    {        
        rb = GetComponent<Rigidbody>();
        distanceGround = GetComponent<Collider>().bounds.extents.y;
    }

    private void Start()
    {
        originalSpeed = moveSpeed;
        airSpeed = moveSpeed / 2;
    }

    private void FixedUpdate()
    {
        float verticalAxis = Input.GetAxisRaw("Vertical");
        float horizontalAxis = Input.GetAxisRaw("Horizontal");
        float jumpAxis = Input.GetAxisRaw("Jump");       

        if (!Physics.Raycast(transform.position, -Vector3.up, distanceGround + 0.5f))
        {
            isGrounded = false;
            moveSpeed = airSpeed;
            GetComponent<Collider>().material = airMaterial;

        }
        else
        {
            isGrounded = true;
            moveSpeed = originalSpeed;
            GetComponent<Collider>().material = groundedMaterial;
        }

        Vector3 camf = camera.forward;
        Vector3 camR = camera.right;

        camf.y = 0;
        camR.y = 0;

        camf = camf.normalized;
        camR = camR.normalized;

        Vector3 direction = ((horizontalAxis * camR) + (verticalAxis * camf)).normalized;

        rb.MovePosition(transform.position + direction * (moveSpeed * Time.deltaTime));

        if (isGrounded && jumpAxis != 0)
        {
            rb.AddForce(new Vector3(rb.velocity.x, jumpAxis * jumpForce, rb.velocity.z), ForceMode.Force);
        }
    }
}
