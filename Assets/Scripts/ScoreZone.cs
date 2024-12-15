using UnityEngine;

public class ScoreZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // ������� ScoreManager �� �����
            ScoreManager scoreManager = Object.FindFirstObjectByType<ScoreManager>();
            if (scoreManager != null)
            {
                // ����������� ���� � ������
                scoreManager.IncrementScore();
            }
            else
            {
                Debug.LogWarning("ScoreManager �� ������ �� �����!");
            }
        }
    }
}