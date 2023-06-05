using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public EnemyDataSO enemyDatas;
    List<SEnemyData> dataList;

    public Transform[] spawnPoint;
    [SerializeField] GameObject enemyPrefab;
    [SerializeField] GameObject[] ExpPrefabs;

    private void Start()
    {
        PoolManager.Instance.Setup();

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
    public void SpawnExp(int num, Vector3 pos)
    {
        GameObject exp = PoolManager.Instance.Pop(ExpPrefabs[num], transform);
        exp.transform.position = pos;
    }
}
