using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] Transform[] spawnPoint;
    [SerializeField] LevelData levelData;
    [SerializeField] GameObject enemyPrefab;
    [SerializeField] GameObject[] ExpPrefabs;

    [SerializeField] FloatValue timer;
    [SerializeField] GameEvent timerEvent;
    int level;

    private void Start()
    {
        PoolManager.Instance.Setup();
        StartCoroutine(spawnEnemy());

        level = 0;
        timerEvent.Raise();
    }
    private void Update()
    {
        timer.RuntimeValue -= Time.deltaTime;
        timerEvent.Raise();

        if (timer.RuntimeValue < levelData.levelRange[level]) gameLevelUP();
    }

    IEnumerator spawnEnemy()
    {
        while (true)
        {
            Enemy em = PoolManager.Instance.Pop(enemyPrefab, transform).GetComponent<Enemy>();
            em.transform.position = spawnPoint[Random.Range(0, spawnPoint.Length)].position;
            em.Init(Random.Range(levelData.enemyRange[level].min,
                levelData.enemyRange[level].max));

            yield return new WaitForSeconds(Random.Range(
                levelData.spawnTerm[level].min,
                levelData.spawnTerm[level].max));
        }
    }
    public void SpawnExp(int num, Vector3 pos)
    {
        GameObject exp = PoolManager.Instance.Pop(ExpPrefabs[num], transform);
        exp.transform.position = pos;
    }

    private void gameLevelUP()
    {
        level++;
    }
}
