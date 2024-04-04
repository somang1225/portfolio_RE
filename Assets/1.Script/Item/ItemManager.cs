using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ItemManager : MonoBehaviour
{
    public ItemData data;
    public int weapon_level;
    public Weapon weapon;
    Collider2D collider2D;
    

    private void Awake()
    {   
        collider2D = GetComponent<Collider2D>();
    }

    


    
}
