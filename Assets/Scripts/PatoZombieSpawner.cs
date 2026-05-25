using UnityEngine;

public class PatoZombieSpawner : MonoBehaviour
{
    public GameObject patoZombiePre;

    public float heightRange = 1.0f;

    public float maxTime = 1.75f;

    public float deleteTime = 10f;

    private float timer;

    void Start()
    {
        SpawnDuck();
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer > maxTime)
        {
            SpawnDuck();
            timer = 0;
                 Debug.Log("time reset");
        }
    }

    public void SpawnDuck()
    {
        Vector3 spawnPosition = transform.position + new Vector3(0, Random.Range(-heightRange, heightRange));

        GameObject newDuck;

        newDuck = Instantiate(patoZombiePre, spawnPosition, Quaternion.identity);

        Destroy(newDuck, deleteTime);
    }
}
