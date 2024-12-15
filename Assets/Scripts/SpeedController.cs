using UnityEngine;

public class SpeedController : MonoBehaviour
{
    public float initialSpeed = 15.0f; // Начальная скорость
    public float acceleration = 1.0f;  // Увеличение скорости за секунду
    public float maxSpeed = 30.0f;     // Максимальная скорость

    public float currentSpeed;         // Текущая скорость

    void Start()
    {
        currentSpeed = initialSpeed; // Устанавливаем начальную скорость
        Debug.Log("Начальная скорость установлена: " + currentSpeed);
    }

    void FixedUpdate()
    {
        // Увеличиваем скорость
        if (currentSpeed < maxSpeed)
        {
            currentSpeed += acceleration * Time.fixedDeltaTime; // Увеличиваем скорость с течением времени
            //Debug.Log("Initial Speed: " + currentSpeed);
        }

        // Применение скорости к объекту
        //transform.Translate(Vector3.forward * currentSpeed * Time.deltaTime);
    }
}