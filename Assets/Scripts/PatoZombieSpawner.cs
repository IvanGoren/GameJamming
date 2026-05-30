using UnityEngine;
using System.Collections.Generic;

public class PatoZombieSpawner : MonoBehaviour
{
    public GameObject patoZombiePre;
    public GameObject patoZombieGlitchPre;
    public GameObject megaPatoZombiePre;


    public float heightRange = 1.0f;

    public float maxTime = 3f;

    public float deleteTime = 10f;
    public float extraHeight = 0.8f;
    private List<float> heights = new() { 0.6f, -0.25f, -0.5f };

    private float spawnerTimer;
    private float timer;

    private bool glitchFlag = true;
    private bool armyFlag = true;
    private bool spawnFlag = true;

    void Start()
    {
        SpawnDuck(Random.Range(-heightRange, heightRange));
    }

    void Update()
    {
        spawnerTimer += Time.deltaTime;
        timer += Time.deltaTime;

        if (!spawnFlag)
        {
            return;
        }

        if (timer >= 63f)
        {
            spawnMegaDuck(0);
            this.spawnFlag = false;
            return;
        }

        if (timer >= 31f && armyFlag)
        {
            this.maxTime = 5f;
            this.armyFlag = false;
        }

        if (timer >= 45f && glitchFlag)
        {
            this.patoZombiePre = this.patoZombieGlitchPre;
            this.glitchFlag = false;
        }

        if (timer>= 9f && timer < 25f && (spawnerTimer > maxTime))
        {
            SpawnDuckWall();
            spawnerTimer = 0;
        }
        else if (spawnerTimer > maxTime)
        {
            SpawnDuck(Random.Range(-heightRange, heightRange));
            spawnerTimer = 0;
        }
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
            float height = heights[i];

            if (i == 1 && Random.value < 0.5f)
            {
                height += extraHeight;
            }

            Vector3 spawnPosition = transform.position + new Vector3(0, height);
            GameObject newDuck;
            newDuck = Instantiate(patoZombiePre, spawnPosition, Quaternion.identity);
            Destroy(newDuck, deleteTime);
        }
    }

    public void spawnMegaDuck(float height)
    {
        Vector3 spawnPosition = transform.position + new Vector3(0, height);
        GameObject newDuck;
        newDuck = Instantiate(megaPatoZombiePre, spawnPosition, Quaternion.identity);
        Destroy(newDuck, 4f);
    }

}