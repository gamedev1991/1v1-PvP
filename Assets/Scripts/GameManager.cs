using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
    public Player player1;
    public Player player2;
    public static GameManager instance;
    public Text winnerText;
    public bool gameOver;
    private Player winner;
    public GameObject restartButton;


    // Use this for initialization
    void Start() {
        if (instance == null)
        {
            instance = this;
        }

    }
    public void CheckWinner()
    {
        if (player1.numberOfLives == 0)
        {
            winner = player2;
            gameOver = true;
        }
        else if (player2.numberOfLives == 0)
        {
            winner = player1;
            gameOver = true;
        }

        if (gameOver)
        {
            winnerText.gameObject.SetActive(true);
            winnerText.text = winner.name + "WINS !!";
            restartButton.SetActive(true);
        }
    }

    public void Restart()
    {
        SceneManager.LoadScene("Demo");
    }

	// Update is called once per frame
	void Update () {
		
	}
}
