using UnityEngine;

public class PipeController : MonoBehaviour
{
    public SpeedController speedController;
    private float speed;
    [SerializeField]
    private Collider trigger;
    public Vector3 targetPosition;

    private void Start()
    {
        // Получаем текущую позицию объекта
        Vector3 currentPosition = transform.position;

        // Генерируем случайное значение по оси Y от -10 до +10
        float randomY = Random.Range(-10f, 10f);

        // Создаем новую позицию с измененной Y координатой
        Vector3 newPosition = new Vector3(currentPosition.x, currentPosition.y + randomY, currentPosition.z);

        // Обновляем позицию объекта
        transform.position = newPosition;

        // Для отладки (опционально)
        Debug.Log($"Объект {gameObject.name} перемещен на новую позицию: {newPosition}");

        if (speedController == null)
        {
            Debug.LogWarning("SpeedController не назначен!"); // Логируем предупреждение, если speedController равен null
        }
    }

    void FixedUpdate()
    {
        if (speedController != null)
        {
            speed = speedController.currentSpeed; // Обновляем скорость при каждом кадре
        }

        transform.position += Vector3.left * speed * Time.fixedDeltaTime;
        //Debug.Log("Current Speed: " + speed);
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Что-то вошло в триггер");
        if (other.gameObject.CompareTag("TriggerWall"))
        {
            Debug.Log("Столкновение с триггером!");
            TeleportWithRandomY(); // Теперь вызываем только этот метод
        }
    }

    public void TeleportWithRandomY()
    {
        Debug.Log("Текущая targetPosition: " + targetPosition.ToString());
        float randomY = Random.Range(targetPosition.y - 12f, targetPosition.y + 12f);
        Vector3 newPosition = new Vector3(targetPosition.x, randomY, targetPosition.z);
        Debug.Log("Новая позиция для телепортации: " + newPosition.ToString());
        TeleportObject(newPosition);
    }

    public void TeleportObject(Vector3 newPosition)
    {
        transform.position = newPosition; // Исправлено имя параметра
    }
}