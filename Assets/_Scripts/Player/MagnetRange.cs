using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetRange : MonoBehaviour
{
    BoxCollider2D range;

    private void Start()
    {
        range = GetComponent<BoxCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("EXP"))
        {
            collision.GetComponent<Exp>().OnTarget();
        }
    }
}
