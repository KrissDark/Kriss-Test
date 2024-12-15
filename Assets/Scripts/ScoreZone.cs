using UnityEngine;

public class ScoreZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Находим ScoreManager на сцене
            ScoreManager scoreManager = Object.FindFirstObjectByType<ScoreManager>();
            if (scoreManager != null)
            {
                // Увеличиваем счет у игрока
                scoreManager.IncrementScore();
            }
            else
            {
                Debug.LogWarning("ScoreManager не найден на сцене!");
            }
        }
    }
}