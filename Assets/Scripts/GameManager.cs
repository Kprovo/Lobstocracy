using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public static GameManager _singleton;

    public Text winText;

    public List<Enemy> enemies = new List<Enemy>();
    public int enemiesLeft = 0;

    public int score = 0;
    // Set in inspector
    public Text scoreText;

	// Use this for initialization
	void Awake () {
        if (_singleton == null) {
            _singleton = this;
        } else {
            Destroy(gameObject);
        }
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.R)) {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        scoreText.text = "Score: " + score.ToString();
	}

    public void GameOverCheck()
    {
        if(enemiesLeft == 0) {
            winText.text = "You Win!";
        }
    }
}
