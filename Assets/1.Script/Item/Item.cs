using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    public ItemData data;
    public int weapon_level;
    public Weapon weapon;

    Image icon;
    Text text_Level;
    Collider2D collider2D;
    

    private void Awake()
    {
        /*
        icon = GetComponentsInChildren<Image>()[1];
        icon.sprite = data.itemIcon;

        Text[] texts = GetComponentsInChildren<Text>();
        text_Level = texts[0];
        */
        collider2D = GetComponent<Collider2D>();

        
    }


    /*
    private void LateUpdate()
    {
        text_Level.text = "Lv." + (weapon_level + 1);
    }

    
    public void LevelUP_Click()
    {
        switch (data.itemType)
        {
            case ItemData.ItemType.Melee:
            case ItemData.ItemType.Range:

                break;

            case ItemData.ItemType.Glove:
                break;

            case ItemData.ItemType.Shose:
                break;

            case ItemData.ItemType.Heal:
                break;
        }

        weapon_level++;

        if(weapon_level == data.damages.Length)
        {
            GetComponent<Button>().interactable = false;
        }

    }
    */

}
