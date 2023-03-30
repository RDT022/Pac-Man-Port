using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Text;

public enum MovementDirections
{
    Up,
    Down,
    Left,
    Right
}
public class GameManager : MonoBehaviour
{
    public static GameManager instance;
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

    [SerializeField]
    TextMeshProUGUI highScoreUI;

    [SerializeField]
    TextMeshProUGUI scoreUI;

    [SerializeField]
    TextMeshProUGUI livesUI;

    public List<GameObject> pellets;

    [SerializeField]
    GameObject[] bonusFruits;

    [SerializeField]
    GameObject[] bonusFruitIcons;

    GameObject activeFruit;

    [SerializeField]
    GhostScript[] ghosts;

    [SerializeField]
    PlayerScript player;

    StringBuilder sb = new StringBuilder();

    float bonusFruitTimer = 20;

    float powerPelletTimer = 15;

    int bonusFruitCounter = 0;

    int counter = 0;

    Vector3 fruitSpawnPosition = new Vector3(17, -11.5f, 0);

    public bool bonusFruitSpawned = false;

    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        StartCoroutine(beginLevel());
        powerPelletTimer -= _loop / 2.0f;
        if (powerPelletTimer < 5)
        {
            powerPelletTimer = 5;
        }
        foreach (GhostScript ghost in ghosts)
        {
            ghost.amplifySpeed(_loop / 3);
        }
        for(int i = 0; i < 8; i++)
        {
            if(i <= _loop)
            {
                bonusFruitIcons[i].SetActive(true);
            }
        }
    }
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    void Update()
    {
        if (Score > HighScore)
        {
            _highScore = Score;
        }
        if (pellets.Count == 0)
        {
            _loop++;
            LoadScene("Level 1");
        }
        if (player.poweredUp)
        {
            StopCoroutine(powerPelletRoutine());
            StartCoroutine(powerPelletRoutine());
        }
        highScoreUI.text = HighScore.ToString();
        scoreUI.text = Score.ToString();
        if (player.lives < 0)
        {
            SceneManager.LoadScene("EndScreen");
        }
    }

    public void FreezeGameplay()
    {
        StopAllCoroutines();
        if (activeFruit != null)
        {
            activeFruit.GetComponent<BonusFruitScript>().Despawn();
        }
        player.isEnabled = false;
        player.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        foreach (GhostScript ghost in ghosts)
        {
            ghost.isEnabled = false;
            ghost.turnTransparent();
        }
    }

    public void ResetLevel()
    {
        player.transform.position = new Vector3(17, -17.5f, 0);
        player.movementDirection = MovementDirections.Left;
        foreach (GhostScript ghost in ghosts)
        {
            ghost.Reset();
        }
        StartCoroutine(beginLevel());
    }

    IEnumerator bonusFruitRoutine()
    {
        while (true)
        {
            yield return new WaitUntil(() => !bonusFruitSpawned);
            yield return new WaitForSeconds(bonusFruitTimer);

            if (counter < 8)
            {
                activeFruit = bonusFruits[counter];
                activeFruit.transform.position = fruitSpawnPosition;
                activeFruit.GetComponent<BonusFruitScript>().enabled = true;
                bonusFruitSpawned = true;
                bonusFruitCounter++;
                counter++;
            }

            if (bonusFruitCounter > _loop)
            {
                bonusFruitCounter = 0;
                counter = 0;
            }

            if (counter >= 8)
            {
                counter = 0;
            }
        }
    }

    IEnumerator powerPelletRoutine()
    {
        foreach (GhostScript ghost in ghosts)
        {
            ghost.vulnerable = true;
        }
        player.poweredUp = false;

        yield return new WaitForSeconds(powerPelletTimer - (powerPelletTimer/3));

        foreach(GhostScript ghost in ghosts)
        {
            ghost.animator.SetBool("WearingOff", true);
        }

        yield return new WaitForSeconds(powerPelletTimer / 3);

        foreach (GhostScript ghost in ghosts)
        {
            ghost.vulnerable = false;
        }
        player.ghostCounter = 0;
    }

    IEnumerator beginLevel()
    {
        sb.Append("x ");
        sb.Append(player.lives);
        livesUI.text = sb.ToString();
        yield return new WaitForSeconds(5);
        StartCoroutine(bonusFruitRoutine());
        player.isEnabled = true;
        player.ResetMomentum();
        foreach (GhostScript ghost in ghosts)
        {
            ghost.isEnabled = true;
        }
        sb.Clear();
    }

    static int _highScore;
    static int _score;
    static int _loop = 0;
}
