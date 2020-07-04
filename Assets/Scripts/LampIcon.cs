using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LampIcon : MonoBehaviour
{
    [SerializeField]
    private float rotateSpeed;

    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y + rotateSpeed * Time.deltaTime, 0);
        
    }
}
