using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField]
    GameObject go_InventoryBase; //인벤 기본 이미지

    [SerializeField]
    GameObject go_SlotsParent; //Slot 부모인 Grid Setting
    
    [SerializeField]
    GameObject eq_slotsParent;

    [SerializeField]
    GameObject slot;

    Inven_Slot[] inven_Slots; //슬룻 배열
    Inven_Slot[] eq_Slots; //장착 슬룻 배열
    

    private void Start() 
    {
        inven_Slots = go_SlotsParent.GetComponentsInChildren<Inven_Slot>();
        //eq_Slots = eq_slotsParent.GetComponentInChildren<Inven_Slot>();
    }


    //아이템 획득시 발생
    public void AcquireItem(ItemData _itemdata, int _count = 1)
    {        
        //장비가 아닐 경우
        if(ItemData.ItemType.Equipment != _itemdata.itemType)
        {
            //슬룻이 없는 경우
            if(inven_Slots.Length == 0)
            {
                Instantiate(slot, go_SlotsParent.transform);
                inven_Slots = go_SlotsParent.GetComponentsInChildren<Inven_Slot>();
            }
            else //슬룻이 있으니 아이템의 존재유무 파악            
            {
                for (int i = 0; i < inven_Slots.Length; i++)
                {
                    if(inven_Slots[i].itemData != null) //null이면 slots[i].itemData.itemName에서 에러 발생 ㅇㅇ
                    {
                        //같은 아이템이 존재확인
                        if(inven_Slots[i].itemData.itemName == _itemdata.itemName)
                        {
                            //Debug.Log("슬룻에 아이템 정보 존재");
                            inven_Slots[i].SetSlotCount(_count);
                            return;
                        }
                    }
                }
                Instantiate(slot, go_SlotsParent.transform);
                inven_Slots = go_SlotsParent.GetComponentsInChildren<Inven_Slot>();
            }
        }
        else
        {
            Instantiate(slot, go_SlotsParent.transform);
            inven_Slots = go_SlotsParent.GetComponentsInChildren<Inven_Slot>();
        }
        
        //일단 작동용 (추후 생성된 슬룻에만 적용하게 변경)
        for(int i = inven_Slots.Length; i > 0; i--)
        {
            if(inven_Slots[i-1].itemData == null)
            {
                Debug.Log("슬룻 생성");
                inven_Slots[i-1].AddItem(_itemdata, _count);
                return;
            }
        }
        
    }


    //텝 클릭
    public void TabClick(ItemData itemData)
    {
        for (int i = 0; i < inven_Slots.Length; i++)
        {
            //slots[i].SetA
        }
    }

}
