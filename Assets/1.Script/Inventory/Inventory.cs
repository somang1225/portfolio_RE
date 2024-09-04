using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    [SerializeField]
    GameObject go_InventoryBase; //인벤 기본 이미지

    [SerializeField]
    GameObject go_SlotsParent; //Slot 부모인 Grid Setting

    [SerializeField]
    GameObject go_tab_bottens;
    
    [SerializeField]
    GameObject go_EqSlotsParent;

    [SerializeField]
    GameObject slot;

    [SerializeField]
    GameObject item_Info_Parent;
    
    
    public ItemData tapDataType;

    

    Inven_Slot[] inven_Slots; //슬룻 배열
    Inven_Slot[] eq_Slots; //장착 슬룻 배열 
    Inven_Slot info_Slot; //정보창에 표기될 아이템 슬릇


    Color click_tab;
    Color nonclick_tab;

    private void Start() 
    {
        inven_Slots = go_SlotsParent.GetComponentsInChildren<Inven_Slot>(true);
        eq_Slots = go_EqSlotsParent.GetComponentsInChildren<Inven_Slot>(true);
        info_Slot = item_Info_Parent.GetComponentInChildren<Inven_Slot>(true);

        tapDataType.itemType = ItemData.ItemType.Equipment;

        UnityEngine.ColorUtility.TryParseHtmlString("#809A9D", out click_tab);
        UnityEngine.ColorUtility.TryParseHtmlString("#E7E7E7", out nonclick_tab);
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
                inven_Slots = go_SlotsParent.GetComponentsInChildren<Inven_Slot>(true);
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
                //슬룻이 있으나 중복된게 없을 경우
                Instantiate(slot, go_SlotsParent.transform);
                inven_Slots = go_SlotsParent.GetComponentsInChildren<Inven_Slot>(true);
            }
        }
        else //장비일 경우
        {
            Instantiate(slot, go_SlotsParent.transform);
            inven_Slots = go_SlotsParent.GetComponentsInChildren<Inven_Slot>(true);
        }
        
        //일단 작동용 (추후 생성된 슬룻에만 적용하게 변경)
        for(int i = inven_Slots.Length; i > 0; i--)
        {
            if(inven_Slots[i-1].itemData == null)
            {
                
                inven_Slots[i-1].AddItem(_itemdata, _count);

                if(inven_Slots[i-1].itemData.itemType != tapDataType.itemType)
                {
                    inven_Slots[i-1].gameObject.SetActive(false);
                }


                return;
            }
        }
        
    }

    public void using_EQ(ItemData _itemdata, GameObject click_gameObject)  /* 0:Helmet 1:Armor 2:Glove 3:Boot */
    {   
        int slotnum = (_itemdata.itemID / 100) -1; //장착 아이템의 자리 찾기
        
        //이미 장착한 장비가 있을 경우
        if(eq_Slots[slotnum].itemData != null)
        {
            Debug.Log("이미 장착한 장비가 존재합니다");
            
            //기존에 착용한 장비의 사용중을 false로 수정
            eq_Slots[slotnum].usingEq.GetComponent<Inven_Slot>().isusing = false;
            eq_Slots[slotnum].usingEq.SetActive(true);

            //AcquireItem(_itemdata);
        }
        

        eq_Slots[slotnum].itemData = _itemdata;
        eq_Slots[slotnum].usingEq = click_gameObject;
        eq_Slots[slotnum].AddItem(_itemdata);

        Update_EQ_State();

        click_gameObject.GetComponent<Inven_Slot>().isusing = true;

        click_gameObject.SetActive(false);
    }


    public void Update_EQ_State()
    {
        int eq_defense = 0;
        int eq_power = 0;
        int eq_speed = 0;

        for (int i = 0; i < eq_Slots.Length; i++) //장비창의 장비를 확인하여 능력치 변경
        {
            if(eq_Slots[i].usingEq == null)
            {
                continue;
            }
            eq_defense += eq_Slots[i].solt_eq_defense + eq_Slots[i].itemData.eq_defense;
            eq_power += eq_Slots[i].solt_eq_power + eq_Slots[i].itemData.eq_power;
            eq_speed += eq_Slots[i].solt_eq_speed + eq_Slots[i].itemData.eq_speed;
        }

        GameManager.Instance.eq_defense = eq_defense;
        GameManager.Instance.eq_power = eq_power;
        GameManager.Instance.eq_speed = eq_speed;
    }


 //텝 클릭
    public void TabClick(ItemData clickData)
    {
        int tabnum = 0;

        if(tapDataType.itemType != clickData.itemType)
        {
            tapDataType.itemType = clickData.itemType;
        }

        switch (tapDataType.itemType)
        {
            case ItemData.ItemType.Equipment:
                tabnum = 0;
                break;

            case ItemData.ItemType.Box:
                tabnum = 1;
                break;

            case ItemData.ItemType.Heal:
                tabnum = 2;
                break;
        }

        for (int i = 0; i < 3; i++)
        {
            go_tab_bottens.transform.GetChild(i).GetComponent<Image>().color = i == tabnum ? click_tab:nonclick_tab;
        }

        for (int i = 0; i < inven_Slots.Length; i++)
        {   
            
            //슬릇의 아이템 타입과 텝의 아이템 타입 일치 && 사용중X
            if(go_SlotsParent.transform.GetChild(i).GetComponent<Inven_Slot>().itemData.itemType == tapDataType.itemType && go_SlotsParent.transform.GetChild(i).GetComponent<Inven_Slot>().isusing == false)
            {
                //아이템의 개수가 0이상이야 보인다
                if(inven_Slots[i].itemData.itemCount > 0 || inven_Slots[i].itemData.itemType == ItemData.ItemType.Equipment)
                {
                    go_SlotsParent.transform.GetChild(i).gameObject.SetActive(true);
                }
            }
            else if(inven_Slots[i].itemData.itemType != tapDataType.itemType)
            {
                inven_Slots[i].gameObject.SetActive(false);
            }
        }
    }


    public void Info_Slot(GameObject clickItem)
    {
        item_Info_Parent.SetActive(true);
        item_Info_Parent.GetComponent<Inventory_info>().info_Item = clickItem;
        item_Info_Parent.GetComponent<Inventory_info>().WakeUP();
        Inven_Slot clickitem = clickItem.GetComponent<Inven_Slot>();

        //정보창에 아이템 등록
        info_Slot.itemData = clickitem.itemData;
        info_Slot.AddItem(clickitem.itemData, 1);

        switch (info_Slot.itemData.itemType)
        {
            case ItemData.ItemType.Equipment:
                Debug.Log("장비 아이템 선택");
                break;
        }
    }

    




}

