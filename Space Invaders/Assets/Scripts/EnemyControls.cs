using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControls : MonoBehaviour
{
    private Rigidbody rb;
    private float speed;
    private float fireTime;
    private float nextFire;

    private float leftSideScreen = 0.2f;
    private float rightSideScren = 0.8f;

    private bool touchSide;
    private Vector2 screenBounds;

    public GameObject instaPrefab;
    public Transform spawnPos;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
    }

    // Update is called once per frame
    void Update()
    {
        EnemyBounds();

        if(Time.time > nextFire)
        {
            Fire();
            nextFire = Time.time + fireTime;
        }
    }

    private void Fire()
    {
        GameObject rocket = Instantiate(instaPrefab, spawnPos.transform.position, spawnPos.transform.rotation) as GameObject;
    }

    private void EnemyBounds()
    {
        screenBounds.x = Mathf.Clamp(screenBounds.x, leftSideScreen, rightSideScren);
        transform.position = Camera.main.ViewportToWorldPoint(screenBounds);

        if(transform.position.y <= -screenBounds.y)
        {
            Destroy(gameObject);
        }

        if(screenBounds.x >= rightSideScren)
        {
            touchSide = true;
        }
        else if(screenBounds.x <= leftSideScreen)
        {
            touchSide = false;
        }

        Movement(speed);

    }

    private void Movement(float direction)
    {
        if(!touchSide)
        {
            rb.velocity = transform.right * direction;
        }
        else
        {
            rb.velocity = transform.right * -direction;
        }
    }

    private void DownMovement()
    {
        Vector2 position = transform.position;
        position.y -= Time.deltaTime;
        transform.position = position;
    }
}

