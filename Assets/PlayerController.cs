using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [SerializeField] private float moveSpeed = 8f;
    [SerializeField] private GameObject laserPrefab;

    // Screen Bounds
    private float xMin, xMax, yMin, yMax;

    private float shipSize = 0.5f;

    private float shootCooldown = 0.1f;
    private float shootCounter;

    // Start is called before the first frame update
    void Start()
    {
        GetScreenBounds();
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        Shoot();

        shootCounter += Time.deltaTime;
    }

    private void Movement()
    {
        transform.Translate(new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0) * moveSpeed * Time.deltaTime);
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, xMin, xMax), Mathf.Clamp(transform.position.y, yMin, yMax), 0f);
    }

    private void GetScreenBounds()
    {
        Camera myCamera = Camera.main;

        xMin = myCamera.ViewportToWorldPoint(new Vector3(0, 0, myCamera.nearClipPlane)).x + shipSize;
        xMax = myCamera.ViewportToWorldPoint(new Vector3(1, 0, myCamera.nearClipPlane)).x - shipSize;

        yMin = myCamera.ViewportToWorldPoint(new Vector3(0, 0, myCamera.nearClipPlane)).y + shipSize;
        yMax = myCamera.ViewportToWorldPoint(new Vector3(1, 1, myCamera.nearClipPlane)).y - shipSize;
    }

    private void Shoot()
    {
        if (Input.GetKey(KeyCode.Space) && shootCounter > shootCooldown)
        {
            foreach (Transform t in transform)
            {
                GameObject laser = ObjectPool.instance.GetLaserFromPool();
                if (laser != null)
                {
                    laser.transform.position = t.position;
                    laser.transform.rotation = t.rotation;
                }                
                //Instantiate(laserPrefab, t.position, t.rotation);
            }
            shootCounter = 0;
        }
    }
}
