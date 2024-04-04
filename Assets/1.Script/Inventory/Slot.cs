using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
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
    public void AddItem(ItemData _item, int _count = 1)
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
            text_Count.text = "0";
        }

        SetColor(1);
    }

    public void SetSlotCount(int _count)
    {
        itemData.itemCount += _count;
        text_Count.text = itemData.itemCount.ToString();

        if(itemData.itemCount <= 0)
        {
            ClearSlot();
        }
    }

    void ClearSlot()
    {
        itemData.itemCount = 0;
        itemData = null;
        item_Image.sprite = null;
        SetColor(0);
        text_Count.text = "";
        Destroy(gameObject);
        //gameObject.SetActive(false);
    }


    public void Use_Box()
    {
        switch(itemData.itemType)
        {
            //Box using
            case ItemData.ItemType.Box:
                SetSlotCount(-1); //개수 차감
                
                break;

            case ItemData.ItemType.Heal:
                SetSlotCount(-1); //개수 차감
                
                break;
        }
    }

}
