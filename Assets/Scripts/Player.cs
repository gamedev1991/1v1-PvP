using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum PlayerType
{
    Player1,
    Player2
}
[System.Serializable]
public class ObjectPoolItem
{
    public GameObject objectToPool;
    public int amountToPool;
    public bool shouldExpand;
}

public class Player : MonoBehaviour {
    public float speed;
    public PlayerType playerType;
    private float boundaryLimit = 2.5f;
    public Rigidbody bullet;
    public Transform shootPos;
    public List<ObjectPoolItem> itemsToPool;
    public List<GameObject> pooledBullets;
    public int numberOfLives = 3;
    public Text lifeText;
    void Start()
    {
        pooledBullets = new List<GameObject>();
        foreach (ObjectPoolItem item in itemsToPool)
        {
            for (int i = 0; i < item.amountToPool; i++)
            {
                GameObject obj = (GameObject)Instantiate(item.objectToPool);
                obj.SetActive(false);
                pooledBullets.Add(obj);
            }
        }
    }

    public void LeftMove()
    {
        if (GameManager.instance.gameOver)
        {
            return;
        }
        if (transform.position.x > -boundaryLimit)
        {
            transform.Translate(Vector3.left * Time.deltaTime * speed);
        }
    }

    public void RightMove()
    {
        if (GameManager.instance.gameOver)
        {
            return;
        }
        if (transform.position.x < boundaryLimit)
        {
            transform.Translate(Vector3.right * Time.deltaTime * speed);
        }
    }


    public void Move()
    {
        if (playerType.Equals(PlayerType.Player1))
        {
            if (Input.GetKey(KeyCode.A) && transform.position.x > -boundaryLimit)
            {
                transform.Translate(Vector3.right * Time.deltaTime * speed);
            }

            if (Input.GetKey(KeyCode.D) && transform.position.x < boundaryLimit)
            {
                transform.Translate(Vector3.left * Time.deltaTime * speed);
            }
        }

        if (playerType.Equals(PlayerType.Player2))
        {
            if (Input.GetKey(KeyCode.LeftArrow) && transform.position.x > -boundaryLimit)
            {
                transform.Translate(Vector3.left * Time.deltaTime * speed);
            }

            if (Input.GetKey(KeyCode.RightArrow) && transform.position.x < boundaryLimit)
            {
                transform.Translate(Vector3.right * Time.deltaTime * speed);
            }
        }
    }
    public void ReduceLife()
    {
        numberOfLives--;
        if (this.tag.Equals("Player1"))
        {
            lifeText.text = "P1:" + numberOfLives;
        }
        else {
            lifeText.text = "P2:" + numberOfLives;
        }
    }

    public void Fire()
    {
        if (GameManager.instance.gameOver)
        {
            return;
        }
        GameObject bullet = GetPooledObject("bullet");
        bullet.SetActive(true);
        bullet.transform.position = shootPos.position;
        Rigidbody cloneRb = bullet.GetComponent<Rigidbody>();
        cloneRb.velocity = Vector3.zero;// Resetting velocity
        cloneRb.velocity = transform.forward * 10;
    }

    public GameObject GetPooledObject(string tag)
    {
        for (int i = 0; i < pooledBullets.Count; i++)
        {
            if (!pooledBullets[i].activeInHierarchy && pooledBullets[i].tag == tag)
            {
                return pooledBullets[i];
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
                    pooledBullets.Add(obj);
                    return obj;
                }
            }
        }
        return null;
    }

    void Update()
    {
#if UNITY_EDITOR
        
        Move();
#endif
    }
}
