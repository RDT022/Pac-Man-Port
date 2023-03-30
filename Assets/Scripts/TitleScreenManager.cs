using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class TitleScreenManager : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI highScoreUI;
    private void Start()
    {
        if (GameManager.instance != null)
        {
            highScoreUI.text = GameManager.instance.HighScore.ToString();
        }
    }
    public void StartGame()
    {
        if (GameManager.instance != null)
        {
            GameManager.instance.Score = 0;
        }
        SceneManager.LoadScene("Level 1");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
