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
    GameObject slot;

    Slot[] slots; //슬룻 배열

    private void Start() 
    {
        slots = go_SlotsParent.GetComponentsInChildren<Slot>();
    }


    public void AcquireItem(ItemData _itemdata, int _count = 1)
    {

        Debug.Log("획득한 아이템 타입 : " + _itemdata.itemType);
        
        //장비가 아닐 경우
        if(ItemData.ItemType.Equipment != _itemdata.itemType)
        {
            //슬룻이 존재 할 경우
            if(slots.Length != 0)
            {
                for (int i = 0; i < slots.Length; i++)
                {
                    if(slots[i].itemData != null) //null이면 slots[i].itemData.itemName에서 에러 발생 ㅇㅇ
                    {
                        Debug.Log("이미 슬룻이 존재하는지 확인");
                        if(slots[i].itemData.itemName == _itemdata.itemName)
                        {
                            Debug.Log("슬룻에 아이템 정보 존재");
                            slots[i].SetSlotCount(_count);
                            return;
                        } 
                    }
                }
            }
            else
            {
                Instantiate(slot, go_SlotsParent.transform);
                slots = go_SlotsParent.GetComponentsInChildren<Slot>();
            }
            

        }
        
        //일단 작동용 (추후 생성된 슬룻에만 적용하게 변경)
        for(int i = 0; i < slots.Length; i++)
        {

            if(slots[i].itemData == null)
            {
                Debug.Log("null인 경우");
                slots[i].AddItem(_itemdata, _count);
                return;
            }
        }
        
    }


    public void Use_Box()
    {
        //Debug.Log(gameObject.name);
    }



}
