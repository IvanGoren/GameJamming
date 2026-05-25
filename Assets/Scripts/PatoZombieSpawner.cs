using UnityEngine;
using System.Collections.Generic;

public class PatoZombieSpawner : MonoBehaviour
{
    public GameObject patoZombiePre;

    public float heightRange = 1.0f;

    public float maxTime = 3f;

    public float deleteTime = 10f;
    public List<float> heights = new() { 1f, 0.5f, -0.5f };

    private float spawnerTimer;
    private float timer;

    private bool duckWall = true;
    
    private bool megaZombie = true;

    void Start()
    {
        SpawnDuck(Random.Range(-heightRange, heightRange));
    }

    void Update()
    {
        spawnerTimer += Time.deltaTime;

       
        if (timer>= 15f && duckWall)
        {
            SpawnDuckWall();
        } else if (timer>= 15f && megaZombie)
        {
            SpawnDuckWall();
        }
        else if (spawnerTimer > maxTime)
        {
            SpawnDuck(Random.Range(-heightRange, heightRange));
            spawnerTimer = 0;
        }

        timer += Time.deltaTime;
        Debug.Log(timer);
    }

    public void SpawnDuck(float height)
    {
        Vector3 spawnPosition = transform.position + new Vector3(0, height);

        GameObject newDuck;

        newDuck = Instantiate(patoZombiePre, spawnPosition, Quaternion.identity);

        Destroy(newDuck, deleteTime);
    }

    public void SpawnDuckWall()
    {
        for (int i = 0; i < heights.Count; i++)
        {
            Vector3 spawnPosition = transform.position + new Vector3(0, heights[i]);

            GameObject newDuck;

            newDuck = Instantiate(patoZombiePre, spawnPosition, Quaternion.identity);
            Destroy(newDuck, deleteTime);
        }
        this.duckWall = false;
    }
}