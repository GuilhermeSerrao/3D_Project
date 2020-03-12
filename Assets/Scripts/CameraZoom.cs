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

    private Camera cam;

    // Start is called before the first frame update
    void Start()
    {
        cam = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        float scroll = Input.GetAxisRaw("Mouse ScrollWheel");

        if (scroll > 0)
        {
            cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, cam.orthographicSize -= zoomValue, zoomSpeed * Time.deltaTime);
            cam.transform.position = new Vector3(cam.transform.position.x, cam.transform.position.y - (zoomValue/ zoomPosition), cam.transform.position.z);
        }
        else if (scroll <  0)
        {
            cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, cam.orthographicSize += zoomValue, zoomSpeed * Time.deltaTime);
            cam.transform.position = new Vector3(cam.transform.position.x, cam.transform.position.y + (zoomValue / zoomPosition), cam.transform.position.z);
        }
    }

    
}
