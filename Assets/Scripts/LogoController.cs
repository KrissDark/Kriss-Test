using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class LogoController : MonoBehaviour
{
    private bool isTransitioning = false;

    // Ссылка на канвас GameScene
    [SerializeField] private Canvas gameSceneCanvas;

    void Update()
    {
        // Проверка нажатия клавиши R для перезагрузки текущей сцены
        if (Input.anyKeyDown)
        {
            ReloadScene();
            return; // Выходим из метода, чтобы не запускать LoadGameScene
        }

        // Проверка нажатия любой другой клавиши
        if (!isTransitioning && Input.anyKeyDown)
        {
            StartCoroutine(LoadGameScene());
        }
    }

    private IEnumerator LoadGameScene()
    {
        isTransitioning = true;

        // Загружаем GameScene в режиме добавления
        AsyncOperation loadOperation = SceneManager.LoadSceneAsync("GameScene", LoadSceneMode.Additive);
        loadOperation.allowSceneActivation = false; // Отключаем активирование сцены

        // Ждем, пока сцена загрузится
        while (!loadOperation.isDone)
        {
            // Проверяем, завершилась ли загрузка
            if (loadOperation.progress >= 0.9f)
            {
                // Отключаем все элементы интерфейса текущей сцены
                DisableCurrentUI();

                // Отключаем все объекты в GameScene
                DisableGameSceneUI();

                // Активируем сцену
                loadOperation.allowSceneActivation = true;
            }

            yield return null; // Ждем завершения загрузки
        }

        Debug.Log("GameScene Loaded.");

        // Включаем канвас GameScene, если он назначен
        if (gameSceneCanvas != null)
        {
            gameSceneCanvas.enabled = true;
            EnableGameSceneObjects(); // Активируем объекты в GameScene после загрузки
            Debug.Log("GameScene Canvas Enabled.");
        }
        else
        {
            Debug.LogWarning("GameScene Canvas is not assigned.");
        }

        // Задержка перед выгрузкой текущей сцены
        yield return new WaitForSeconds(1f); // Задержка перед выгрузкой
        Debug.Log("Unloading current scene...");

        // Убедитесь, что вы находитесь в правильной сцене перед выгрузкой
        AsyncOperation unloadOperation = SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene().buildIndex);

        // Ждем завершения выгрузки
        while (!unloadOperation.isDone)
        {
            yield return null; // Ждем завершения выгрузки
        }

        Debug.Log("Current scene Unloaded.");

        // Отключаем объект с логотипом
        gameObject.SetActive(false);
    }

    private void DisableCurrentUI()
    {
        // Отключаем все элементы интерфейса на текущей сцене
        Canvas[] canvases = Object.FindObjectsByType<Canvas>(FindObjectsSortMode.None);
        foreach (var canvas in canvases)
        {
            canvas.enabled = false; // Отключаем все канвасы
        }
    }

    private void DisableGameSceneUI()
    {
        // Отключаем все объекты UI в GameScene (предполагается, что они являются дочерними)
        GameObject[] gameSceneObjects = GameObject.FindGameObjectsWithTag("GameSceneUI"); // Предполагается, что у объектов UI есть тег GameSceneUI
        foreach (var obj in gameSceneObjects)
        {
            obj.SetActive(false);
        }
    }

    private void EnableGameSceneObjects()
    {
        // Активируем все объекты UI в GameScene
        GameObject[] gameSceneObjects = GameObject.FindGameObjectsWithTag("GameSceneUI");
        foreach (var obj in gameSceneObjects)
        {
            obj.SetActive(true);
        }
    }

    private void ReloadScene()
    {
        // Находим ScoreManager в текущей сцене
        ScoreManager scoreManager = FindFirstObjectByType<ScoreManager>();
        if (scoreManager != null)
        {
            scoreManager.ResetScore(); // Сбрасываем счет
        }
        else
        {
            Debug.LogWarning("ScoreManager not found in the current scene.");
        }

        Scene currentScene = SceneManager.GetActiveScene();
        Debug.Log("Current Scene Index: " + currentScene.buildIndex);
        SceneManager.LoadScene(currentScene.buildIndex); // Перезагружаем текущую сцену
    }
}