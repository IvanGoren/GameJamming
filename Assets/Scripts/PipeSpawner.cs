using UnityEngine;
using UnityEngine.Jobs;

public class PipeSpawner : MonoBehaviour
{

    public GameObject pipePrefab;

    public float heightRange = 0.5f;

    public float maxTime = 1.75f;

    public float deleteTime = 10f;

    private float timer;

    void Start()
    {
        SpawnPipe();
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer > maxTime)
        {
            SpawnPipe();
            timer = 0;
                 Debug.Log("time reset");
        }
    }

    public void SpawnPipe()
    {
        Vector3 spawnPosition = transform.position + new Vector3(0, Random.Range(-heightRange, heightRange));

        GameObject newPipe;

        newPipe = Instantiate(pipePrefab, spawnPosition, Quaternion.identity);

        Destroy(newPipe, deleteTime);
    }
}