using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    // Screen Bounds
    private float xMin, xMax, yMin, yMax;

    [SerializeField] private GameObject enemyPrefab;

    private float minSpawnTime = 0.01f;
    private float maxSpawnTime = 0.04f;

    // Start is called before the first frame update
    void Start()
    {
        GetScreenBounds();
        StartCoroutine(SpawnEnemy());
    }

    private void GetScreenBounds()
    {
        Camera myCamera = Camera.main;

        xMin = myCamera.ViewportToWorldPoint(new Vector3(0, 0, myCamera.nearClipPlane)).x;
        xMax = myCamera.ViewportToWorldPoint(new Vector3(1, 0, myCamera.nearClipPlane)).x;

        yMin = myCamera.ViewportToWorldPoint(new Vector3(0, 0, myCamera.nearClipPlane)).y;
        yMax = myCamera.ViewportToWorldPoint(new Vector3(1, 1, myCamera.nearClipPlane)).y;
    }

    private IEnumerator SpawnEnemy()
    {
        while (true)
        {
            GameObject enemy = ObjectPool.instance.GetEnemyFromPool();
            enemy.transform.position = new Vector3(Random.Range(xMin, xMax), yMax + 1f, 0f);            
            //Instantiate(enemyPrefab, new Vector3(Random.Range(xMin, xMax), yMax + 1f, 0f), Quaternion.identity);
            yield return new WaitForSeconds(Random.Range(minSpawnTime, maxSpawnTime));
        }
    }
}
