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

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    void Start()
    {
        
    }

    void Update()
    {
        if(Score > HighScore)
        {
            _highScore = Score;
        }
    }

    static int _highScore;
    static int _score;
}
