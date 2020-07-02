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

    private bool changeFloor = false;

    private float targetHeight;
    private void Update()
    {
        if (changeFloor)
        {
            transform.position = Vector3.Lerp(transform.position, new Vector3(target.position.x, targetHeight, target.position.z), 5 * Time.deltaTime);
        }
        else if (transform.position.y == targetHeight)
        {
            changeFloor = false;
        }

        if (!UIManager.paused)
        {        

        rotation.y += Input.GetAxis("Mouse X");

        transform.rotation = Quaternion.Euler(transform.rotation.x, rotation.y, transform.rotation.z);

        }
    }

    private void FixedUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, new Vector3(target.position.x, transform.position.y, target.position.z), speed*Time.deltaTime);
    }

    public void ChangeFloorPivot(float height)
    {
        changeFloor = true;
        targetHeight = height;

    }

    public void ChangeCameraTarget(Transform newTarget)
    {
        target = newTarget;
    }
}
