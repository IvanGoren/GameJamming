using UnityEngine;

public class MoveLeft : MonoBehaviour
{

    public float speed = 0.75f;
    
    void Start()
    {

    }
    void Update()
    {
        transform.position += Vector3.left * speed * Time.deltaTime;
    }
}
