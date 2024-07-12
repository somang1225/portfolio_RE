using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory_info : MonoBehaviour
{
    public GameObject info_Item;


    [SerializeField]
    Text mytext;
    Inven_Slot info_Item_Data;

    public void WakeUP()
    {   
        info_Item_Data = info_Item.GetComponent<Inven_Slot>();
        Text_Made();
    }

    void Text_Made()
    {
        mytext.text = string.Format(
            " 장비 공격력 {0} + {1}\n 장비 방어력 {2} + {3}\n 장비 이동속도 {4} + {5}" 
            ,info_Item_Data.itemData.eq_power, info_Item_Data.solt_eq_power
            ,info_Item_Data.itemData.eq_defense, info_Item_Data.solt_eq_defense
            ,info_Item_Data.itemData.eq_speed, info_Item_Data.solt_eq_speed
            );
    }


    public void Using_Btn()
    {
        GameManager.Instance.inventory.using_EQ(info_Item_Data.itemData, info_Item);
    }

    public void Using_Upgrade_Btn()
    {
        
    }


    void Upgrade() //장비의 최대 강화 수치 //1:5 6:15 16:22(25)의 수치 
    {
        
    }

}
