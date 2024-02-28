using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RePosition : MonoBehaviour
{
    Collider2D collider2D;

    private void Awake()
    {
        collider2D = GetComponent<Collider2D>();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(!collision.CompareTag("Area"))
        {
            return;
        }

        Vector3 playerPos = GameManager.Instance.player.transform.position;
        Vector3 myPos = transform.position;
        
        

        switch (transform.tag)
        {
            case "Ground":
                float disx = playerPos.x - myPos.x;
                float disy = playerPos.y - myPos.y;

                float dirX = disx < 0 ? -1 : 1;
                float dirY = disy < 0 ? -1 : 1;

                disx = Mathf.Abs(disx);
                disy = Mathf.Abs(disy);

                if (disx > disy)
                {
                    transform.Translate(Vector3.right * dirX * 40);
                }
                else if (disx < disy)
                {
                    transform.Translate(Vector3.up * dirY * 40);
                }
                    break;

            case "Enemy":
                if (collider2D.enabled)
                {
                    Vector3 dist = playerPos - myPos;
                    Vector3 random = new Vector3(Random.Range(-3, 3), Random.Range(-3, 3));
                    transform.Translate(random + dist * 2);
                }
                break;

            case "Item":
                if (collider2D.enabled)
                {
                    Vector3 dist = playerPos - myPos;
                    Vector3 random = new Vector3(Random.Range(-3, 3), Random.Range(-3, 3));
                    transform.Translate(random + dist * 2);
                }
                break;

        }
    }
}