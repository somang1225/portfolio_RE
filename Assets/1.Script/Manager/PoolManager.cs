using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{

    public GameObject[] monster_prefabs;
    public GameObject[] weapon_prefabs;

    List<GameObject>[] monster_pools;
    List<GameObject>[] weapon_pools;

    //Pool에 게임 오브젝트 삽입
    private void Awake()
    {
        monster_pools = new List<GameObject>[monster_prefabs.Length];
        weapon_pools = new List<GameObject>[weapon_prefabs.Length];

        //몬스터 풀 생성
        for (int index = 0; index < monster_prefabs.Length; index++)
        {
            monster_pools[index] = new List<GameObject>();
        }

        //무기 풀 생성
        for (int index = 0; index < weapon_prefabs.Length; index++)
        {
            weapon_pools[index] = new List<GameObject>();
        }
    }

    //몬스터 생성 함수
    public GameObject Get_monster(int index)
    {
        GameObject select = null;

        foreach (GameObject enemy in monster_pools[index])
        {
            if (!enemy.activeSelf) // || this.transform.childCount > 30
            {
                select = enemy;
                select.SetActive(true);
                break;
            }
        }

        //예외 처리
        if (!select)
        {
            select = Instantiate(monster_prefabs[index], transform);
            monster_pools[index].Add(select);
        }

        return select;
    }

    //무기 생성 함수
    public GameObject Get_weapon(int index)
    {
        GameObject select = null;

        foreach (GameObject bullet in weapon_pools[index])
        {
            if (!bullet.activeSelf)
            {
                select = bullet;
                select.SetActive(true);
                break;
            }
        }

        //예외 처리
        if (!select)
        {
            select = Instantiate(weapon_prefabs[index], transform);
            weapon_pools[index].Add(select);
        }

        return select;
    }

}
