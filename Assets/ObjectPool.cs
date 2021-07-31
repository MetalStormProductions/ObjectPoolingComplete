using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool instance;

    [SerializeField] private GameObject laserPrefab;
    private Queue<GameObject> laserPool = new Queue<GameObject>();
    [SerializeField] private int laserPoolSize = 10;

    [SerializeField] private GameObject enemyPrefab;
    private Queue<GameObject> enemyPool = new Queue<GameObject>();
    [SerializeField] private int enemyPoolSize = 10;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < laserPoolSize; i++)
        {
            GameObject laser = Instantiate(laserPrefab);
            laserPool.Enqueue(laser);
            laser.SetActive(false);
        }

        for (int i = 0; i < enemyPoolSize; i++)
        {
            GameObject enemy = Instantiate(enemyPrefab);
            enemyPool.Enqueue(enemy);
            enemy.SetActive(false);
        }
    }

    public GameObject GetLaserFromPool()
    {
        if (laserPool.Count > 0)
        {
            GameObject laser = laserPool.Dequeue();
            laser.SetActive(true);
            return laser;
        }
        else
        {
            GameObject laser = Instantiate(laserPrefab);
            return laser;
        }
    }


    public GameObject GetEnemyFromPool()
    {
        if (enemyPool.Count > 0)
        {
            GameObject enemy = enemyPool.Dequeue();
            enemy.SetActive(true);
            return enemy;
        }
        else
        {
            GameObject enemy = Instantiate(enemyPrefab);
            return enemy;
        }
    }


    public void ReturnLaserToPool(GameObject laser)
    {
        laserPool.Enqueue(laser);
        laser.SetActive(false);
    }

    public void ReturnEnemyToPool(GameObject enemy)
    {
        enemyPool.Enqueue(enemy);
        enemy.SetActive(false);
    }
}
