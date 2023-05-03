using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [Header("Menu Buttons")]
    [SerializeField] private Button newGameButton;
    [SerializeField] private Button continueGameButton;

    private void Start()
    {
        if (!DataPersistenceManager.instance.HasGameData())
        {
            continueGameButton.interactable = false;
        }
    }

    public void OnNewGameClicked()
    {
         DisableMenuButtons();
        //create a new game - which will initialize our game data
         DataPersistenceManager.instance.NewGame();
        //Load the gameplay scene - which will in turn save the game because of 
        //OnSceneUnloaded() in the DataPersistenceManager.
          SceneManager.LoadSceneAsync("SampleScene");
       
    }

    public void OnContinueGameClicked()
    {
        DisableMenuButtons();
        // Load the next scene - which will in turn load the game because of
        // OnSceneLoaded() in the DataPersistenceManager
        string currentScene = DataPersistenceManager.instance.GetCurrentSceneName();
        if (!string.IsNullOrEmpty(currentScene))
        {
            SceneManager.LoadSceneAsync(currentScene);
        }
        else
        {
            Debug.LogWarning("No saved game data available.");
        }
    }



    public void QuitApp()
    {
        Application.Quit();
        Debug.Log("Application has quit.");
    }

    private void DisableMenuButtons()
    {
        newGameButton.interactable = false;
        continueGameButton.interactable = false;
    }
}
