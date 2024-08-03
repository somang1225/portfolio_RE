using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Redcode.Pools;

public class Enemy : MonoBehaviour //, IPoolObject
{

    public float hp;
    public float max_HP;
    public float speed;
    public float damage;

    //드랍용 아이템
    public GameObject itemGold, item_Hp, item_weapon_box, item_;

    public Rigidbody2D target;
    public RuntimeAnimatorController[] animCon;

    bool isLive;

    Collider2D collider2D;
    Rigidbody2D rigidbody2D;
    SpriteRenderer spriteRenderer;
    Animator animator;
    WaitForFixedUpdate wait; //물리프레임의 wait를 위한 변수

    private void Awake()
    {
        collider2D = GetComponent<Collider2D>();
        rigidbody2D = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        wait = new WaitForFixedUpdate();
    }

    private void FixedUpdate()
    {
        if (!isLive || animator.GetCurrentAnimatorStateInfo(0).IsName("Hit"))
            return;

        Vector2 dirVec = target.position - rigidbody2D.position;
        Vector2 nextVec = dirVec.normalized * speed * Time.fixedDeltaTime;

        rigidbody2D.MovePosition(rigidbody2D.position + nextVec);
        rigidbody2D.velocity = Vector2.zero;


        //스테이지 변화
        if(animator.runtimeAnimatorController != animCon[GameManager.Instance.stage])
        {
            isLive = false;
            collider2D.enabled = false;
            rigidbody2D.simulated = false;
            spriteRenderer.sortingOrder = 1;
            animator.SetBool("Dead", true);
            
        }
    }

    private void LateUpdate()
    {
        if (target.position.x < rigidbody2D.position.x)
        {
            spriteRenderer.flipX = true;
        }

        else if (target.position.x > rigidbody2D.position.x)
        {
            spriteRenderer.flipX = false;
        }
    }

    //오브젝트 풀링으로 다시 오브젝트 활성화
    private void OnEnable()
    {
        target = GameManager.Instance.player.GetComponent<Rigidbody2D>();
        isLive = true;
        collider2D.enabled = true;
        rigidbody2D.simulated = true;
        spriteRenderer.sortingOrder = 2;
        animator.SetBool("Dead", false);
        hp = max_HP;
    }

    //몬스터 정보에 대한 초기화
    public void Init(SpawnData monster_data)
    {
        animator.runtimeAnimatorController = animCon[monster_data.monster_spriteType];
        speed = monster_data.monster_speed;
        max_HP = monster_data.monster_HP;
        hp = monster_data.monster_HP;
        damage = monster_data.monster_Damage;
    }

    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //몬스터랑 무기(스킬)에 닿을 경우
        if (!collision.CompareTag("Bullet") || !isLive)
        {
            return;
        }

        
        
        hp -= collision.GetComponent<Bullet>().damage;


        //Debug.Log(collision.GetComponent<Bullet>().damage);

        StartCoroutine(KnockBack());
        

        //몬스터가 살아있는 경우
        if (hp > 0)
        {
            animator.SetTrigger("Hit");
        }
        else // 몬스터 피가 0보다 작거나 같을 때
        {
            GameManager.Instance.GetKill();
            isLive = false;
            collider2D.enabled = false;
            rigidbody2D.simulated = false;
            spriteRenderer.sortingOrder = 1;
            animator.SetBool("Dead", true);
            GameManager.Instance.GetExp();
            

            //골드 드랍 100%
            //Instantiate(itemGold, transform.position + new Vector3 (1, 0, 0), itemGold.transform.rotation);
            DropItem(0);

            int item_ran = Random.Range(0, 10); //아이템 드랍 확률
            
            if(item_ran < 2) //아이템 획득 실패 20%
            {
                
            }
            else if (item_ran < 9) //장비 아이템 획득 70%
            {
                //Instantiate(item_, transform.position, item_.transform.rotation);
                DropItem(2);
            }
            else if (item_ran < 10) //소비 아이템 획득 10%
            {
                //Instantiate(item_, transform.position, item_.transform.rotation);
                DropItem(1);
            }



        }
    }

    //몬스터 넉백용 코루틴
    IEnumerator KnockBack()
    {
        yield return wait; //하나의 물리 프레임 딜레이      //null 값이면 1프레임 쉬고 또는 시간을 통해 기다리게 할 수 있다.
        Vector3 playerPos = GameManager.Instance.player.transform.position;
        Vector3 dirVec = transform.position - playerPos;
        rigidbody2D.AddForce(dirVec.normalized * 3, ForceMode2D.Impulse);
    }

    //죽었을 경우 이벤트 (애니메이션에서 작동)
    void Dead()
    {
        gameObject.SetActive(false);
    }

    void DropItem(int index)
    {
        GameObject dropItem = GameManager.Instance.pool.Get_item(index);
        dropItem.transform.position = gameObject.transform.position;

    }



}
