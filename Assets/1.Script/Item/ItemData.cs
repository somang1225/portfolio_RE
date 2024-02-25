using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName ="Scriptble Object/ItemData")]

public class ItemData : ScriptableObject
{
    //일단 임시로 카테고리 지정
    public enum ItemType {  Melee, Range, Glove, Shose, Heal, Gold, Box } //근접공격 / 원거리공격 / 장갑 / 신발 / 힐 / 골드

    //기본 데이터
    [Header("Main Info")]
    //아이템의 종류(일회성, 무기, 물약 등등)
    public ItemType itemType;

    public int itemID;
    public string itemName; //아이템 이름
    public string itemDesc; //아이템 설명
    public int itemCount; //아이템 개수
    public bool isUse; //아이템 사용 유무

    public ItemData(int _itemID, string _itemName, string _itemDesc, int _itemCount, bool _isUse)
    {
        itemID = _itemID; itemName = _itemName; itemDesc = _itemDesc; itemCount = _itemCount; isUse = _isUse;
    }

    public Sprite itemIcon;

    //레벨업에 관련된 데이터
    [Header("Level Data")]
    public float baseDamage; //0레벨 데미지
    public int baseCount; //관통 관련 카운트
    public float[] damages; //강화별 데미지
    public int[] counts; //


    //특수한 무기 관련 데이터
    [Header("Weapon")]
    public GameObject projectile;


}
