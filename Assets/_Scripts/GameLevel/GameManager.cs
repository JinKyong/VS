using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] Transform[] spawnPoint;
    [SerializeField] LevelData levelData;
    [SerializeField] GameObject enemyPrefab;
    [SerializeField] GameObject[] ExpPrefabs;

    int level;
    float timer;

    private void Start()
    {
        PoolManager.Instance.Setup();

        StartCoroutine(spawnEnemy());

        level = 0;
        timer = 1800f;
        HUD.Instance.UpdateTimer(timer);
    }
    private void Update()
    {
        timer -= Time.deltaTime;
        HUD.Instance.UpdateTimer(timer);

        if (timer < levelData.levelRange[level]) gameLevelUP();
    }

    IEnumerator spawnEnemy()
    {
        while (true)
        {
            Enemy em = PoolManager.Instance.Pop(enemyPrefab, transform).GetComponent<Enemy>();
            em.transform.position = spawnPoint[Random.Range(0, spawnPoint.Length)].position;
            em.Init(Random.Range(levelData.levels[level].min,
                levelData.levels[level].max));

            yield return new WaitForSeconds(Random.Range(1f, 3f));
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
