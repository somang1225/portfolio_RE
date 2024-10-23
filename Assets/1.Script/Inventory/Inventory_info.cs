using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

public class Inventory_info : MonoBehaviour
{
    public GameObject info_Item;

    int[] item_level_point = new int[7] {0, 1, 1, 1, 1, 1, 2};
    int[] item_level_success = new int[7] {95, 90, 85, 80, 75, 70, 65};
    int[] item_level_cost = new int[7] {1, 2, 3, 4, 5, 6, 7};


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
        
        int cur_item_Level = info_Item_Data.solt_eq_level;

        switch (cur_item_Level)
        {
            case int i when i < 5:
                Upgrade_Info(cur_item_Level);
                break;
            default:
                break;
        }
    }

    public void Upgrade_star(int cur_item_Level, int cur_item_Data)
    {
        /* 0:Helmet 1:Armor 2:Glove 3:Boot */
        switch(info_Item_Data.itemData.itemID / 100)
        {
            case 1:
                info_Item_Data.solt_eq_defense += item_level_point[cur_item_Level];
                break;

            case 2:
                info_Item_Data.solt_eq_defense += item_level_point[cur_item_Level];
                break;

            case 3:
                info_Item_Data.solt_eq_power += item_level_point[cur_item_Level];
                break;
                
            case 4:
                info_Item_Data.solt_eq_speed += item_level_point[cur_item_Level];
                break;
        }
    }

    public void Upgrade_Item()
    {
        if(GameManager.Instance.gold - item_level_cost[info_Item_Data.solt_eq_level] < 0)
        {
            UnityEngine.Debug.Log("돈 없으니까 돈 모아오세요");
            return;
        }
        else
        {
            GameManager.Instance.gold -= item_level_cost[info_Item_Data.solt_eq_level];
        }
        

        int item_ran = Random.Range(0, 100);

        if (item_ran > item_level_success[info_Item_Data.solt_eq_level])
        {
            UnityEngine.Debug.Log("실패");
        }
        else if(item_ran < item_level_success[info_Item_Data.solt_eq_level])
        {
            UnityEngine.Debug.Log("성공");

            info_Item_Data.solt_eq_level++;
            Upgrade_star(info_Item_Data.solt_eq_level, info_Item_Data.itemData.itemID);
            Upgrade_Info(info_Item_Data.solt_eq_level);
            Text_Made();
            info_Item_Data.AddItem(info_Item_Data.itemData);
            
        }
    }


    void Upgrade_Info(int cur_item_level) //장비의 최대 강화 수치 //1:5 6:15 16:22(25)의 수치 
    {
        before_Info.text = string.Format(
            "before\n+ {0}\n공격력 + {1}\n방어력 + {2}\n이동속도 + {3}"   //0 : 강화수치, 1:공격력 2:방어력 3:이동속도
            , cur_item_level
            , info_Item_Data.solt_eq_power
            , info_Item_Data.solt_eq_defense
            , info_Item_Data.solt_eq_speed);

        /* 0:Helmet 1:Armor 2:Glove 3:Boot */
        switch(info_Item_Data.itemData.itemID / 100)
        {
            case 1:
            after_Info.text = string.Format(
                "after\n+ {0}\n공격력 + {1}\n방어력 + {2}\n이동속도 + {3}"   //0 : 강화수치, 1:공격력 2:방어력 3:이동속도
                ,cur_item_level + 1
                ,info_Item_Data.solt_eq_power  //+ item_level_point[cur_item_level + 1]
                ,info_Item_Data.solt_eq_defense + item_level_point[cur_item_level + 1]
                ,info_Item_Data.solt_eq_speed);  //+ item_level_point[cur_item_level + 1]);
                break;

            case 2:
            after_Info.text = string.Format(
                "after\n+ {0}\n공격력 + {1}\n방어력 + {2}\n이동속도 + {3}"   //0 : 강화수치, 1:공격력 2:방어력 3:이동속도
                ,cur_item_level + 1
                ,info_Item_Data.solt_eq_power  //+ item_level_point[cur_item_level + 1]
                ,info_Item_Data.solt_eq_defense + item_level_point[cur_item_level + 1]
                ,info_Item_Data.solt_eq_speed);  //+ item_level_point[cur_item_level + 1]);
                break;

            case 3:
            after_Info.text = string.Format(
                "after\n+ {0}\n공격력 + {1}\n방어력 + {2}\n이동속도 + {3}"   //0 : 강화수치, 1:공격력 2:방어력 3:이동속도
                ,cur_item_level + 1
                ,info_Item_Data.solt_eq_power + item_level_point[cur_item_level + 1]
                ,info_Item_Data.solt_eq_defense // + item_level_point[cur_item_level + 1]
                ,info_Item_Data.solt_eq_speed);  //+ item_level_point[cur_item_level + 1]);
                break;
                
            case 4:
            after_Info.text = string.Format(
                "after\n+ {0}\n공격력 + {1}\n방어력 + {2}\n이동속도 + {3}"   //0 : 강화수치, 1:공격력 2:방어력 3:이동속도
                ,cur_item_level + 1
                ,info_Item_Data.solt_eq_power  //+ item_level_point[cur_item_level + 1]
                ,info_Item_Data.solt_eq_defense // + item_level_point[cur_item_level + 1]
                ,info_Item_Data.solt_eq_speed + item_level_point[cur_item_level + 1]);
                break;
        }

        


        success_Info.text = string.Format(
            "성공확률 : {0}%"
            ,item_level_success[cur_item_level]
        );

        upgrad_cost.text = string.Format(
            "강화비용 : {0}골드"
            ,item_level_cost[cur_item_level]
        );


    }

}
