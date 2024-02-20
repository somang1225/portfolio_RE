using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
//using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public Vector2 inputVec;
    public float speed = 5;
    public Scanner scanner;
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

    void Start()
    {
        
    }


    void Update()
    {
        inputVec.x = Input.GetAxisRaw("Horizontal");
        inputVec.y = Input.GetAxisRaw("Vertical");
    }

    private void FixedUpdate()
    {
        Vector2 moveVec = inputVec.normalized * speed * Time.fixedDeltaTime;
        rigidbody2D.MovePosition(rigidbody2D.position + moveVec);
    }

    private void LateUpdate()
    {

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
        GameManager.instance.player_Hp -= Time.deltaTime * collision.collider.GetComponent<Enemy>().damage;
        
    }

    private void OnTriggerEnter2D(Collider2D collision) 
    {
        if(collider2D.gameObject.tag == "Item")
        {
            Item item = collision.gameObject.GetComponent<Item>();

            switch (item.data.itemType)
            {
                case ItemData.ItemType.Gold:
                    GameManager.instance.gold++;
                    break;
            }
        }    
    }

}