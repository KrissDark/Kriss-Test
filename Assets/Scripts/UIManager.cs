using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public TMP_Text[] scoreTexts; // ������ ��������� ��������� ��� ����������� �����

    private void Start()
    {
        // ������� ScoreManager �� �����
        ScoreManager scoreManager = Object.FindFirstObjectByType<ScoreManager>();
        if (scoreManager != null)
        {
            // ������������� ������ �� ��������� �������� � ScoreManager
            scoreManager.SetScoreTexts(scoreTexts);
        }
        else
        {
            Debug.LogWarning("ScoreManager �� ������ �� �����!");
        }
    }
}