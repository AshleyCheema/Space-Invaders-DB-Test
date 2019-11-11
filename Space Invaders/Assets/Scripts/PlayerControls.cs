using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerControls : MonoBehaviour
{
    private float speed = 10.0f;
    private float translation;
    private float nextFire;

    public static int playerScore;

    public bool deadJim;
    public TMP_Text score;
    public GameObject rocketPrefab;
    public float fireRate;
    public Transform firePos;

    public GameObject gameOver;

    // Update is called once per frame
    void Update()
    {
        //Get the player position
        //Using the clamp function we use the camera as a bounding box. The floats were added so the player will stay in full view
        //This is better then using colliders as it will scale with any screen resolution
        Vector3 pos = Camera.main.WorldToViewportPoint(transform.position);
        pos.x = Mathf.Clamp(pos.x, 0.1f, 0.9f);
        transform.position = Camera.main.ViewportToWorldPoint(pos);

        //This gets the horizontal axis which uses the left and right arrow keys for movement
        //translation = Input.GetAxis("Horizontal");
        //translation *= Time.deltaTime * speed;
        //transform.Translate(translation, 0, 0);

        transform.Translate(Input.acceleration.x / 2, 0, 0);

        if (deadJim == false)
        {
            if (Input.GetKeyDown(KeyCode.Space) && Time.time > nextFire)
            {
                Fire();
            }

            if (Input.touchCount > 0 && Time.time > nextFire)
            {
                Touch touch = Input.GetTouch(0);

                if (touch.phase == TouchPhase.Began)
                {
                    Fire();
                }
            }
        }
        else
        {
            gameOver.SetActive(true);
        }

        score.text = "Score: " + playerScore;
    }

    private void Fire()
    {
        nextFire = Time.time + fireRate;
        GameObject rocket = Instantiate(rocketPrefab, firePos.position, firePos.rotation) as GameObject;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Enemy")
        {
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
            deadJim = true;
        }
    }
}
