using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashDrop : MonoBehaviour
{

    public enum categories
    {
        Food = 1,
        Clothes = 2,
        Books = 3,
        Electronics = 4
    }

    public categories category;

    private GameObject particles;

    private void Start()
    {
        if (transform.childCount > 0)
        {
            particles = transform.GetChild(0).gameObject;
            particles.SetActive(false);
            particles.transform.position = transform.position;
        }
        
    }

    public void StartParticles()
    {
        particles.SetActive(true);
    }

    public void StopParticles()
    {
        particles.SetActive(false);
    }
}
