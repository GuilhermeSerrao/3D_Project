using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToClean : MonoBehaviour
{
    private ItemInteraction playerItem;
    public enum categories
    {
        Dust = 5,
        Puddle = 6
    }

    public categories category;

    private void Start()
    {
        playerItem = FindObjectOfType<ItemInteraction>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerMovement>())
        {
            
            if ((int)category==5 && playerItem.hasBroom)
            {
                Clean();
                
            }
            else if((int)category==6 && playerItem.hasMop)
            {
                Clean();
                
            }
        }
    }

    private void Clean()
    {
        playerItem.PlayTrashSound();
        FindObjectOfType<UIManager>().SetTrashBar();
        Destroy(gameObject);
    }

}
