using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

public class Inventory_info : MonoBehaviour
{
    public GameObject info_Item;

    int[] item_level_point = new int[7] {0, 1, 1, 1, 1, 1, 2};


    [SerializeField]
    Text mytext;
    [SerializeField]
    Text before_Info;
    [SerializeField]
    Text after_Info;
    [SerializeField]
    Text success_Info;
    [SerializeField]
    Text upgrad_cost;

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
        int cur_item_Level = info_Item_Data.itemData.item_Level;

        switch (cur_item_Level)
        {
            case int i when i < 5:
                Upgrade_Info(cur_item_Level);
                break;
            default:
                break;
        }
    }


    void Upgrade_Info(int cur_item_level) //장비의 최대 강화 수치 //1:5 6:15 16:22(25)의 수치 
    {



        before_Info.text = string.Format(
            "+ {0}\n공격력 + {1}\n방어력 + {2}\n이동속도 + {3}"   //0 : 강화수치, 1:공격력 2:방어력 3:이동속도
            , item_level_point[cur_item_level]
            , item_level_point[cur_item_level]
            , item_level_point[cur_item_level]
            , item_level_point[cur_item_level]);

        after_Info.text = string.Format(
            "+ {0}\n공격력 + {1}\n방어력 + {2}\n이동속도 + {3}"   //0 : 강화수치, 1:공격력 2:방어력 3:이동속도
            ,item_level_point[cur_item_level + 1]
            ,item_level_point[cur_item_level + 1]
            ,item_level_point[cur_item_level + 1]
            ,item_level_point[cur_item_level + 1]);
    }

}
