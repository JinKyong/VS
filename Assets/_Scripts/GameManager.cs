using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject[] enemyPrefabs;
    public Transform[] spawnPoint;

    private void Start()
    {
        StartCoroutine(spawnEnemy());
    }

    IEnumerator spawnEnemy()
    {
        while (true)
        {
            GameObject em = PoolManager.Instance.Pop(enemyPrefabs[0], transform);
            em.transform.position = spawnPoint[Random.Range(0, spawnPoint.Length)].position;

            yield return new WaitForSeconds(Random.Range(3f, 8f));
        }
    }
}
