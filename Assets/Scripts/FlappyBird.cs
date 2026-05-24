using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlappyBird : MonoBehaviour
{

    public Rigidbody2D rb2D;
    public float velocity = 2;
    public float rotationSpeed = 25;

    void Start()
    {
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            rb2D.linearVelocity = Vector2.up * velocity;
        }

        transform.rotation = Quaternion.Euler(0, 0, rb2D.linearVelocity.y * rotationSpeed * Time.deltaTime * 100);
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        FindAnyObjectByType<GameManager>().GameOver();
    }

}
