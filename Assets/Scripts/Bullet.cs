using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {
    public Vector3 startPos;

    void Awake()
    {
        startPos = this.transform.position;
    }
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (this.transform.position.z > 4 || this.transform.position.z < -4)
        {
            this.gameObject.SetActive(false);
            this.transform.position = startPos;           
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other != null)
        {
            if (!GameManager.instance.gameOver)
            {
                if (other.name.Contains("Player"))
                {
                    other.GetComponent<Player>().ReduceLife();
                    GameManager.instance.CheckWinner();
                    Debug.Log("Name = " + other.name);
                    this.gameObject.SetActive(false);
                }

                if (other.tag.Equals("Wall"))
                {
                    other.gameObject.SetActive(false);
                    this.gameObject.SetActive(false);
                }
            }
        }
    }
}
