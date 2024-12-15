using UnityEngine;

public class GroundController : MonoBehaviour
{
    public SpeedController speedController;
    private float speed;
    [SerializeField]
    private Collider trigger;
    public Vector3 targetPosition;

    private void Start()
    {
        // Обновляем позицию объекта
        Vector3 newPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        transform.position = newPosition;

        // Для отладки (опционально)
        Debug.Log($"Объект {gameObject.name} перемещен на новую позицию: {newPosition}");

        if (speedController == null)
        {
            Debug.LogWarning("SpeedController не назначен!(земля)"); // Логируем предупреждение, если speedController равен null
        }
    }

    void FixedUpdate()
    {
        if (speedController != null)
        {
            speed = speedController.currentSpeed; // Обновляем скорость при каждом кадре
        }

        transform.position += Vector3.left * speed * Time.fixedDeltaTime; // Использовать Time.fixedDeltaTime в FixedUpdate
        // Debug.Log("Current Speed: " + speed); // Закомментируйте, если не нужно
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Что-то вошло в триггер(земля)");
        if (other.gameObject.CompareTag("TriggerWall"))
        {
            Debug.Log("Столкновение с триггером!(земля)");
            TeleportObject(); // Вызываем телепортацию
        }
    }

    public void TeleportObject()
    {
        transform.localPosition = targetPosition; // Устанавливаем позицию на targetPosition
        Debug.Log("Новая позиция для телепортации(земля): " + targetPosition.ToString());
    }
}