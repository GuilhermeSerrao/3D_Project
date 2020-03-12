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
    private float gravity;

    [SerializeField]
    private PhysicMaterial groundedMaterial;

    [SerializeField]
    private PhysicMaterial airMaterial;

    private float originalSpeed;

    private float airSpeed;

    private float distanceGround;

    private bool isGrounded = false;

    private Rigidbody rb;

    private RaycastHit groundHit;

    private Collider coll;

    private void Awake()
    {
        coll = GetComponent<Collider>();
        rb = GetComponent<Rigidbody>();
        distanceGround = GetComponent<Collider>().bounds.extents.y;
    }

    private void Start()
    {
        originalSpeed = moveSpeed;
        airSpeed = moveSpeed / 2;
    }

    private void Update()
    {

        if (Physics.CapsuleCast(transform.position + Vector3.up * 0.5f, transform.position, 0.4f, transform.TransformDirection(Vector3.down), out groundHit, Mathf.Infinity))
        {
            Debug.Log(groundHit.distance);
            if (groundHit.distance <= 0.7f)
            {
                isGrounded = true;
                coll.material = groundedMaterial;                
                moveSpeed = originalSpeed;
            }
            else
            {
                isGrounded = false;
                coll.material = airMaterial;
                moveSpeed = airSpeed;
            }
                
        }

    }

    private void FixedUpdate()
    {
        float verticalAxis = Input.GetAxisRaw("Vertical");
        float horizontalAxis = Input.GetAxisRaw("Horizontal");
        float jumpAxis = Input.GetAxisRaw("Jump");       

       

        Vector3 camf = camera.forward;
        Vector3 camR = camera.right;

        camf.y = 0;
        camR.y = 0;

        camf = camf.normalized;
        camR = camR.normalized;

        Vector3 direction = ((horizontalAxis * camR) + (verticalAxis * camf)).normalized;

        rb.MovePosition(transform.position + direction * (moveSpeed * Time.deltaTime));

        if (!isGrounded)
        {
            rb.AddForce(Vector3.down * gravity);
        }

        if (isGrounded && jumpAxis != 0 && rb.velocity.y <= 0)
        {
            rb.velocity = new Vector3(rb.velocity.x, jumpAxis * jumpForce, rb.velocity.z);
            //rb.AddForce(new Vector3(rb.velocity.x, jumpAxis * jumpForce, rb.velocity.z), ForceMode.VelocityChange);
        }
    }
}
