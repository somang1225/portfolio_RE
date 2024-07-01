using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float damage;
    public int per;

    Rigidbody2D rigidbody2D;

    private void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    //무기의 기본적인 스텟
    public void Init(float pre_damage, int per, Vector3 dir)
    {
        //pre = 무기상수 // 무기 데미지 = 무기상수 + 케릭터 데미지 강화
        this.damage = pre_damage + GameManager.Instance.player_damage + GameManager.Instance.eq_power;
        this.per = per;

        if(per >= 0)
        {
            rigidbody2D.velocity = dir * 15f;
        }
    }

    //무기와 몬스터의 충돌
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Enemy") || per == -100)
        {
            return;
        }

        per -- ;

        if (per < 0)
        {
            rigidbody2D.velocity = Vector2.zero;
            gameObject.SetActive(false);
        }
    }

    //무기가 화면 밖으로 나갈 경우
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(!collision.CompareTag("Area") || per == -100)
        {
            return;
        }
        
        gameObject.SetActive(false);
    }
}
