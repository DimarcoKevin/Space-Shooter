using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour {


    public GameObject[] asteroids;
    public GameObject enemyShip;
    public GameObject[] hazards;
    public Vector3 spawnValues;

   
    public int asteroidCount;
    public int enemyShipCount;
    public int hazardCount;
    public int waveCount;
    public float spawnWait;
    public float startWait;
    public float waveWait;
    

    public Text scoreText;
    public Text restartText;
    public Text gameOverText;
    public Text HighscoreTitle;
    public Text Highscores;
    public Text EnterName;
    public Text WaveText;

    private bool decision;
    private bool gameOver;
    private bool restart;
    private int score;
    private int choice;
    

    void Start() {
        gameOver = false;
        restart = false;
        restartText.text = ("");
        gameOverText.text = ("");
        HighscoreTitle.text = ("");
        Highscores.text = ("");
        EnterName.text = ("");

        StartCoroutine (SpawnWaves());
        waveCount = 1;
        score = 0;
        UpdateScore();
    }

    void Update() {
        if (restart) {
            if (Input.GetKeyDown(KeyCode.R)) {
                SceneManager.LoadScene("Main");               
            }
        }
    } // reloads first level

    private IEnumerator SpawnWaves() {
        while (waveCount < 1000) {
            waveCount++;
            Debug.Log("Wave # " + waveCount);
            UpdateWave();
            
        yield return new WaitForSeconds(startWait);
            for (int i = 0; i < hazardCount; i++) {
                EnemyDecider(waveCount);
                Debug.Log("Decision = " + decision); // output to log for if enemy ship or if asteroid
                if (decision) {
                    GameObject hazard = (enemyShip);
                    Vector3 spawnPosition = new Vector3((Random.Range(-spawnValues.x, spawnValues.x)), spawnValues.y, spawnValues.z);
                    Quaternion spawnRoatation = Quaternion.identity;
                    Instantiate(hazard, spawnPosition, rotation: spawnRoatation);
                    yield return new WaitForSeconds(spawnWait);
                } else {
                    GameObject hazard = (asteroids)[Random.Range(0, (asteroids.Length))];
                    Vector3 spawnPosition = new Vector3((Random.Range(-spawnValues.x, spawnValues.x)), spawnValues.y, spawnValues.z);
                    Quaternion spawnRoatation = Quaternion.identity;
                    Instantiate(hazard, spawnPosition, rotation: spawnRoatation);
                    yield return new WaitForSeconds(spawnWait);
                }
            }
            yield return new WaitForSeconds(waveWait);
            if (gameOver) {
                HighScores(score);
                restartText.text = ("Press 'R' to Restart");
                restart = true;
                break;
            }
        }
    } // Main function for waves 

    public void EnemyDecider(int waveNumber) {
        if (waveNumber < 10) {
            hazardCount = 8;
            choice = Random.Range(0, 10);    
            if (choice <= 2) {
                decision = true;
            } else {
                decision = false;
            }
        } else if (waveNumber < 25) {
            hazardCount = 10;
            choice = Random.Range(0, 10);
            if (choice <= 3) {
                decision = true;
            } else {
                decision = false;
            }
        } else if (waveNumber < 50) {
            hazardCount = 12;
            choice = Random.Range(0, 10);
            if (choice <= 4) {
                decision = true;
            } else {
                decision =  false;
            }
        } else if (waveNumber < 100) {
            hazardCount = 15;
            choice = Random.Range(0, 10);
            if (choice <= 4) {
                decision = true;
            } else {
                decision = false;
            }
        } else if (waveNumber < 500) {
            hazardCount = 20;
            choice = Random.Range(0, 10);
            if (choice <= 5) {
                decision = true;
            } else {
                decision = false;
            }
        } else {
            hazardCount = 25;
            choice = Random.Range(0, 10);
            if (choice <= 5) {
                decision = true;
            } else {
                decision = false;
            }
        } 

    } // how game controller decides which enemy to spawn based on wave difficulty (FINALLY WORKING) 

    public void AddScore(int newScoreValue) {
        score += newScoreValue;
        UpdateScore();
    }
    void UpdateScore() {
        scoreText.text = "Score: " + score;
    }
    void UpdateWave() {
        WaveText.text = "Wave: " + waveCount;
    }
    public void GameOver() {
        gameOverText.text = ("Game Over!");
        gameOver = true;
    }
    void HighScores(int newHighscore) {

        if (gameOver) {
                      
           

            // Needs to add an input for users to put a name


            HighscoreTitle.text = ("Highscore: ");

            int oldHighscore = PlayerPrefs.GetInt("Highscore", 0);
            if (newHighscore > oldHighscore) {
                PlayerPrefs.SetInt("Highscore", newHighscore);
                Highscores.text = (newHighscore + "\n New Highscore");

            } else {
                PlayerPrefs.SetInt("Highscore", oldHighscore);
                Highscores.text = (oldHighscore + "\n Previous Highscore");
            }
        }
    }
}
