using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoom : MonoBehaviour
{

    [SerializeField]
    private float zoomValue;

    [SerializeField]
    private float zoomSpeed;

    [SerializeField]
    private float zoomPosition;

    private Vector3 direction;

    

    private Camera cam;

    void Start()
    {
        cam = GetComponent<Camera>();
        direction = new Vector3(0, 2.5f, -9) - transform.position;
    }

    void Update()
    {
        float scroll = Input.GetAxisRaw("Mouse ScrollWheel");


        if (cam.orthographicSize > 5.5f)
        {
            cam.orthographicSize = 5.5f;
        }

        if (scroll > 0)
        {
            cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, cam.orthographicSize -= zoomValue, zoomSpeed * Time.deltaTime);
            //cam.transform.position = new Vector3(cam.transform.position.x, cam.transform.position.y - (zoomValue/ zoomPosition), cam.transform.position.z);
        }
        else if (scroll <  0 && cam.orthographicSize < 5.5f)
        {
            cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, cam.orthographicSize += zoomValue, zoomSpeed * Time.deltaTime);
            //cam.transform.position = new Vector3(cam.transform.position.x, cam.transform.position.y + (zoomValue / zoomPosition), cam.transform.position.z);
        }
    }

    
}
