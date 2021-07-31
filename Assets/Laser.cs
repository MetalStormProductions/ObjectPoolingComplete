using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    Rigidbody2D myRigidBody;
    private float moveVelocity = 400f;

    private void Awake()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        myRigidBody.velocity = transform.up * moveVelocity * Time.deltaTime;
    }

    private void OnBecameInvisible()
    {
        ObjectPool.instance.ReturnLaserToPool(gameObject);
    }
}
