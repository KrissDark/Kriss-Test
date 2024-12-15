using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    private int score;

    public TMP_Text[] scoreTexts; // ������ ��� �������� ������ �� ��������� ��������

    private void Start()
    {
        // ����� ����� ��� ������
        ResetScore();
    }

    public void IncrementScore()
    {
        score++;
        Debug.Log("����: " + score);
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
        Debug.Log("���� �������.");
    }

    private void UpdateScoreText()
    {
        foreach (var scoreText in scoreTexts)
        {
            if (scoreText != null)
            {
                scoreText.text = "����: " + score.ToString();
            }
            else
            {
                Debug.LogWarning("���� �� scoreText �� ��������.");
            }
        }
    }
}
