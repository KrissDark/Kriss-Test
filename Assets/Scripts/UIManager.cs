using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public TMP_Text[] scoreTexts; // Массив текстовых элементов для отображения очков

    private void Start()
    {
        // Находим ScoreManager на сцене
        ScoreManager scoreManager = Object.FindFirstObjectByType<ScoreManager>();
        if (scoreManager != null)
        {
            // Устанавливаем ссылки на текстовые элементы в ScoreManager
            scoreManager.SetScoreTexts(scoreTexts);
        }
        else
        {
            Debug.LogWarning("ScoreManager не найден на сцене!");
        }
    }
}