using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HudDamageText : MonoBehaviour
{
    public float moveSpeed = 1;
    public float alphaSpeed = 5;
    public float destroyTime = 2;
    public TextMeshPro text;
    Color alpha;

    public string damage;

    private void Start()
    {
        text = GetComponent<TextMeshPro>();
        text.text = damage.ToString();
        alpha = text.color;
        Invoke("DestroyObject", destroyTime);
    }

    private void Update()
    {
        transform.Translate(new Vector3(0, moveSpeed * Time.deltaTime, 0));
        alpha.a = Mathf.Lerp(alpha.a, 0, Time.deltaTime * alphaSpeed);
        text.color = alpha;
    }

    private void DestroyObject()
    {
        Destroy(gameObject);
    }
}