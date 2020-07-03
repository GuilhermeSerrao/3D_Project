using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PickUpItem : MonoBehaviour
{
    public enum categories
    {
        Food = 1,
        Clothes = 2,
        Books = 3,
        Electronics = 4,
        Dust = 5,
        Puddle = 6
    }

    public categories category;

    public Sprite icon;
}
