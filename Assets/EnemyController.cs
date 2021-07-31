using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private Rigidbody2D myRigidBody;
    private float moveVelocity = 100f;

    [SerializeField] private GameObject explosionPrefab;

    private void Awake()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        myRigidBody.velocity = Vector2.down * moveVelocity * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Laser")
        {
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);

            ObjectPool.instance.ReturnLaserToPool(collision.gameObject);
            //Destroy(collision.gameObject);

            ObjectPool.instance.ReturnEnemyToPool(gameObject);
            //Destroy(gameObject);
        }
    }

    private void OnBecameInvisible()
    {
        ObjectPool.instance.ReturnEnemyToPool(gameObject);
    }
}
