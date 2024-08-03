using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{

    public Vector2 inputVec;
    float speed = 5;
    public Scanner scanner{get; private set;}
    public GameObject Shadow;

    Collider2D collider2D;
    
    Rigidbody2D rigidbody2D;
    SpriteRenderer spriteRenderer;
    Animator animator;
    CapsuleCollider2D capsuleCollider2D;

    private void Awake()
    {
        collider2D = GetComponent<Collider2D>();
        rigidbody2D = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        capsuleCollider2D = GetComponent<CapsuleCollider2D>();
        scanner = GetComponent<Scanner>();
    }


    void Update()
    {
        if(!GameManager.Instance.isLive)
        {
            return;
        }
    }

    private void FixedUpdate()
    {        
        if(!GameManager.Instance.isLive)
        {
            return;
        }
        Vector2 moveVec = inputVec.normalized * Time.fixedDeltaTime * (speed + GameManager.Instance.ap_speed);
        rigidbody2D.MovePosition(rigidbody2D.position + moveVec);
    }

    void OnMove(InputValue value)
    {
        inputVec = value.Get<Vector2>();
    }


    private void LateUpdate()
    {
        if(!GameManager.Instance.isLive)
        {
            return;
        }

        //이동하는 동안 케릭터의 좌우 모션
        if (inputVec.x < 0)
        {
            spriteRenderer.flipX = true;
            animator.SetBool("doMove", true);
            Shadow.transform.localPosition = new Vector3(0.35f, 0, 0);
            //capsuleCollider2D.offset = (0.33f, 0.42f);
        }
        else if (inputVec.x > 0)
        {
            spriteRenderer.flipX = false;
            animator.SetBool("doMove", true);
            Shadow.transform.localPosition = new Vector3(-0.35f, 0, 0);
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        //몬스터에게 데미지를 입는 경우
        if(collision.gameObject.tag == "Enemy")
        {
            float curdamage = collision.collider.GetComponent<Enemy>().damage - GameManager.Instance.eq_defense;
            //데미지가 양수일 경우만 데미지 적용
            if(curdamage > 0)
            {
                GameManager.Instance.player_Hp -= Time.deltaTime * curdamage;
            }
            else
            {
                GameManager.Instance.player_Hp -= Time.deltaTime * 0.01f;
            }
            
            
        }
        
    }
    
    
    private void OnTriggerEnter2D(Collider2D collision) 
    {
           //플레이어가 아이템을 먹을 경우
        if(collision.gameObject.tag == "Item")
        {
            ItemManager item = collision.gameObject.GetComponent<ItemManager>();
            //GameManager.Instance.inventory.AcquireItem(item);
            GameManager.Instance.GetItem(item);
            collision.gameObject.SetActive(false);
        } 
    }
    
    
}
