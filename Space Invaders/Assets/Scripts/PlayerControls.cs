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

    public TMP_Text score;
    public GameObject rocketPrefab;
    public float fireRate;
    public Transform firePos;

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

        transform.Translate(Input.acceleration.x * speed, 0, 0);

        if (Input.GetKeyDown(KeyCode.Space) && Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            GameObject rocket = Instantiate(rocketPrefab, firePos.position, firePos.rotation) as GameObject;
        }

        score.text = "Score: " + playerScore;
    }
}
