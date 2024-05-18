using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class Inven_Slot : MonoBehaviour
{

    public int solt_eq_defense;
    public int solt_eq_speed;
    public int solt_eq_power;

    public ItemData itemData; //획득한 아이템 정보
    public Image item_Image;

    [SerializeField]
    Text text_Count;


    //아이템 이미지 투명도 조절
    void SetColor(float _alpha)
    {
        Color color = item_Image.color;
        color.a = _alpha;
        item_Image.color = color;
    }

    //아이템 슬룻 추가
    public void AddItem(ItemData _item, int _count = 1) //아이템 추가
    {
        itemData = _item;
        itemData.itemCount = _count;
        item_Image.sprite = itemData.itemIcon;

        

        if(itemData.itemType != ItemData.ItemType.Equipment)
        {
            text_Count.text = itemData.itemCount.ToString();
        } 
        else
        {
            text_Count.text = " ";

            //장비 아이템의 데이터를 받는다
            solt_eq_defense = itemData.eq_defense;
            solt_eq_speed = itemData.eq_speed;
            solt_eq_power = itemData.eq_power;
        
        }

        SetColor(1);
    }

    public void SetSlotCount(int _count) //개수 카운트
    {
        itemData.itemCount += _count;
        text_Count.text = itemData.itemCount.ToString();

        if(itemData.itemCount <= 0)
        {
            ClearSlot();
        }
    }

    void ClearSlot() //슬룻 초기화
    {
        itemData.itemCount = 0;
        itemData = null;
        item_Image.sprite = null;
        SetColor(0);
        text_Count.text = "";
        Destroy(gameObject);
        //gameObject.SetActive(false);
    }


    public void Clikc_slot() //슬룻 클릭
    {
        switch(itemData.itemType)
        {
            //Box using
            case ItemData.ItemType.Box:
                int item_ran = Random.Range(0, 4); /* 0:Helmet 1:Armor 2:Glove 3:Boot */
                
                //GameManager.Instance.inventory.AcquireItem(itemData.eqitem_data_rare[item_ran]);
                
                GameManager.Instance.inventory.AcquireItem(itemData.eqitem_data_nomal[item_ran]);
                SetSlotCount(-1); //개수 차감
                
                break;

            case ItemData.ItemType.Heal:
                SetSlotCount(-1); //개수 차감
                break;

            case ItemData.ItemType.Equipment:

                    GameManager.Instance.inventory.using_EQ(itemData, gameObject);
                    SetSlotCount(-1); //개수 차감
                
                break;    
        }
    }

}
