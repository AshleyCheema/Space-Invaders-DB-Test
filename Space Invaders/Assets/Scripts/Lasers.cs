using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lasers : MonoBehaviour
{
    private Rigidbody rb;
    private Vector2 screenBounds;
    private float speed = 5f;

    public GameObject playerRocket;
    public GameObject enemyRocket;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        RocketDirection();

        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
    }

    private void Update()
    {
        if (transform.position.y > screenBounds.y)
        {
            Destroy(gameObject);
        }
        if (transform.position.y < -screenBounds.y)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy" && playerRocket != null)
        {
            DestroyRocket(other.gameObject);
            DestroyRocket(gameObject);
        }
        else if (other.gameObject.tag == "Player" && enemyRocket != null)
        {
            DestroyRocket(other.gameObject);

            DestroyRocket(gameObject);
        }
    }

    private void DestroyRocket(GameObject destroy)
    {
        Destroy(destroy);
    }

    private void RocketDirection()
    {
        if (playerRocket != null)
        {
            rb.velocity = transform.up * speed;
        }
        if (enemyRocket != null)
        {
            rb.velocity = transform.up * -speed;
        }
    }
}
