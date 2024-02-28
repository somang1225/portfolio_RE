using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
        Debug.Log("아이템 추가");
        itemData = _item;
        itemData.itemCount = _count;
        item_Image.sprite = itemData.itemIcon;

        

        if(itemData.itemType == ItemData.ItemType.Box)
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
        Debug.Log("카운터 추가");
        itemData.itemCount += _count;
        text_Count.text = itemData.itemCount.ToString();

        if(itemData.itemCount <= 0)
        {
            ClearSlot();
        }
    }

    void ClearSlot()
    {
        itemData = null;
        itemData.itemCount = 0;
        item_Image.sprite = null;
        SetColor(0);

        text_Count.text = "0";
    }


    public void Use_Box()
    {
        switch(itemData.itemType)
        {
            case ItemData.ItemType.Box:
                itemData.itemCount--;
                text_Count.text = itemData.itemCount.ToString();
                if(itemData.itemCount <= 0)
                {
                    //gameObject.SetActive(false);
                    itemData = null;
                }
                //GameManager.Instance.inventory.AcquireItem(itemData.eqItemType.);
                break;
        }
    }

}
