using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName ="Scriptble Object/ItemData")]

public class ItemData : ScriptableObject
{
    //일단 임시로 카테고리 지정
    public enum ItemType {Equipment, Box, Heal, Gold } //장비 //힐 //골드 //박스
    //기본 데이터
    [Header("Main Info")]
    public ItemType itemType;
    public Sprite itemIcon;

    [Tooltip("모자 / 갑옷 / 장갑 / 신발 / 100~400")]
    
    public int itemID;
    public string itemName; //아이템 이름
    public string itemDesc; //아이템 설명
    public int item_Level;
    public int itemCount; //아이템 개수
    public bool isUse; //아이템 사용 유무


    //레벨업에 관련된 데이터
    [Header("Level Data")]
    public float baseDamage; //0레벨 데미지
    public int baseCount; //관통 관련 카운트
    public float[] damages; //강화별 데미지
    public int[] counts; //


    //특수한 무기 관련 데이터
    [Header("Equipment")]
    public ItemData[] eqitem_data;


    public int eq_defense;
    public int eq_speed;
    public int eq_power;
    


}
