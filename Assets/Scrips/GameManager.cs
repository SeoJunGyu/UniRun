using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public bool IsGameOver { get; private set; }

    private int score;

    private TextMeshProUGUI scoreText;
    private GameObject gameOverUi;

    private void Awake()
    {
        scoreText = GameObject.FindWithTag("Score").GetComponent<TextMeshProUGUI>();
        gameOverUi = GameObject.FindWithTag("GameOver");

        gameOverUi.SetActive(false);
        IsGameOver = false;
        score = 0;

    }

    private void Update()
    {

        if(IsGameOver && Input.GetMouseButtonUp(0))
        {
            //score = 0;

            //name이나 buildIndex 아무거나 써도 같은 결과다.
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    public void AddScore(int add)
    {
        if(!IsGameOver)
        {
            score += add;
            scoreText.text = $"Score: {score}";
        }
    }

    public void OnPlayerDead()
    {
        IsGameOver = true;
        gameOverUi.SetActive(true);
    }
}
