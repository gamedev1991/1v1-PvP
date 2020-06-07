using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour {
    private float wallLifeTimer;
    private float wallLife = 2f;
	
	// Update is called once per frame
	void Update () {
        wallLifeTimer += Time.deltaTime;
        if (wallLifeTimer > wallLife)
        {
            this.gameObject.SetActive(false);
            wallLifeTimer = 0;
        }

    }
}
