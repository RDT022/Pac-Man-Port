using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public int HighScore
    {
        get
        {
            return _highScore;
        }
    }

    public int Score
    {
        get
        {
            return _score;
        }
        set
        {
            _score = value;
        }
    }

    public List<GameObject> pellets;

    [SerializeField]
    GameObject[] bonusFruits;

    [SerializeField]
    GhostScript[] ghosts;

    [SerializeField]
    PlayerScript player;

    float bonusFruitTimer = 15;

    float powerPelletTimer = 10;

    bool setGhosts = false;

    public bool bonusFruitSpawned = false;

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    void Update()
    {
        if(Score > HighScore)
        {
            _highScore = Score;
        }
        if(pellets.Count == 0)
        {
            LoadScene("Scene 1");
        }
        if (!bonusFruitSpawned)
        {
            if (bonusFruitTimer <= 0)
            {
                Instantiate(bonusFruits[Random.Range((int)0, (int)bonusFruits.Length)], new Vector3(17, -11.5f, 0), new Quaternion(0, 0, 0, 0));
                bonusFruitTimer = 15;
                bonusFruitSpawned = true;
            }
            bonusFruitTimer -= Time.deltaTime;
        }
        if (player.poweredUp)
        {
            if (powerPelletTimer <= 0)
            {
                foreach (GhostScript ghost in ghosts)
                {
                    ghost.vulnerable = false;
                }
                player.poweredUp = false;
                powerPelletTimer = 15;
                setGhosts = false;
                player.ghostCounter = 0;
            }
            else if(setGhosts == false)
            {
                foreach (GhostScript ghost in ghosts)
                {
                    ghost.vulnerable = true;
                }
                setGhosts = true;
            }
            powerPelletTimer -= Time.deltaTime;
        }
    }

    static int _highScore;
    static int _score;
}
