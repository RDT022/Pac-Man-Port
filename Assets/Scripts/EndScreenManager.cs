using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class EndScreenManager : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI highScoreUI;

    [SerializeField]
    TextMeshProUGUI scoreUI;
    private void Start()
    {
        highScoreUI.text = GameManager.instance.HighScore.ToString();
        scoreUI.text = GameManager.instance.Score.ToString();
    }
    public void BackToMenu()
    {
        SceneManager.LoadScene("TitleScreen");
    }
}
