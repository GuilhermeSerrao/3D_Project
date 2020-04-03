using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{

    [SerializeField]
    private Transform target;

    [SerializeField]
    private float speed;

    private Quaternion rotation;

    bool changeFloor = false;
    private void Update()
    {
        rotation.y += Input.GetAxis("Mouse X");

        transform.rotation = Quaternion.Euler(transform.rotation.x, rotation.y, transform.rotation.z);
    }

    private void FixedUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, new Vector3(target.position.x, transform.position.y, target.position.z), speed*Time.deltaTime);
    }

    public void ChangeFloorPivot()
    {


        //transform.position = Vector3.Lerp(transform.position, new Vector3(target.position.x, targetHeight, target.position.z), 5 * Time.deltaTime);

    }
}
