using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeFloorCamera : MonoBehaviour
{

    [SerializeField] private GameObject floorToEnable;
    [SerializeField] private GameObject floorToDisable;
    [SerializeField] private GameObject cameraPivot;
    [SerializeField] private float cameraHeight;


    private Transform player;

    private void Start()
    {
        player = FindObjectOfType<PlayerMovement>().transform;
    }

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.GetComponent<PlayerMovement>())
        {
            FindObjectOfType<CameraMovement>().ChangeFloorPivot(cameraHeight);           
            //cameraPivot.transform.position = new Vector3(player.transform.position.x, cameraHeight, player.position.z);

            floorToEnable.SetActive(true);
            floorToDisable.SetActive(false);

            var triggers = Resources.FindObjectsOfTypeAll<ChangeFloorCamera>();
            foreach (var item in triggers)
            {
                item.gameObject.SetActive(true);
            }
            gameObject.SetActive(false);
        }
    }
}
