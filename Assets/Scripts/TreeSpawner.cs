using UnityEngine;
using UnityEngine.Jobs;

public class TreeSpawner : MonoBehaviour
{

    public GameObject tree;
    public float maxTime = 5f;
    public float deleteTime = 10f;
    private float treeTimer;

    void Start()
    {
    }

    void Update()
    {
        treeTimer += Time.deltaTime;

        if (treeTimer > maxTime)
        {
            SpawnTree();
            treeTimer = 0;
        }
    }

    public void SpawnTree()
    {
        Vector3 spawnPosition = transform.position + new Vector3(0, -0.3f);

        GameObject newTree;

        newTree = Instantiate(tree, spawnPosition, Quaternion.identity);

        Destroy(newTree, deleteTime);
    }
}