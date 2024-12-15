using UnityEngine;
using UnityEngine.SceneManagement; // Не забудьте добавить эту строчку!

public class PlayerController : MonoBehaviour
{
    public float jumpForce = 5f;
    public float rotationSpeed = 200f; // Скорость поворота
    private Rigidbody rb;
    private float initialZRotation; // Начальный угол
    public GameObject endUIGroup; // Ссылка на группу объектов в канвасе End
    private bool isGameEnded = false; // Флаг, указывающий, что игра закончена

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        initialZRotation = transform.eulerAngles.z; // Сохраняем начальный угол Z
        endUIGroup.SetActive(false);
    }

    void Update()
    {
        // Проверяем нажатие клавиши Space для прыжка
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.linearVelocity = new Vector3(0, jumpForce, 0); // Устанавливаем скорость прыжка
        }

        // Проверяем, закончилась ли игра, если да, то перезагружаем сцену
        if (isGameEnded && Input.anyKeyDown) // Проверяем, нажата ли любая клавиша
        {
            Debug.Log("Reloading scene: " + "GameScene");
            ReloadScene(); // Вызываем метод без параметров
        }
    }

    void FixedUpdate()
    {
        // Поворот игрока по оси Z во время прыжка или падения
        float newZRotation = transform.eulerAngles.z;

        // Если скорость по Y положительная (прыжок)
        if (rb.linearVelocity.y > 0)
        {
            newZRotation += rotationSpeed * Time.fixedDeltaTime * 2; // Поворачиваем вверх
        }
        // Если скорость по Y отрицательная (падение)
        else if (rb.linearVelocity.y < 0)
        {
            newZRotation -= rotationSpeed * Time.fixedDeltaTime * 2; // Поворачиваем вниз
        }

        // Ограничиваем угол вращения от изначального положения (0 до 90 градусов)
        newZRotation = Mathf.Clamp(newZRotation, initialZRotation - 30f, initialZRotation + 15f);

        // Применяем новое вращение
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, newZRotation);
    }

    private void OnTriggerEnter(Collider other)
    {
        // Проверяем, имеет ли объект, с которым произошло столкновение, тег "hit"
        if (other.CompareTag("Hit"))
        {
            //// Удаляем текущий игровой объект
            //Destroy(gameObject);
            if (endUIGroup != null)
            {
                endUIGroup.SetActive(true); // Включаем группу объектов
                isGameEnded = true; // Устанавливаем флаг игры завершён
            }
        }
    }

    private void ReloadScene()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        Debug.Log("Current Scene Index: " + currentScene.buildIndex);
        SceneManager.LoadScene(currentScene.buildIndex); // Перезагружаем текущую сцену
    }
}
