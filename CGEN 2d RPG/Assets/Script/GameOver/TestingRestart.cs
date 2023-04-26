using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class TestingRestart : MonoBehaviour
{
    private List<Button> restartButtons = new List<Button>();
    [SerializeField] public Button button2;


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

        // Find Button2 in the scene using its tag
        GameObject button2Object = GameObject.FindGameObjectWithTag("RestartButton");
        if (button2Object != null)
        {
            button2 = button2Object.GetComponent<Button>();
        }
        else
        {
            Debug.LogError("Button2 is not found.");
        }

    }

   public void SetupRestartButtons()
{
    Button[] buttons = FindObjectsOfType<Button>();

    foreach (Button button in buttons)
    {
        if (button.CompareTag("RestartButton"))
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
