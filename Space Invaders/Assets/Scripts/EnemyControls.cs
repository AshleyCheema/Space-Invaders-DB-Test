using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControls : MonoBehaviour
{
    private Rigidbody rb;
    private float speed = 1f;
    private float fireTime = 3f;
    private float nextFire;

    private float leftSideScreen = 0.2f;
    private float rightSideScreen = 0.8f;

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

        if(gameObject.tag == "Enemy")
        {
            DownMovement();
        }

        if (Time.time > nextFire)
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
        //Constraining the enemies with in the camera border
        Vector3 pos = Camera.main.WorldToViewportPoint(transform.position);
        pos.x = Mathf.Clamp(pos.x, leftSideScreen, rightSideScreen);
        transform.position = Camera.main.ViewportToWorldPoint(pos);

        if (transform.position.y <= -screenBounds.y)
        {
            Destroy(gameObject);
        }

        //Check whether the enemies have hit the right side of the screen
        //If they have not then continue to move that direction
        if (pos.x >= rightSideScreen)
        {
            touchSide = true;
            //transform.position -= new Vector3(transform.position.x, 1, transform.position.z);
        }
        else if (pos.x <= leftSideScreen)
        {
            //transform.position -= new Vector3(transform.position.x, 1, transform.position.z);
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

