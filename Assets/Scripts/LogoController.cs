using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class LogoController : MonoBehaviour
{
    private bool isTransitioning = false;

    // ������ �� ������ GameScene
    [SerializeField] private Canvas gameSceneCanvas;

    void Update()
    {
        // �������� ������� ������� R ��� ������������ ������� �����
        if (Input.anyKeyDown)
        {
            ReloadScene();
            return; // ������� �� ������, ����� �� ��������� LoadGameScene
        }

        // �������� ������� ����� ������ �������
        if (!isTransitioning && Input.anyKeyDown)
        {
            StartCoroutine(LoadGameScene());
        }
    }

    private IEnumerator LoadGameScene()
    {
        isTransitioning = true;

        // ��������� GameScene � ������ ����������
        AsyncOperation loadOperation = SceneManager.LoadSceneAsync("GameScene", LoadSceneMode.Additive);
        loadOperation.allowSceneActivation = false; // ��������� ������������� �����

        // ����, ���� ����� ����������
        while (!loadOperation.isDone)
        {
            // ���������, ����������� �� ��������
            if (loadOperation.progress >= 0.9f)
            {
                // ��������� ��� �������� ���������� ������� �����
                DisableCurrentUI();

                // ��������� ��� ������� � GameScene
                DisableGameSceneUI();

                // ���������� �����
                loadOperation.allowSceneActivation = true;
            }

            yield return null; // ���� ���������� ��������
        }

        Debug.Log("GameScene Loaded.");

        // �������� ������ GameScene, ���� �� ��������
        if (gameSceneCanvas != null)
        {
            gameSceneCanvas.enabled = true;
            EnableGameSceneObjects(); // ���������� ������� � GameScene ����� ��������
            Debug.Log("GameScene Canvas Enabled.");
        }
        else
        {
            Debug.LogWarning("GameScene Canvas is not assigned.");
        }

        // �������� ����� ��������� ������� �����
        yield return new WaitForSeconds(1f); // �������� ����� ���������
        Debug.Log("Unloading current scene...");

        // ���������, ��� �� ���������� � ���������� ����� ����� ���������
        AsyncOperation unloadOperation = SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene().buildIndex);

        // ���� ���������� ��������
        while (!unloadOperation.isDone)
        {
            yield return null; // ���� ���������� ��������
        }

        Debug.Log("Current scene Unloaded.");

        // ��������� ������ � ���������
        gameObject.SetActive(false);
    }

    private void DisableCurrentUI()
    {
        // ��������� ��� �������� ���������� �� ������� �����
        Canvas[] canvases = Object.FindObjectsByType<Canvas>(FindObjectsSortMode.None);
        foreach (var canvas in canvases)
        {
            canvas.enabled = false; // ��������� ��� �������
        }
    }

    private void DisableGameSceneUI()
    {
        // ��������� ��� ������� UI � GameScene (��������������, ��� ��� �������� ���������)
        GameObject[] gameSceneObjects = GameObject.FindGameObjectsWithTag("GameSceneUI"); // ��������������, ��� � �������� UI ���� ��� GameSceneUI
        foreach (var obj in gameSceneObjects)
        {
            obj.SetActive(false);
        }
    }

    private void EnableGameSceneObjects()
    {
        // ���������� ��� ������� UI � GameScene
        GameObject[] gameSceneObjects = GameObject.FindGameObjectsWithTag("GameSceneUI");
        foreach (var obj in gameSceneObjects)
        {
            obj.SetActive(true);
        }
    }

    private void ReloadScene()
    {
        // ������� ScoreManager � ������� �����
        ScoreManager scoreManager = FindFirstObjectByType<ScoreManager>();
        if (scoreManager != null)
        {
            scoreManager.ResetScore(); // ���������� ����
        }
        else
        {
            Debug.LogWarning("ScoreManager not found in the current scene.");
        }

        Scene currentScene = SceneManager.GetActiveScene();
        Debug.Log("Current Scene Index: " + currentScene.buildIndex);
        SceneManager.LoadScene(currentScene.buildIndex); // ������������� ������� �����
    }
}