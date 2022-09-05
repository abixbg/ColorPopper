using UnityEngine;
using System;

public class DotCollector : MonoBehaviour
{

    public SpriteRenderer spriteColor;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Dot")
        other.gameObject.SetActive(false);
    }
}
