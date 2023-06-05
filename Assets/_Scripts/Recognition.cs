using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Recognition : MonoBehaviour
{
    public List<Collider2D> targetList;

    private void Start()
    {
        targetList = new List<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
            targetList.Add(collision);
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
            targetList.Remove(collision);
    }
}
