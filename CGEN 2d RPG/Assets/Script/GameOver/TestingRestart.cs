using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class TestingRestart : MonoBehaviour
{
    private List<Button> restartButtons = new List<Button>();

    public static TestingRestart instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        SetupRestartButtons();
    }

    public void SetupRestartButtons()
    {
        Button[] buttons = FindObjectsOfType<Button>();

        foreach (Button button in buttons)
        {
            if (button.CompareTag("RestartButton") || button.CompareTag("PopUpRestartButton"))
            {
                restartButtons.Add(button);
                button.onClick.RemoveAllListeners(); // Remove all listeners to avoid duplicates
                button.onClick.AddListener(RestartGame);
            }
        }
    }

    public void RestartGame()
    {
        DataPersistenceManager.instance.RestartGame();
    }
}
