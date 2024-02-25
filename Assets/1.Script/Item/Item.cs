using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    public ItemData data;
    public int weapon_level;
    public Weapon weapon;

    

    public GameObject[] slots;

    Image icon;
    Text text_Level;
    Text text_Count;
    Text text_Name;
    Text text_Desc;



    Text test_text;
    Collider2D collider2D;
    

    private void Awake()
    {
        
        icon = GetComponentsInChildren<Image>()[1];
        icon.sprite = data.itemIcon;

        Text[] tests = GetComponentsInChildren<Text>();
        
        //text_Count = texts[0];
        text_Count = tests[0];
        
        collider2D = GetComponent<Collider2D>();
    }

    private void OnEnable() 
    {
        
    }

    
    private void LateUpdate()
    {
        //text.text = "Lv." + (weapon_level + 1);
        text_Count.text = "1234";
    }

    
    public void Get_EqItem()
    {
        switch(data.itemType)
        {
            case ItemData.ItemType.Box:

                break;
        }
    }

    public void Tap_Click()
    {
        


    }
    


    
}
