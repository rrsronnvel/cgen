using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class TestingRestart : MonoBehaviour
{
    private List<Button> restartButtons = new List<Button>();

    public static TestingRestart instance;

    private List<Button> additionalButtons = new List<Button>();


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
        SetupAdditionalButtons();
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

   

    public void SetupAdditionalButtons()
    {
        Button[] buttons = FindObjectsOfType<Button>();

        foreach (Button button in buttons)
        {
            if (button.CompareTag("MainMenuButton"))
            {
                button.onClick.RemoveAllListeners();
                button.onClick.AddListener(Menu.instance.GoToMainMenu);
            }
            else if (button.CompareTag("QuitAppButton"))
            {
                button.onClick.RemoveAllListeners();
                button.onClick.AddListener(Menu.instance.QuitApp);
            }
            else if (button.CompareTag("BackShipButton"))
            {
                button.onClick.RemoveAllListeners();
                button.onClick.AddListener(Menu.instance.BackShip);
            }
        }
    }


    public void RestartGame()
    {
        DataPersistenceManager.instance.RestartGame();
    }
}
