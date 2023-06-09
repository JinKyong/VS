using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectWeapon : MonoBehaviour
{
    bool bFirst;
    int count;

    private void Awake()
    {
        count = transform.childCount;

        foreach (Transform tr in transform)
            tr.gameObject.SetActive(false);

        bFirst = true;
    }

    private void OnEnable()
    {
        Time.timeScale = 0f;
        int[] rand = new int[3];

        if (bFirst)
        {
            rand[0] = 0;
            rand[1] = 1;
            rand[2] = 2;

            bFirst = false;
        }
        else
        {
            while (true)
            {
                rand[0] = Random.Range(0, count);
                rand[1] = Random.Range(1, count);
                rand[2] = Random.Range(2, count);

                if (rand[0] != rand[1] && rand[1] != rand[2] && rand[2] != rand[0]) break;
            }
        }

        for (int i = 0; i < 3; i++)
            transform.GetChild(rand[i]).gameObject.SetActive(true);
    }

    private void OnDisable()
    {
        Time.timeScale = 1f;
        foreach (Transform tr in transform)
            tr.gameObject.SetActive(false);
    }
}
