using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public EnemyDataSO enemyDatas;
    List<SEnemyData> dataList;

    public GameObject enemyPrefab;
    public Transform[] spawnPoint;

    private void Start()
    {
        dataList = enemyDatas.dataList;
        StartCoroutine(spawnEnemy());
    }

    IEnumerator spawnEnemy()
    {
        while (true)
        {
            Enemy em = PoolManager.Instance.Pop(enemyPrefab, transform).GetComponent<Enemy>();
            em.transform.position = spawnPoint[Random.Range(0, spawnPoint.Length)].position;
            em.Init(dataList[0]);

            yield return new WaitForSeconds(Random.Range(1f, 3f));
        }
    }
}
