using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{

    public Rigidbody2D rb2D;
    public float moveSpeed = 1;

    void Start()
    {
    }

    void Update()
    {
    if (Input.GetKey(KeyCode.UpArrow))
    {
        transform.Translate(Vector3.up * moveSpeed * Time.deltaTime);
    }
    if (Input.GetKey(KeyCode.DownArrow))
    {
        transform.Translate(Vector3.down * moveSpeed * Time.deltaTime);
    }

    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        FindAnyObjectByType<GameManager>().GameOver();
    }
}
