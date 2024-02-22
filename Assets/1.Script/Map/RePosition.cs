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

        float disx = Mathf.Abs( playerPos.x - myPos.x ); 
        float disy = Mathf.Abs( playerPos.y - myPos.y);

        Vector3 playerDir = GameManager.Instance.player.inputVec;
        float dirX = playerDir.x < 0 ? -1 : 1;
        float dirY = playerDir.y < 0 ? -1 : 1;

        switch (transform.tag)
        {
            case "Ground":
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
                    transform.Translate(playerDir * 20 + new Vector3 (Random.Range(-3f, 3f), Random.Range(-3f, 3f), 0f));
                }
                break;

        }
    }
}
