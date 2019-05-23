using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnableManager : MonoBehaviour {

    public List<ObjectPoolItem> itemsToPool;
    public List<GameObject> pooledWalls;
    private float spawnTimer;
    private float spawnInterval = 0.5f;
    // Use this for initialization
    void Start () {
        pooledWalls = new List<GameObject>();
        foreach (ObjectPoolItem item in itemsToPool)
        {
            for (int i = 0; i < item.amountToPool; i++)
            {
                GameObject obj = (GameObject)Instantiate(item.objectToPool);
                obj.SetActive(false);
                pooledWalls.Add(obj);
            }
        }
    }
	
	// Update is called once per frame
	void Update () {
        if (!GameManager.instance.gameOver)
        {
            spawnTimer += Time.deltaTime;
            if (spawnTimer > spawnInterval)
            {
                GameObject wall = GetPooledObject("Wall");
                wall.SetActive(true);
                wall.transform.position = new Vector3(Random.Range(-2.0f, 2.0f), 1, Random.Range(3, -3));
                spawnTimer = 0;
            }
        }

    }

    public GameObject GetPooledObject(string tag)
    {
        for (int i = 0; i < pooledWalls.Count; i++)
        {
            if (!pooledWalls[i].activeInHierarchy && pooledWalls[i].tag == tag)
            {
                return pooledWalls[i];
            }
        }
        foreach (ObjectPoolItem item in itemsToPool)
        {
            if (item.objectToPool.tag == tag)
            {
                if (item.shouldExpand)
                {
                    GameObject obj = (GameObject)Instantiate(item.objectToPool);
                    obj.SetActive(false);
                    pooledWalls.Add(obj);
                    return obj;
                }
            }
        }
        return null;
    }
}
