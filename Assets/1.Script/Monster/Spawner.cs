using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SpawnData //스포너에서 몬스터를 생성할 경우 몬스터에 들어갈 데이터
{
    public int monster_HP;
    public int monster_Damage;
    public int monster_spriteType;
    public float monster_spawnTime;
    public float monster_speed;
}

public class Spawner : MonoBehaviour
{
    public Transform[] spawnerPoint;
    public SpawnData[] spawnDatas;
    int stage_level;
    float timer;


    private void Awake()
    {
        spawnerPoint = GetComponentsInChildren<Transform>();
    }


    void Update()
    {
        timer += Time.deltaTime;
        stage_level = GameManager.Instance.stage;

        if (timer > spawnDatas[stage_level] .monster_spawnTime)
        {
            timer = 0;
            Spawn();
        }

        

    }

    void Spawn()
    {
        GameObject enemy = GameManager.Instance.pool.Get_monster(0);
        enemy.transform.position = spawnerPoint[Random.Range(1 , spawnerPoint.Length)].position;
        enemy.GetComponent<Enemy>().Init(spawnDatas[stage_level]);
    }

}
