using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUP : MonoBehaviour
{
    int count;

    private void Awake()
    {
        count = transform.childCount;

        foreach (Transform tr in transform)
            tr.gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        int[] rand = new int[3];
        while (true)
        {
            rand[0] = Random.Range(0, count);
            rand[1] = Random.Range(1, count);
            rand[2] = Random.Range(2, count);

            if (rand[0] != rand[1] && rand[1] != rand[2] && rand[2] != rand[0]) break;
        }

        for (int i = 0; i < 3; i++)
            transform.GetChild(rand[i]).gameObject.SetActive(true);
    }

    private void OnDisable()
    {
        foreach (Transform tr in transform)
            tr.gameObject.SetActive(false);
    }
}
