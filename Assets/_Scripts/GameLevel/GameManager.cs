using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] Transform[] spawnPoint;
    [SerializeField] GameObject enemyPrefab;
    Transform enemyTR;

    [SerializeField] LevelData levelData;
    [SerializeField] GameObject[] ExpPrefabs;
    Coroutine spawnRoutine;

    [SerializeField] FloatValue timer;
    [SerializeField] GameEvent timerEvent;
    int level;
    bool bStart;
    public bool IsClear { get; private set; }

    [Space]
    [SerializeField] GameEvent finishEvent;
    [SerializeField] AudioSource clearSFX;
    [SerializeField] AudioSource overSFX;

    private void Start()
    {
        enemyTR = new GameObject().transform;
        enemyTR.SetParent(transform);

        PoolManager.Instance.Setup();
        spawnRoutine = StartCoroutine(spawnEnemy());

        level = 0;
        //타이머 초기화
        timer.OnAfterDeserialize();
        timerEvent.Raise();
        bStart = true;
    }
    private void Update()
    {
        if (bStart)
        {
            timer.RuntimeValue -= Time.deltaTime;
            timerEvent.Raise();

            if (timer.RuntimeValue < levelData.levelRange[level]) gameLevelUP();
            if (timer.RuntimeValue <= 0) FinishGame(true);
        }
    }

    IEnumerator spawnEnemy()
    {
        while (true)
        {
            Enemy em = PoolManager.Instance.Pop(enemyPrefab, enemyTR).GetComponent<Enemy>();
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

    public void FinishGame(bool isClear)
    {
        if (!bStart) return;

        bStart = false;
        IsClear = isClear;

        if (spawnRoutine is not null) StopCoroutine(spawnRoutine);

        while (enemyTR.childCount > 0)
            PoolManager.Instance.Push(enemyTR.GetChild(0).gameObject);

        if (isClear) clearSFX.Play();
        else overSFX.Play();

        finishEvent.Raise();
        
        //Coin Save
        int coin = PlayerPrefs.HasKey("TotalCoin") ? PlayerPrefs.GetInt("TotalCoin") : 0;
        coin += Player.Instance.Stat.coin;
        PlayerPrefs.SetInt("TotalCoin", coin);
        PlayerPrefs.Save();
    }
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void ToMain()
    {
        SceneManager.LoadScene("MainScene");
    }
}
