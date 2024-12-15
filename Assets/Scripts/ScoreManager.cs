using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    private int score;

    public TMP_Text[] scoreTexts; // Массив для хранения ссылок на текстовые элементы

    private void Start()
    {
        // Сброс счета при старте
        ResetScore();
    }

    public void IncrementScore()
    {
        score++;
        Debug.Log("Очки: " + score);
        UpdateScoreText();
    }

    public int GetScore()
    {
        return score;
    }

    public void SetScoreTexts(TMP_Text[] texts)
    {
        scoreTexts = texts;
        UpdateScoreText();
    }

    public void ResetScore()
    {
        score = 0;
        UpdateScoreText();
        Debug.Log("Счет сброшен.");
    }

    private void UpdateScoreText()
    {
        foreach (var scoreText in scoreTexts)
        {
            if (scoreText != null)
            {
                scoreText.text = "Очки: " + score.ToString();
            }
            else
            {
                Debug.LogWarning("Один из scoreText не назначен.");
            }
        }
    }
}
